using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EkspertBooker.WebAPI.Database
{
    public class KorisnikSlika
    {
        public int KorisnikSlikaId { get; set; }
        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }
        public byte[] ProfilnaSlika { get; set; }
        public string SlikaNaziv { get; set; }
    }
}
