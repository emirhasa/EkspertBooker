using AutoMapper;
using EkspertBooker.Model.Requests;
using EkspertBooker.WebAPI.Database;
using EkspertBooker.WebAPI.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EkspertBooker.WebAPI.Service
{
    public class KorisniciKategorijeService : IKorisniciKategorijeService
    {
        protected readonly IMapper _mapper;
        protected readonly EkspertBookerContext _context;
        public KorisniciKategorijeService(EkspertBookerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<Model.KorisnikKategorija> Get(KorisniciKategorijeSearchRequest search)
        {
            if(search.KategorijaId.HasValue)
            {
                return _mapper.Map<List<Model.KorisnikKategorija>>(_context.KorisniciKategorije.Where(kk => kk.KategorijaId == search.KategorijaId));
            } else
            {
                return _mapper.Map<List<Model.KorisnikKategorija>>(_context.KorisniciKategorije.Where(kk => kk.KorisnikId == search.KorisnikId));
            }
        }

        public Model.KorisnikKategorija GetById(int id)
        {
            return _mapper.Map<Model.KorisnikKategorija>(_context.KorisniciKategorije.Find(id));
        }

        /*public List<Model.KorisnikKategorija> Insert(KorisnikKategorijeUpsertRequest insert)
        {
            throw new UserException("Ima samo update");
        }*/

        public List<Model.KorisnikKategorija> Update(int id, KorisnikKategorijeUpsertRequest request)
        {
            if (request.Kategorije.Count > 0)
            {
                if(id != 0)
                {
                    try
                    {
                        //obrisi postojece kategorije od korisnika
                        var postojece_kategorije = _context.KorisniciKategorije.Where(kk => kk.KorisnikId == id);
                        foreach (var kategorija in postojece_kategorije)
                        {
                            _context.Remove(kategorija);
                        }
                        _context.SaveChanges();
                        //dodaj nove
                        foreach (Model.Kategorija item in request.Kategorije)
                        {
                            var entity = _mapper.Map<Database.KorisnikKategorija>(item);
                            _context.Add(entity);
                            _context.SaveChanges();
                        }
                        return _mapper.Map<List<Model.KorisnikKategorija>>(_context.KorisniciKategorije.Where(kk => kk.KorisnikId == id));
                    } catch
                    {
                        throw new Exception("Greska prilikom unosa!");
                    }
                } else
                {
                    throw new Exception("Niste proslijedili id korisnika, a vrsite update nj. kategorija!");
                }
            }
            else
            {
                throw new Exception("Niste proslijedili kategorije, a vrsite insert!");
            }
        }

        public bool Delete(int id)
        {
            var entity = _context.KorisniciKategorije.Find(id);
            if(entity != null )
            {
                _context.Remove(entity);
                return true;
            } else
            {
                return false;
            }
        }

    }
}
