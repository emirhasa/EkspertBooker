using System;
using System.Collections.Generic;
using System.Text;

namespace EkspertBookerMobileApp.Models
{
    public enum PoslodavacMenuItemType
    {
        MojiProjekti,
        Eksperti,
        NoviProjekat,
        MojProfil,
        Postavke,
        Logout
    }
    public class PoslodavacMenuItem
    {
        public PoslodavacMenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
