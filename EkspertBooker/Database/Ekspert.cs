using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EkspertBooker.WebAPI.Database
{
    public class Ekspert
    {
        public Ekspert()
        {
            Projekti = new HashSet<Projekt>();
            RecenzijeOEksperti = new HashSet<RecenzijaOEkspert>();
            RecenzijeOPoslodavci = new HashSet<RecenzijaOPoslodavac>();
            Ponude = new HashSet<Ponuda>();
        }
        public int EkspertId { get; set; }
        public int KorisnikUlogaId { get; set; }
        public KorisnikUloga KorisnikUloga { get; set; }
        public float? ProsjecnaOcjena { get; set; }
        public int BrojZavrsenihProjekata { get; set; }

        public int BrojRecenzija { get; set; } // broj recenzija koje je dobio, ne pisao

        public ICollection<Projekt> Projekti { get; set; }
        public ICollection<RecenzijaOEkspert> RecenzijeOEksperti { get; set; }
        public ICollection<RecenzijaOPoslodavac> RecenzijeOPoslodavci { get; set; }

        public ICollection<Ponuda> Ponude { get; set; }

        public Korisnik Korisnik { get; set; }

    }
}
