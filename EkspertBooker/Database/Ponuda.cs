﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EkspertBooker.WebAPI.Database
{
    public class Ponuda
    {
        public int PonudaId { get; set; }

        public int EkspertId { get; set; }
        public Ekspert Ekspert { get; set; }

        public int ProjektId { get; set; }
        public Projekt Projekt { get; set; }

        public string Naslov { get; set; }
        public string OpisPonude { get; set; }

        public int? Cijena { get; set; }
        public DateTime VrijemePonude { get; set; }

        public DateTime? VrijemePrihvatanja { get; set; }
        public DateTime? VrijemeOdbijanja { get; set; }
        public string PoslodavacKomentar { get; set; }

        //0 - odbijena, 1 - aktivna, 2 - prihvacena
        public int Status { get; set; }

    }
}
