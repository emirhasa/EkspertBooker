using EkspertBooker.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EkspertBookerMobileApp
{
    public static class LoggedUser
    {
        public static Korisnik logovaniKorisnik { get; set; }
        public static string Role { get; set; }
    }
}
