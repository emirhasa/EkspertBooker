﻿namespace EkspertBooker.Model
{
    public class ProjektDetalji
    {
        public int ProjektDetaljiId { get; set; }
        public int ProjektId { get; set; }
        public string AktivanDetaljanOpis { get; set; }
        public string Napomena { get; set; }
    }
}