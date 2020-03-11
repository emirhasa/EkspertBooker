using EkspertBooker.Model;
using EkspertBooker.Model.Requests;
using EkspertBookerMobileApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using EkspertBookerMobileApp.Validation;


namespace EkspertBookerMobileApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public ObservableCollection<Uloga> UserRolesList { get; set; } = new ObservableCollection<Uloga>();
        private APIService KorisniciService = new APIService("Korisnici");
        private APIService PoslodavciService = new APIService("Poslodavci");
        private APIService EkspertiService = new APIService("Eksperti");

        public bool _validationTriggered = false;
        public bool _usernameErrorVisible = false;
        public bool _passwordErrorVisible = false;

        public bool UsernameErrorVisible
        {
            get { return _usernameErrorVisible; }
            set { SetProperty(ref _usernameErrorVisible, value); }
        }

        public bool PasswordErrorVisible
        {
            get { return _passwordErrorVisible; }
            set { SetProperty(ref _passwordErrorVisible, value); }
        }


        string _username = string.Empty;
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        string _password = string.Empty;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        Uloga _selectedUloga = null;
        public Uloga SelectedUloga
        {
            get { return _selectedUloga; }
            set
            {
                SetProperty(ref _selectedUloga, value);
            }
        }

        public ICommand LoginCommand { get; set; }
        public LoginViewModel()
        {
            UserRolesList.Add(new Uloga
            {
                UlogaId = 2,
                Naziv = "Poslodavac"
            });
            UserRolesList.Add(new Uloga
            {
                UlogaId = 3,
                Naziv = "Ekspert"
            });
            LoginCommand = new Command(async () => await Login());
        }

        async Task Login()      
        {
            _validationTriggered = true;
            if (IsValid())
            {
                IsBusy = true;
                APIService.Username = Username;
                APIService.Password = Password;
                //pokusaj kao ekspert ili kao poslodavac
                try
                {
                    var korisnici = await KorisniciService.Get<List<Korisnik>>(new KorisniciSearchRequest { Username = APIService.Username });
                    Korisnik korisnik;
                    if (korisnici.Count > 0)
                    {
                        korisnik = korisnici[0];
                        if (korisnik == null) await Application.Current.MainPage.DisplayAlert("Greška", "Pogrešan username/lozinka", "0K");
                        else
                        {
                            LoggedUser.logovaniKorisnik = korisnik;
                            foreach (var uloga in korisnik.KorisnikUloge)
                            {
                                if(uloga.UlogaId == 1)
                                {
                                    //administrator
                                    LoggedUser.Role = "Poslodavac";
                                    Application.Current.MainPage = new PoslodavacMainPage();
                                    await Application.Current.MainPage.DisplayAlert("Dobrodošli", "Dobrodošao, administrator!", "OK");
                                    return;
                                }
                                if (uloga.UlogaId == 2)
                                {
                                    //to je poslodavac
                                    LoggedUser.Role = "Poslodavac";
                                    Application.Current.MainPage = new PoslodavacMainPage();
                                    return;
                                    //LoggedUser.logovaniPoslodavac = await PoslodavciService.Get<List<Poslodavac>>(new PoslodavciSearchRequest { })
                                }
                                else if (uloga.UlogaId == 3)
                                {
                                    //to je ekspert
                                    LoggedUser.Role = "Ekspert";
                                    Application.Current.MainPage = new EkspertMainPage();
                                    return;
                                }
                                else await Application.Current.MainPage.DisplayAlert("Greška", "Pogrešan username/lozinka", "0K");
                            }
                        }
                    }
                }
                catch
                {
                    //await Application.Current.MainPage.DisplayAlert("Greška", "Pogrešan username/lozinka", "0K");
                }
            } else
            {
                await Application.Current.MainPage.DisplayAlert("Greška", "Unesite obavezna polja", "0K");
            }
        }

        bool IsValid()
        {
            if (ValidationRules.IsNullOrEmptyRule(_username) && ValidationRules.IsNullOrEmptyRule(_password)) return true;
            else
            {
                if(!ValidationRules.IsNullOrEmptyRule(_username))
                {
                    UsernameErrorVisible = true;
                }
                if(!ValidationRules.IsNullOrEmptyRule(_password))
                {
                    PasswordErrorVisible = true;
                }
                return false;
            }
            
        }


    }
}
