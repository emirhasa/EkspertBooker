using System;
using System.Collections.Generic;
using System.Text;

namespace EkspertBookerMobileApp.Models
{
    public enum EkspertMenuItemType
    {
        MojePonude,
        Projekti,
        Poslodavci,
        MojProfil,
        Postavke,
        Logout
    }
    public class EkspertMenuItem
    {
        public EkspertMenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
