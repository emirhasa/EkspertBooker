using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EkspertBooker.WebAPI.Database
{
    public class Poslodavac
    {
        public Poslodavac()
        {
            Projekti = new HashSet<Projekt>();
            RecenzijeOEksperti = new HashSet<RecenzijaOEkspert>();
            RecenzijeOPoslodavci = new HashSet<RecenzijaOPoslodavac>();
        }
        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }
        public int? KorisnikUlogaId { get; set; }
        public KorisnikUloga KorisnikUloga { get; set; }
        public float? ProsjecnaOcjena { get; set; }
        public int BrojZavrsenihProjekata { get; set; }
        public int BrojRecenzija { get; set; } // broj recenzija koje je dobio, ne pisao

        public ICollection<Projekt> Projekti { get; set; }
        public ICollection<RecenzijaOEkspert> RecenzijeOEksperti { get; set; }
        public ICollection<RecenzijaOPoslodavac> RecenzijeOPoslodavci { get; set; }
    }
}
