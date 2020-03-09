using EkspertBooker.Model;
using EkspertBooker.Model.Requests;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EkspertBookerMobileApp.ViewModels
{
    public class MojProfilPoslodavacViewModel : BaseViewModel
    {
        private readonly APIService _korisniciService = new APIService("Korisnici");
        private readonly APIService _poslodavciService = new APIService("Poslodavci");
        Korisnik _trenutniKorisnik;
        public Korisnik TrenutniKorisnik
        {
            get { return _trenutniKorisnik; }
            set { SetProperty(ref _trenutniKorisnik, value); }
        }
        Poslodavac _trenutniPoslodavac;

        public Poslodavac TrenutniPoslodavac
        {
            get { return _trenutniPoslodavac; }
            set { SetProperty(ref _trenutniPoslodavac, value); }
        }

        public MojProfilPoslodavacViewModel()
        {
            InitCommand = new Command(async () => await Init());
        }

        public ICommand InitCommand { get; set; }

        public async Task Init()
        {
            TrenutniKorisnik = await _korisniciService.GetById<Korisnik>(LoggedUser.logovaniKorisnik.KorisnikId);
            TrenutniPoslodavac = await _poslodavciService.GetById<Poslodavac>(LoggedUser.logovaniKorisnik.KorisnikId);
        }

        string _urediIme = null;
        public string UrediIme
        {
            get { return _urediIme; }
            set { SetProperty(ref _urediIme, value); }
        }

        string _urediPrezime = null;
        public string UrediPrezime
        {
            get { return _urediPrezime; }
            set { SetProperty(ref _urediPrezime, value); }
        }

        string _urediEmail = null;
        public string UrediEmail
        {
            get { return _urediEmail; }
            set { SetProperty(ref _urediEmail, value); }
        }

        string _urediTelefon = null;
        public string UrediTelefon
        {
            get { return _urediTelefon; }
            set { SetProperty(ref _urediTelefon, value); }
        }

        public async Task<bool> SacuvajPromjene()
        {
            if (IsValid())
            {
                KorisnikUpsertRequest request = new KorisnikUpsertRequest
                {
                    Ime = TrenutniKorisnik.Ime,
                    Prezime = TrenutniKorisnik.Prezime,
                    Email = TrenutniKorisnik.Email,
                    Telefon = TrenutniKorisnik.Telefon,
                    KorisnickoIme = TrenutniKorisnik.KorisnickoIme
                };

                if (!string.IsNullOrWhiteSpace(UrediIme))
                {
                    request.Ime = UrediIme;
                }
                if (!string.IsNullOrWhiteSpace(UrediPrezime))
                {
                    request.Prezime = UrediPrezime;
                }
                if (!string.IsNullOrWhiteSpace(UrediEmail))
                {
                    request.Email = UrediEmail;
                }
                if (!string.IsNullOrWhiteSpace(UrediTelefon))
                {
                    request.Telefon = UrediTelefon;
                }

                try
                {
                    var result = await _korisniciService.Update<Korisnik>(TrenutniKorisnik.KorisnikId, request);
                    if (result != null) return true;
                }
                catch (Exception ex)
                {
                    var exception = ex;
                    Application.Current.MainPage.DisplayAlert("Greška", ex.Message, "OK");
                    return false;
                }
            }
            return false;
        }

        bool _urediFormErrorVisible;
        public bool UrediFormErrorVisible
        {
            get { return _urediFormErrorVisible; }
            set { SetProperty(ref _urediFormErrorVisible, value); }
        }

        string _urediErrorLabelText;
        public string UrediErrorLabelText
        {
            get { return _urediErrorLabelText; }
            set { SetProperty(ref _urediErrorLabelText, value); }
        }

        bool _validationTriggered = false;
        public bool ValidationTriggered
        {
            get { return _validationTriggered; }
            set { SetProperty(ref _validationTriggered, value); }
        }

        public bool IsValid()
        {
            ValidationTriggered = true;
            //validate fields
            //only one input change is required -> ime, prezime, email or telefon
            if (!string.IsNullOrWhiteSpace(UrediIme) || !string.IsNullOrWhiteSpace(UrediPrezime) ||
                !string.IsNullOrWhiteSpace(UrediEmail) || !string.IsNullOrWhiteSpace(UrediTelefon))
            {
                if (!string.IsNullOrWhiteSpace(UrediEmail))
                {
                    try
                    {
                        MailAddress provjera = new MailAddress(UrediEmail);
                        UrediFormErrorVisible = false;
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
                UrediFormErrorVisible = false;
                return true;
            }
            UrediFormErrorVisible = true;
            return false;
        }

    }
}
