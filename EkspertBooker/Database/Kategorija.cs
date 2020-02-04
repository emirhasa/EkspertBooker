using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EkspertBooker.WebAPI.Database
{
    public class Kategorija
    {
        public Kategorija()
        {
            Projekti = new HashSet<Projekt>();
            //lista korisnika na kategoriji
            KategorijaKorisnici = new HashSet<KorisnikKategorija>();
        }

        public int KategorijaId { get; set; }
        public string Naziv { get; set; }

        public ICollection<Projekt> Projekti { get; set; }
        public ICollection<KorisnikKategorija> KategorijaKorisnici { get; set; }
    }
}