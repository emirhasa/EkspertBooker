using EkspertBooker.Model;
using EkspertBooker.Model.Requests;
using EkspertBookerMobileApp.Validation;
using EkspertBookerMobileApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EkspertBookerMobileApp.ViewModels
{
    public class PonudaUpdateViewModel : BaseViewModel
    {
        int ponudaId;
        Projekt _projekt;
        Ponuda _ponuda;
        public Projekt Projekt
        {
            get { return _projekt; }
            set { SetProperty(ref _projekt, value); }
        }

        private readonly APIService _projektiService = new APIService("Projekti");
        private readonly APIService _ponudeService = new APIService("Ponude");

        string _title = "Uredi ponudu";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        string _naslov;
        public string Naslov
        {
            get { return _naslov; }
            set { SetProperty(ref _naslov, value); }
        }

        int? _cijena = null;
        public int? Cijena
        {
            get { return _cijena; }
            set { SetProperty(ref _cijena, value); }
        }

        string _opisPonude;
        public string OpisPonude
        {
            get { return _opisPonude; }
            set { SetProperty(ref _opisPonude, value); }
        }

        public PonudaUpdateViewModel(int _ponudaId)
        {
            ponudaId = _ponudaId;
            InitCommand = new Command(async () => await Init());
            SubmitCommand = new Command(async () => await Submit());
        }

        public ICommand InitCommand { get; set; }
        public ICommand SubmitCommand { get; set; }

        public async Task Init()
        {
            _ponuda = await _ponudeService.GetById<Ponuda>(ponudaId);
            Projekt = await _projektiService.GetById<Projekt>(_ponuda.ProjektId);
            Naslov = _ponuda.Naslov;
            OpisPonude = _ponuda.OpisPonude;
            Cijena = _ponuda.Cijena;
        }

        public async Task<bool> Submit()
        {
            if (IsValid())
            {
                PonudaUpsertRequest nova_ponuda = new PonudaUpsertRequest
                {
                    EkspertId = LoggedUser.logovaniKorisnik.KorisnikId,
                    Naslov = Naslov,
                    OpisPonude = OpisPonude,
                    Cijena = Cijena,
                    ProjektId = _ponuda.ProjektId,
                    VrijemePonude = DateTime.Now,
                    Status = _ponuda.Status
                };

                var result = _ponudeService.Update<Ponuda>(ponudaId, nova_ponuda);
                if (result != null)
                {
                    Application.Current.MainPage.DisplayAlert("Info", "Sačuvane promjene!", "OK");
                    return true;
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Greška", "Greška prilikom spremanja promjena!", "Dalje...");
                    return false;
                }
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Greška", "Unesite obavezna polja!", "Dalje...");
                return false;
            }
        }

        public bool ValidationTriggered = false;
        bool _naslovErrorVisible = false;
        bool _opisPonudeErrorVisible = false;
        bool _cijenaErrorVisible = false;

        public bool NaslovErrorVisible
        {
            get { return _naslovErrorVisible; }
            set { SetProperty(ref _naslovErrorVisible, value); }
        }

        public bool OpisPonudeErrorVisible
        {
            get { return _opisPonudeErrorVisible; }
            set { SetProperty(ref _opisPonudeErrorVisible, value); }
        }

        public bool CijenaErrorVisible
        {
            get { return _cijenaErrorVisible; }
            set { SetProperty(ref _cijenaErrorVisible, value); }
        }

        public bool IsValid()
        {
            bool pronadjena_greska = false;
            ValidationTriggered = true;
            if (!ValidationRules.IsNullOrEmptyRule(Naslov))
            {
                NaslovErrorVisible = true;
                pronadjena_greska = true;
            }

            if (!ValidationRules.IsNullOrEmptyRule(OpisPonude))
            {
                OpisPonudeErrorVisible = true;
                pronadjena_greska = true;
            }

            if(int.TryParse(Cijena.ToString(), out int cijena))
            {
                if(cijena < 10 || cijena > 500000)
                {
                    CijenaErrorVisible = true;
                    pronadjena_greska = true;
                }
            } else
            {
                CijenaErrorVisible = true;
                pronadjena_greska = true;
            }
            
            return !pronadjena_greska;

        }
    }
}
