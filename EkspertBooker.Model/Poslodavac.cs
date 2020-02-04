namespace EkspertBooker.Model
{
    public class Poslodavac
    {
        public int PoslodavacId { get; set; }
        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }
        public int KorisnikUlogaId { get; set; }
        public KorisnikUloga KorisnikUloga { get; set; }
        public float ProsjecnaOcjena { get; set; }
        public int BrojZavrsenihProjekata { get; set; }
    }
}