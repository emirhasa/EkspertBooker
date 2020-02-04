namespace EkspertBooker.WebAPI.Database
{
    public class ProjektDetaljiPrilog
    {
        public int ProjektDetaljiPrilogId { get; set; }
        public int ProjektDetaljiId { get; set; }
        public ProjektDetalji ProjektDetalji { get; set; }
        public string PrilogNaziv { get; set; }
        public byte[] Prilog { get; set; }
    }
}