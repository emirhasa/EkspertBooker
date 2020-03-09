using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EkspertBooker.WebAPI.Database
{
    public class NotifikacijaPoslodavac
    {
        public int NotifikacijaPoslodavacId { get; set; }
        public int PoslodavacId { get; set; }
        public Poslodavac Poslodavac { get; set; }
        public int EkspertId { get; set; }
        public Ekspert Ekspert { get; set; }
        public int ProjektId { get; set; }
        public Projekt Projekt { get; set; }
        public string Poruka { get; set; }
        public DateTime Vrijeme { get; set; }
    }
}
