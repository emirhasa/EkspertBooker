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
                    var result_alt = _mapper.Map<List<Model.Ekspert>>(_context.Eksperti.Include(e => e.KorisnikUloga).Include(e=>e.Korisnik).ThenInclude(k=>k.KorisnikSlika).Where(e=>e.BrojZavrsenihProjekata>=search.BrojZavrsenihProjekata).ToList());
                    return result_alt;
                }
            } 
            var result =  _mapper.Map<List<Model.Ekspert>>(_context.Eksperti.Include(e=>e.EkspertStrucnaKategorija).Include(e=>e.KorisnikUloga).Include(e=>e.Korisnik).ThenInclude(k => k.KorisnikSlika).ToList());
            return result;
        }

        public override Model.Ekspert GetById(int id)
        {
            return _mapper.Map<Model.Ekspert>(_context.Eksperti.Include(e => e.EkspertStrucnaKategorija).Include(e=>e.KorisnikUloga).Include(e => e.Korisnik).ThenInclude(k => k.KorisnikSlika).Where(e => e.KorisnikId == id).SingleOrDefault());
        }

        public override Model.Ekspert Update(int id, EkspertUpsertRequest request)
        {
            var entity = _context.Eksperti.Find(id);
            entity.EkspertStrucnaKategorijaId = request.EkspertStrucnaKategorijaId;

            _context.SaveChanges();

            return _mapper.Map<Model.Ekspert>(entity);
        }
    }
}
