using AutoMapper;
using EkspertBooker.Model.Requests;
using EkspertBooker.WebAPI.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EkspertBooker.WebAPI.Service
{
    public class PonudeService : BaseCRUDService<Model.Ponuda, PonudeSearchRequest, PonudaUpsertRequest, PonudaUpsertRequest, Database.Ponuda>
    {
        public PonudeService(EkspertBookerContext context, IMapper mapper) :base(context, mapper)
        {
        }

        public override Model.Ponuda GetById(int id)
        {
            var ponuda = _context.Ponude.Include(p => p.Ekspert).ThenInclude(e => e.Korisnik).Where(p => p.PonudaId == id).SingleOrDefault();
            return _mapper.Map<Model.Ponuda>(ponuda);
        }

        public override List<Model.Ponuda> Get(PonudeSearchRequest search)
        {
            var query = _context.Ponude.AsQueryable();

            if(search.EkspertId.HasValue )
            {
                query = query.Where(p => p.EkspertId == search.EkspertId);
            }

            if(search.ProjektId.HasValue)
            {
                query = query.Where(p => p.ProjektId == search.ProjektId);
            }

            if(search.Status.HasValue)
            {
                query = query.Where(p => p.Status == search.Status);
            }

            var result = query.Include(p=>p.Ekspert).ThenInclude(e=>e.Korisnik).ToList();
            if (result.Count == 0) return null; else
            return _mapper.Map<List<Model.Ponuda>>(result);
        }



        public override Model.Ponuda Update(int id, PonudaUpsertRequest request)
        {
            var entity = _context.Set<Ponuda>().Find(id);
            _mapper.Map(request, entity);
            var projekt = _context.Projekti.Find(request.ProjektId);
            try
            {
                if (request.Status == 2)
                {
                    //ako je ponuda prihvacena projekat je sada aktivan, a odbij sve ostale ponude
                    if (projekt.StanjeId == "Aktivan") throw new Exception("Projekt vec aktivan!");
                    projekt.StanjeId = "Aktivan";
                    projekt.EkspertId = request.EkspertId;
                    var ostale_ponude = _context.Ponude.Where(p => p.PonudaId != id && p.ProjektId == projekt.ProjektId).Include(p=>p.Ekspert).ToList();
                    foreach (var item in ostale_ponude)
                    {
                        if(item.Ekspert.Notifikacije == true)
                        {
                            _context.NotifikacijeEksperti.Add(new NotifikacijaEkspert
                            {
                                EkspertId = item.EkspertId,
                                Poruka = "Vaša ponuda je odbijena, žao nam je! Projekt ime: " + projekt.Naziv,
                                ProjektId = item.ProjektId,
                                Vrijeme= DateTime.Now
                            });
                        }
                        item.Status = 0;
                    }

                    //takodjer dodati sada projekt detalje za dati projekat
                    if (_context.ProjektDetalji.Find(projekt.ProjektId) == null)
                        _context.ProjektDetalji.Add(new ProjektDetalji
                        {
                            ProjektId = projekt.ProjektId
                        });
                    //potrebno obavijestiti eksperta o prihvacenoj ponudi po potrebi
                    var ekspert = _context.Eksperti.Find(request.EkspertId);
                    if(ekspert.Notifikacije == true)
                    {
                        _context.NotifikacijeEksperti.Add(new NotifikacijaEkspert
                        {
                            EkspertId = ekspert.KorisnikId,
                            Poruka = "Vaša ponuda je prihvaćena, čestitamo! Projekt ime: " + projekt.Naziv,
                            ProjektId = projekt.ProjektId,
                            Vrijeme = DateTime.Now
                        });
                    }
                    _context.SaveChanges();
                    return _mapper.Map<Model.Ponuda>(request);
                }
                else
                {
                    if(request.Status == 0)
                    {
                        _context.NotifikacijeEksperti.Add(new NotifikacijaEkspert
                        {
                            EkspertId = entity.EkspertId,
                            Poruka = "Vaša ponuda je odbijena, žao nam je! Projekt ime: " + projekt.Naziv,
                            ProjektId = entity.ProjektId,
                            Vrijeme = DateTime.Now
                        });
                    }
                    _context.SaveChanges();
                    return _mapper.Map<Model.Ponuda>(request);
                }
            }
            catch(Exception ex)
            {
                var exception = ex;
                throw new Exception();
            }
        }

        public override Model.Ponuda Insert(PonudaUpsertRequest request)
        {
            var projekt = _context.Projekti.Find(request.ProjektId);
            if (projekt != null)
            {
                projekt.BrojPonuda += 1;
                _context.SaveChanges();
                request.VrijemePonude = DateTime.Now;
                try
                {
                    var nova_ponuda = base.Insert(request);
                    var poslodavac = _context.Poslodavci.Find(projekt.PoslodavacId);
                    if(poslodavac != null)
                    {
                        if(poslodavac.Notifikacije == true)
                        {
                            _context.NotifikacijePoslodavci.Add(new NotifikacijaPoslodavac
                            {
                                PoslodavacId = poslodavac.KorisnikId,
                                EkspertId = nova_ponuda.EkspertId,
                                ProjektId = nova_ponuda.ProjektId,
                                Poruka = "Dobili ste novu ponudu za projekat: " + projekt.Naziv,
                                Vrijeme = DateTime.Now
                            });
                        }
                    }
                    _context.SaveChanges();
                    nova_ponuda.Projekt = null;
                    return nova_ponuda;
                }
                catch
                {
                    throw new Exception("Problem prilikom insert ponude ili notifikacije");
                }
                
            } else
            {
                throw new Exception("nema projekt za ponudu");
            }
        }

        public override bool Delete(int id)
        {
            var ponuda = _context.Ponude.Find(id);
            if (ponuda != null)
            {
                var projekt = _context.Projekti.Find(ponuda.ProjektId);
                if(projekt.BrojPonuda > 0)
                {
                    projekt.BrojPonuda -= 1;
                }
                _context.SaveChanges();
                return base.Delete(id);
            } else
            {
                return false;
            }
        }
    }
}
