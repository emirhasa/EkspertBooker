using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EkspertBooker.WebAPI.Database
{
    public class PoslodavacKategorija
    {
        public int PoslodavacKategorijaId {get; set;}

        public int PoslodavacId { get; set; }
        public Poslodavac Poslodavac { get; set; }

        public int KategorijaId { get; set; }
        public Kategorija Kategorija { get; set; }

    }
}
