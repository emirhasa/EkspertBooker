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

            return _mapper.Map<List<Model.Ponuda>>(result);
        }



        public override Model.Ponuda Update(int id, PonudaUpsertRequest request)
        {
            var entity = _context.Set<Ponuda>().Find(id);
            _mapper.Map(request, entity);
            try
            {
                if (request.Status == 2)
                {
                    //ako je ponuda prihvacena projekat je sada aktivan, a odbij sve ostale ponude
                    var projekt = _context.Projekti.Find(request.ProjektId);
                    if (projekt.StanjeId == "Aktivan") throw new Exception("Projekt vec aktivan!");
                    projekt.StanjeId = "Aktivan";
                    var ostale_ponude = _context.Ponude.Where(p => p.PonudaId != id).ToList();
                    foreach (var item in ostale_ponude)
                    {
                        item.Status = 0;
                    }
                    _context.SaveChanges();
                    return _mapper.Map<Model.Ponuda>(request);
                }
                else
                {
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

        public override bool Delete(int id)
        {
            return base.Delete(id);
        }
    }
}
