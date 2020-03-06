using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EkspertBooker.WebAPI.Database
{
    public class EkspertKategorijaPretplata
    {
        public int EkspertKategorijaPretplataId { get; set;}

        public int EkspertId { get; set; }
        public Ekspert Ekspert { get; set; }

        public int KategorijaId { get; set; }
        public Kategorija Kategorija { get; set; }

    }
}
