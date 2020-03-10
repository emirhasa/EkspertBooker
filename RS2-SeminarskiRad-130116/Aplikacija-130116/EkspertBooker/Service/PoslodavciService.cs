﻿using AutoMapper;
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
                    var result_alt = _mapper.Map<List<Model.Poslodavac>>(_context.Poslodavci.Include(p => p.KorisnikUloga).Include(p=>p.Korisnik).ThenInclude(k=>k.KorisnikSlika).Where(p=>p.BrojZavrsenihProjekata>=search.BrojZavrsenihProjekata).ToList());
                    return result_alt;
                }
            } 
            var result =  _mapper.Map<List<Model.Poslodavac>>(_context.Poslodavci.Include(p=>p.KorisnikUloga).Include(p=>p.Korisnik).ThenInclude(k => k.KorisnikSlika).ToList());
            return result;
        }

        public override Model.Poslodavac GetById(int id)
        {
            return _mapper.Map<Model.Poslodavac>(_context.Poslodavci.Include(p => p.KorisnikUloga.Korisnik).ThenInclude(k=>k.KorisnikSlika).First(p => p.KorisnikId == id));
        }

        public override Model.Poslodavac Update(int id, PoslodavacUpsertRequest request)
        {
            try
            {
                var entity = _context.Poslodavci.Find(id);
                entity.Notifikacije = request.Notifikacije;
                _context.SaveChanges();
                return _mapper.Map<Model.Poslodavac>(entity);
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}