using AutoMapper;
using EkspertBooker.Model.Requests;
using EkspertBooker.WebAPI.Database;
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

        public override List<Model.Ponuda> Get(PonudeSearchRequest search)
        {
            if(search.EkspertId.HasValue || search.ProjektId.HasValue)
            {
                if (search.EkspertId.HasValue)
                {
                    return _mapper.Map<List<Model.Ponuda>>(_context.Ponude.Where(p => p.EkspertId == search.EkspertId).ToList());
                }
                else return _mapper.Map<List<Model.Ponuda>>(_context.Ponude.Where(p => p.ProjektId == search.ProjektId).ToList());
            } else
            {
                return _mapper.Map<List<Model.Ponuda>>(_context.Ponude.ToList());
            }
        }

        public override Model.Ponuda Update(int id, PonudaUpsertRequest request)
        {
            var entity = _context.Set<Ponuda>().Find(id);

            _mapper.Map(request, entity);
            _context.SaveChanges();
            try
            {
                if (request.Status == true)
                {
                    //ako je ponuda prihvacena projekat je sada aktivan, a odbij sve ostale ponude
                    var projekt = _context.Projekti.Find(request.ProjektId);
                    if (projekt.StanjeId == "Aktivan") throw new Exception("Projekat vec aktivan!");
                    projekt.Stanje = new Stanje
                    {
                        StanjeId = "Aktivan"
                    };
                    var ostale_ponude = _context.Ponude.Where(p => p.PonudaId != id).ToList();
                    foreach (var item in ostale_ponude)
                    {
                        item.Status = false;
                    }
                    _context.SaveChanges();
                    var prihvacena_ponuda = _context.Ponude.Find(id);
                    prihvacena_ponuda.Status = true;
                    _context.SaveChanges();
                    return _mapper.Map<Model.Ponuda>(prihvacena_ponuda);
                }
                else
                {
                    //samo odbij ponudu
                    var odbijena_ponuda = _context.Ponude.Find(id);
                    odbijena_ponuda.Status = false;
                    _context.SaveChanges();
                    return _mapper.Map<Model.Ponuda>(odbijena_ponuda);
                }
            }
            catch
            {
                throw new Exception();
            }
        }

        public override bool Delete(int id)
        {
            return base.Delete(id);
        }
    }
}
