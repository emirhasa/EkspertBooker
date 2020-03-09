using EkspertBooker.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EkspertBookerMobileApp.ViewModels
{
    public class EkspertProfilViewModel : BaseViewModel
    {
        private readonly APIService _ekspertiService = new APIService("Eksperti");
        private readonly APIService _korisniciService = new APIService("Korisnici");

        int _korisnikId;
        //Korisnik _korisnik;
        //public Korisnik Korisnik
        //{
        //    get { return _korisnik; }
        //    set { SetProperty(ref _korisnik, value); }
        //}

        Ekspert _ekspert;
        public Ekspert Ekspert
        {
            get { return _ekspert; }
            set { SetProperty(ref _ekspert, value); }
        }

        public EkspertProfilViewModel(int korisnikId) {
            _korisnikId = korisnikId;
            InitCommand = new Command(async () => await Init());
        }

        bool _noStrucnaKategorija = true;
        public bool NoStrucnaKategorija
        {
            get { return _noStrucnaKategorija; }
            set { SetProperty(ref _noStrucnaKategorija, value); }
        }

        bool _slikaVisible = false;
        public bool SlikaVisible
        {
            get { return _slikaVisible; }
            set { SetProperty(ref _slikaVisible, value); }
        }

        public ICommand InitCommand { get; set; }

        public async Task Init()
        {
            //try - catch ? if korisnik == null? throw or pop page
            
            //Korisnik = await _korisniciService.GetById<Korisnik>(_korisnikId);
            Ekspert = await _ekspertiService.GetById<Ekspert>(_korisnikId);
            if (Ekspert.EkspertStrucnaKategorija != null)
            {
                NoStrucnaKategorija = false;
            }
            if (Ekspert.Korisnik.KorisnikSlika != null)
            {
                SlikaVisible = true;
            } else
            {
                SlikaVisible = false;
            }
        }
    }
}
