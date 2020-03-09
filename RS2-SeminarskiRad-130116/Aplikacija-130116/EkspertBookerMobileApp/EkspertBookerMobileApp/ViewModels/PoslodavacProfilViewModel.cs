using EkspertBooker.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EkspertBookerMobileApp.ViewModels
{
    public class PoslodavacProfiLViewModel : BaseViewModel
    {
        private readonly APIService _poslodavciService = new APIService("Poslodavci");

        int _korisnikId;
        Poslodavac _poslodavac;
        public Poslodavac Poslodavac
        {
            get { return _poslodavac; }
            set { SetProperty(ref _poslodavac, value); }
        }

        public PoslodavacProfiLViewModel(int korisnikId) {
            _korisnikId = korisnikId;
            InitCommand = new Command(async () => await Init());
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
            //try - catch ? if Poslodavac == null? throw or pop page
            Poslodavac = await _poslodavciService.GetById<Poslodavac>(_korisnikId);

            if (Poslodavac.Korisnik.KorisnikSlika != null)
            {
                SlikaVisible = true;
            }
            else
            {
                SlikaVisible = false;
            }
        }
    }
}
