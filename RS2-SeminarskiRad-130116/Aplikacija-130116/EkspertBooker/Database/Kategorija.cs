using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EkspertBooker.WebAPI.Database
{
    public class Kategorija
    {
        public Kategorija()
        {
            Projekti = new HashSet<Projekt>();

            EkspertiKategorijaStrucnosti = new HashSet<Ekspert>();
            EkspertiKategorijaPretplate = new HashSet<EkspertKategorijaPretplata>();
        }

        public int KategorijaId { get; set; }
        public string Naziv { get; set; }

        public ICollection<Projekt> Projekti { get; set; }

        public ICollection<Ekspert> EkspertiKategorijaStrucnosti { get; set; }
        public ICollection<EkspertKategorijaPretplata> EkspertiKategorijaPretplate { get; set; }
    }
}