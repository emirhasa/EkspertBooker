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
    public class PoslodavciService : BaseCRUDService<Model.Poslodavac, PoslodavciSearchRequest, PoslodavacUpsertRequest, PoslodavacUpsertRequest, Database.Poslodavac>
    {
        public PoslodavciService(EkspertBookerContext context, IMapper mapper) :base(context, mapper)
        {
        }

        public override List<Model.Poslodavac> Get(PoslodavciSearchRequest search)
        {
            if(search != null )
            {
                if(search.BrojZavrsenihProjekata.HasValue)
                {
                    var result_alt = _mapper.Map<List<Model.Poslodavac>>(_context.Poslodavci.Include(p => p.KorisnikUloga).Where(p=>p.BrojZavrsenihProjekata>=search.BrojZavrsenihProjekata).ToList());
                    return result_alt;
                }
            } 
            var result =  _mapper.Map<List<Model.Poslodavac>>(_context.Poslodavci.Include(p=>p.KorisnikUloga).Include(p=>p.Korisnik).ToList());
            return result;
        }

        public override Model.Poslodavac GetById(int id)
        {
            return _mapper.Map<Model.Poslodavac>(_context.Poslodavci.Include(p => p.KorisnikUloga.Korisnik).First(p => p.PoslodavacId == id));
        }
    }
}
