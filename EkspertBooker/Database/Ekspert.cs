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
            EkspertKategorijePretplate = new HashSet<EkspertKategorijaPretplata>();
            NotifikacijePoslodavci = new HashSet<NotifikacijaPoslodavac>();
            NotifikacijeEkspert = new HashSet<NotifikacijaEkspert>();
        }
        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }
        public int? KorisnikUlogaId { get; set; }
        public KorisnikUloga KorisnikUloga { get; set; }
        public decimal? ProsjecnaOcjena { get; set; }
        public int BrojZavrsenihProjekata { get; set; }
        public int? EkspertStrucnaKategorijaId { get; set; }
        public Kategorija EkspertStrucnaKategorija { get; set; }

        public int BrojRecenzija { get; set; } // broj recenzija koje je dobio, ne pisao

        public ICollection<Projekt> Projekti { get; set; }
        public ICollection<RecenzijaOEkspert> RecenzijeOEksperti { get; set; }
        public ICollection<RecenzijaOPoslodavac> RecenzijeOPoslodavci { get; set; }

        public ICollection<Ponuda> Ponude { get; set; }
        public ICollection<EkspertKategorijaPretplata> EkspertKategorijePretplate { get; set; }
        public bool? Notifikacije { get; set; }

        //za mapiranje relacije -> prilikom notifikacije poslodavaca o dobijanju nove ponude od eksperta
        public ICollection<NotifikacijaPoslodavac> NotifikacijePoslodavci { get; set; }

        //kolekcija notifikacija za datog eksperta
        public ICollection<NotifikacijaEkspert> NotifikacijeEkspert { get; set; }
    }
}
