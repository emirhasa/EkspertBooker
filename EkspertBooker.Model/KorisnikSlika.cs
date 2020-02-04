using System;
using System.Collections.Generic;
using System.Text;

namespace EkspertBooker.Model
{
    public class KorisnikSlika
    {
        public int KorisnikSlikaId { get; set; }
        public int KorisnikId { get; set; }
       // public Korisnik Korisnik { get; set; }
        public byte[] ProfilnaSlika { get; set; }
    }
}
