using AutoMapper;
using EkspertBooker.Model.Requests;
using EkspertBooker.WebAPI.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EkspertBooker.WebAPI.Service
{
    public class RecenzijeOPoslodavciService : BaseCRUDService<Model.RecenzijaOPoslodavac, RecenzijeOPoslodavciSearchRequest, RecenzijaOPoslodavacUpsertRequest, RecenzijaOPoslodavacUpsertRequest, Database.RecenzijaOPoslodavac>
    {
        public RecenzijeOPoslodavciService(EkspertBookerContext context, IMapper mapper):base(context, mapper)
        {
        }

        public override List<Model.RecenzijaOPoslodavac> Get(RecenzijeOPoslodavciSearchRequest search)
        {
            if(search.PoslodavacId.HasValue) 
            {
                return _mapper.Map<List<Model.RecenzijaOPoslodavac>>(_context.RecenzijeOPoslodavci.Where(rop=>rop.PoslodavacId == search.PoslodavacId).ToList());
            } else
            {
                return _mapper.Map<List<Model.RecenzijaOPoslodavac>>(_context.RecenzijeOPoslodavci.ToList());
            }
        }

        public override Model.RecenzijaOPoslodavac Insert(RecenzijaOPoslodavacUpsertRequest request)
        {
            request.Vrijeme = DateTime.Now;
            try
            {
                var result = base.Insert(request);
                //update info o poslodavcu, ponovo izracunati prosjecnu ocjenu
                var poslodavac = _context.Poslodavci.Find(result.PoslodavacId);
                if (poslodavac.BrojZavrsenihProjekata > 0)
                {
                    poslodavac.ProsjecnaOcjena = ((poslodavac.ProsjecnaOcjena * poslodavac.BrojRecenzija ) + request.Ocjena) / (poslodavac.BrojRecenzija + 1);
                }
                poslodavac.BrojRecenzija++;
                _context.SaveChanges();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override bool Delete(int id)
        {
            return base.Delete(id);
        }

    }
}
