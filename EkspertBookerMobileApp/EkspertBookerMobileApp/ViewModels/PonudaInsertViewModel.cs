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
    public class PonudaInsertViewModel : BaseViewModel
    {

        int projektId;

        Projekt _projekt;
        public Projekt Projekt
        {
            get { return _projekt; }
            set { SetProperty(ref _projekt, value); }
        }
        private readonly APIService _projektiService = new APIService("Projekti");
        private readonly APIService _ponudeService = new APIService("Ponude");

        string _title = "Nova ponuda";
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

        public PonudaInsertViewModel(int _projektId)
        {
            projektId = _projektId;
            InitCommand = new Command(async () => await Init());
            SubmitCommand = new Command(async () => await Submit());
        }

        public ICommand InitCommand { get; set; }
        public ICommand SubmitCommand { get; set; }

        public async Task Init()
        {
            Projekt = await _projektiService.GetById<Projekt>(projektId);
        }

        public async Task Submit()
        {
            if (IsValid())
            {
                PonudaUpsertRequest nova_ponuda = new PonudaUpsertRequest
                {
                    EkspertId = LoggedUser.logovaniKorisnik.KorisnikId,
                    Naslov = Naslov,
                    OpisPonude = OpisPonude,
                    Cijena = Cijena,
                    ProjektId = projektId,
                    Status = 1,
                    VrijemePonude = DateTime.Now
                };

                var result = _ponudeService.Insert<Ponuda>(nova_ponuda);
                if (result != null)
                {
                    Application.Current.MainPage.DisplayAlert("Info", "Ponuda uspješno dodana!", "OK");
                    Application.Current.MainPage = new EkspertProjektDetailPage(projektId);
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Greška", "Greška prilikom dodavanja ponude!", "Dalje...");
                }
            } else
            {
                Application.Current.MainPage.DisplayAlert("Greška", "Unesite obavezna polja!", "Dalje...");
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
            if(!ValidationRules.IsNullOrEmptyRule(Naslov))
            {
                NaslovErrorVisible = true;
                pronadjena_greska = true;
            }

            if(!ValidationRules.IsNullOrEmptyRule(OpisPonude))
            {
                OpisPonudeErrorVisible = true;
                pronadjena_greska = true;
            }

            if (!ValidationRules.IsNullOrEmptyRule(Cijena.ToString()))
            {
                CijenaErrorVisible = true;
                pronadjena_greska = true;
            }

            if (int.TryParse(Cijena.ToString(), out int cijena))
            {
                if (cijena < 10 || cijena > 500000)
                {
                    CijenaErrorVisible = true;
                    pronadjena_greska = true;
                }
            }
            else
            {
                CijenaErrorVisible = true;
                pronadjena_greska = true;
            }

            return !pronadjena_greska;

        }

    }
}
