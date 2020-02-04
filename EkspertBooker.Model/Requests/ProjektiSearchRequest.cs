using System;

namespace EkspertBooker.Model
{
    public class ProjektiSearchRequest
    {
        //public int ProjektId { get; set; }
        public string Naziv { get; set; }
        public bool? Hitan { get; set; } 
        public string StanjeId { get; set; }
        public int KategorijaId { get; set; }
  
    }
}
