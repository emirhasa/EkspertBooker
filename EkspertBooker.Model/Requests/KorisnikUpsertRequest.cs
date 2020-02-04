using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EkspertBooker.Model.Requests
{
    public class KorisnikUpsertRequest
    {
        [Required]
        public string Ime { get; set; }
        [Required]
        public string Prezime { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Telefon { get; set; }

        [Required]
        [MinLength(4)]
        public string KorisnickoIme { get; set; }

        public byte[] Slika { get; set; }

        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public List<Uloga> Uloge { get; set; } = new List<Uloga>();
    }
}
