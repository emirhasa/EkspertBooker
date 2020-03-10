using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EkspertBooker.Model;
using EkspertBooker.Model.Requests;
using EkspertBooker.WebAPI.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EkspertBooker.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoslodavacPreporukeController : ControllerBase
    {
        private EkspertBookerContext _context;
        private IMapper _mapper;
        public PoslodavacPreporukeController(EkspertBookerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public ActionResult<List<EkspertPreporuka>> GetPreporuke(int id)
        {
            var projekt = _context.Projekti.Find(id);
            if (projekt == null) return NoContent();
            var eksperti = _context.Eksperti.Include(e=>e.Korisnik).Select(e => new Database.Ekspert
            {
                KorisnikId = e.KorisnikId,
                Korisnik = e.Korisnik,
                ProsjecnaOcjena = e.ProsjecnaOcjena,
                BrojRecenzija = e.BrojRecenzija,
                BrojZavrsenihProjekata = e.BrojZavrsenihProjekata,
                EkspertStrucnaKategorijaId = e.EkspertStrucnaKategorijaId
            }).ToList();

            List<EkspertPreporuka> rekomendacije = new List<EkspertPreporuka>();
            foreach (Database.Ekspert ekspert in eksperti)
            {
                EkspertPreporuka rekomendacija = new EkspertPreporuka
                {
                    Ekspert = _mapper.Map<Model.Ekspert>(ekspert),
                    Rejting = 5
                };
                if (ekspert.EkspertStrucnaKategorijaId != null)
                {
                    if (ekspert.EkspertStrucnaKategorijaId == projekt.KategorijaId)
                    {
                        rekomendacija.Rejting += 5;
                    }
                }

                var ekspert_zavrseni_projekti = ekspert.BrojZavrsenihProjekata;
                rekomendacija.Rejting = rekomendacija.Rejting + (ekspert_zavrseni_projekti * (decimal)2.5);
                var ekspert_zavrseni_projekti_bonus_kategorija = _context.Projekti.Where(p => p.StanjeId == "Zavrsen" && p.KategorijaId == ekspert.EkspertStrucnaKategorijaId && p.EkspertId == ekspert.KorisnikId).Count();

                if (ekspert_zavrseni_projekti_bonus_kategorija > 0)
                    rekomendacija.Rejting = rekomendacija.Rejting + (ekspert_zavrseni_projekti_bonus_kategorija * 2);

                if (ekspert.ProsjecnaOcjena != null)
                {
                    rekomendacija.Rejting = rekomendacija.Rejting + ((decimal)ekspert.ProsjecnaOcjena * 5);
                }

                rekomendacije.Add(rekomendacija);
            }

            return rekomendacije.OrderByDescending(r=>r.Rejting).Take(3).ToList();
        }
    }
}