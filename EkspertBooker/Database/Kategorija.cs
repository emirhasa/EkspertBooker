using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EkspertBooker.WebAPI.Database
{
    public class Kategorija
    {
        public Kategorija()
        {
            Projekti = new HashSet<Projekt>();
            //lista korisnika na kategoriji - odnosi se na pretplate - vjerovatno treba ukinuti odnosno modifikovati tako da su to ekspertikategorije
            //KategorijaKorisnici = new HashSet<KorisnikKategorija>();
            EkspertiKategorijaStrucnosti = new HashSet<Ekspert>();
            EkspertiKategorijaPretplate = new HashSet<EkspertKategorijaPretplata>();
        }

        public int KategorijaId { get; set; }
        public string Naziv { get; set; }

        public ICollection<Projekt> Projekti { get; set; }
        //public ICollection<KorisnikKategorija> KategorijaKorisnici { get; set; }
        public ICollection<Ekspert> EkspertiKategorijaStrucnosti { get; set; }
        public ICollection<EkspertKategorijaPretplata> EkspertiKategorijaPretplate { get; set; }
    }
}