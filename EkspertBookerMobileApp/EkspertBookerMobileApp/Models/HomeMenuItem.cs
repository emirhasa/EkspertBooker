using System;
using System.Collections.Generic;
using System.Text;

namespace EkspertBookerMobileApp.Models
{
    public enum MenuItemType
    {
        Browse,
        About,
        Projekti,
        Eksperti,
        Poslodavci
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
