﻿using System.Collections.Generic;

namespace EkspertBooker.WebAPI.Database
{
    public class Uloga
    {
        public Uloga()
        {
            //predstavlja listu korisnika za pojedinu ulogu
            KorisniciUloga = new HashSet<KorisnikUloga>();
        }
        public int UlogaId { get; set; }
        public string Naziv { get; set; }

        public ICollection<KorisnikUloga> KorisniciUloga { get; set; }
    }
}