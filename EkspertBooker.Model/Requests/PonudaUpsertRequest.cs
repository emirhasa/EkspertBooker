using System;
using System.Collections.Generic;
using System.Text;

namespace EkspertBooker.Model.Requests
{
    public class PonudaUpsertRequest
    {
        public int EkspertId { get; set; }

        public int ProjektId { get; set; }

        public string OpisPonude { get; set; }

        public DateTime? VrijemePonude { get; set; }
        public int? Cijena { get; set; }
        public bool Status { get; set; }
    }
}
