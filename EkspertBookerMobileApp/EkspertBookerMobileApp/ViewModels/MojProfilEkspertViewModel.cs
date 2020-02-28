using EkspertBooker.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EkspertBookerMobileApp.ViewModels
{
    public class MojProfilEkspertViewModel : BaseViewModel
    {
        private readonly APIService _ekspertiService = new APIService("Eksperti");

        Korisnik _trenutniKorisnik;
        public Korisnik TrenutniKorisnik {
            get { return _trenutniKorisnik; }
            set { SetProperty(ref _trenutniKorisnik, value); }
        }

        Ekspert _trenutniEkspert;
        public Ekspert TrenutniEkspert
        {
            get {  return _trenutniEkspert; }
            set { SetProperty(ref _trenutniEkspert, value); }
        }
        public MojProfilEkspertViewModel()
        {
            InitCommand = new Command(async () => await Init());
        }

        public ICommand InitCommand { get; set; }

        public async Task Init()
        {
            TrenutniKorisnik = LoggedUser.logovaniKorisnik;
            TrenutniEkspert = await _ekspertiService.GetById<Ekspert>(TrenutniKorisnik.KorisnikId);
        }
    }
}
