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
    public class EkspertiService : BaseCRUDService<Model.Ekspert, EkspertiSearchRequest, EkspertUpsertRequest, EkspertUpsertRequest, Database.Ekspert>
    {
        public EkspertiService(EkspertBookerContext context, IMapper mapper) :base(context, mapper)
        {
        }

        public override List<Model.Ekspert> Get(EkspertiSearchRequest search)
        {
            if(search != null )
            {
                if(search.BrojZavrsenihProjekata.HasValue)
                {
                    var result_alt = _mapper.Map<List<Model.Ekspert>>(_context.Eksperti.Include(e => e.KorisnikUloga).Where(e=>e.BrojZavrsenihProjekata>=search.BrojZavrsenihProjekata).ToList());
                    return result_alt;
                }
            } 
            var result =  _mapper.Map<List<Model.Ekspert>>(_context.Eksperti.Include(e=>e.KorisnikUloga).Include(e=>e.Korisnik).ToList());
            return result;
        }

        public override Model.Ekspert GetById(int id)
        {
            return _mapper.Map<Model.Ekspert >(_context.Eksperti.Include(e=>e.KorisnikUloga).Include(e => e.Korisnik).First(e => e.EkspertId == id));
        }

    }
}
