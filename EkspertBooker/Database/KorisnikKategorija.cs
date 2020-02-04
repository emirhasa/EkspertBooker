using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EkspertBooker.WebAPI.Database
{
    public class KorisnikKategorija
    {
        public int KorisnikKategorijaId { get; set; }
        public Korisnik Korisnik { get; set; }
        public int KorisnikId { get; set; }
        public Kategorija Kategorija { get; set; }
        public int KategorijaId { get; set; }
    }
}
