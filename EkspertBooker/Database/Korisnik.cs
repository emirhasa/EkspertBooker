using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EkspertBooker.WebAPI.Database
{
    public partial class Korisnik
    {
        public Korisnik()
        {
            KorisnikUloge = new HashSet<KorisnikUloga>();
            KorisnikKategorije = new HashSet<KorisnikKategorija>();
        }
        public int KorisnikId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string KorisnickoIme { get; set; }
        public string LozinkaHash { get; set; }
        public string LozinkaSalt { get; set; }
        public ICollection<KorisnikUloga> KorisnikUloge { get; set; }
        public ICollection<KorisnikKategorija> KorisnikKategorije { get; set; }
        public KorisnikSlika KorisnikSlika { get; set; }
    }
}
