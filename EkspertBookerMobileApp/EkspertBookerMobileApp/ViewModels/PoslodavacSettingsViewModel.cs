using EkspertBooker.Model;
using EkspertBooker.Model.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EkspertBookerMobileApp.ViewModels
{
    public class PoslodavacSettingsViewModel : BaseViewModel
    {
        private readonly APIService _poslodavciService = new APIService("Poslodavci");
        bool _notifikacije;
        public bool Notifikacije
        {
            get { return _notifikacije; }
            set { SetProperty(ref _notifikacije, value); }
        }

        public PoslodavacSettingsViewModel()
        {
            InitCommand = new Command(async () => await Init());
        }

        public ICommand InitCommand { get; set; }

        public async Task<bool> Init()
        {
            try
            {
                var poslodavac = await _poslodavciService.GetById<Poslodavac>(LoggedUser.logovaniKorisnik.KorisnikId);
                if (poslodavac == null) return false;
                if (poslodavac.Notifikacije == true)
                {
                    Notifikacije = true;
                }
                else Notifikacije = false;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task UpdateSettings()
        {
            try
            {
                await _poslodavciService.Update<Poslodavac>(LoggedUser.logovaniKorisnik.KorisnikId, new PoslodavacUpsertRequest
                {
                    Notifikacije = Notifikacije
                });
                Application.Current.MainPage.DisplayAlert("Info", "Promjene sačuvane", "OK");
            } 
            catch
            {
                Application.Current.MainPage.DisplayAlert("Greška", "Problem prilikom mijenjanja postavki...", "OK");
            }
        }
    }
}
