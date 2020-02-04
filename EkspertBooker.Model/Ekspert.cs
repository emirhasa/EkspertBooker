﻿namespace EkspertBooker.Model
{
    public class Ekspert
    {
        public int EkspertId { get; set; }
        public int KorisnikId { get; set; }
        public int KorisnikUlogaId { get; set; }
        public KorisnikUloga KorisnikUloga { get; set; }
        public Korisnik Korisnik { get; set; }
        public float ProsjecnaOcjena { get; set; }
        public int BrojZavrsenihProjekata { get; set; }

    }
}