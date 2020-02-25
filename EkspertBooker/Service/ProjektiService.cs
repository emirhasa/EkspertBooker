using AutoMapper;
using EkspertBooker.WebAPI.Database;
using EkspertBooker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EkspertBooker.WebAPI.Service
{
    public class ProjektiService : BaseCRUDService<Model.Projekt, ProjektiSearchRequest, ProjektUpsertRequest, ProjektUpsertRequest, Database.Projekt>
    {
        public ProjektiService(EkspertBookerContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override List<Model.Projekt> Get(ProjektiSearchRequest request)
        {
            var query = _context.Projekti.AsQueryable();

            if(request.KategorijaId > 0)
            {
                query = query.Where(p => p.KategorijaId == request.KategorijaId);
            }

            if(!string.IsNullOrWhiteSpace(request?.Naziv))
            {
                query = query.Where(p => p.Naziv.Contains(request.Naziv));
            }

            if(!string.IsNullOrWhiteSpace(request?.StanjeId))
            {
                query = query.Where(p=>p.StanjeId == request.StanjeId);
            }

            if(request.Hitan != null)
            {
                query = query.Where(p => p.Hitan == request.Hitan);
            }

            if(request.PoslodavacId != null)
            {
                query = query.Where(p => p.PoslodavacId == request.PoslodavacId);
            }

            if(request.EkspertId != null)
            {
                query = query.Where(p => p.EkspertId == request.EkspertId);
            }

            var result = query.Include(p=>p.Kategorija).Include(p=>p.ProjektDetalji).Include(p=>p.Poslodavac).ThenInclude(p=>p.Korisnik).ToList();
            return _mapper.Map<List<Model.Projekt>>(result);
        }

        public override Model.Projekt GetById(int id)
        {
            return _mapper.Map<Model.Projekt>(_context.Projekti.Include(p => p.ProjektDetalji).Include(p => p.Poslodavac).Where(p=>p.ProjektId == id).SingleOrDefault());
        }
    }
}
