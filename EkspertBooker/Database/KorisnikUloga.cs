namespace EkspertBooker.WebAPI.Database
{
    public class KorisnikUloga
    {
        public int KorisnikUlogaId { get; set; }
        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }
        public int UlogaId { get; set; }
        public Uloga Uloga { get; set; }
        public Ekspert Ekspert { get; set; }
        public Poslodavac Poslodavac { get; set; }
    }
}