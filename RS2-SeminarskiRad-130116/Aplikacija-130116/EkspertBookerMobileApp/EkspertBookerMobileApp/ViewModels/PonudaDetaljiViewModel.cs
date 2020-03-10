﻿using EkspertBooker.Model;
using EkspertBooker.Model.Requests;
using EkspertBookerMobileApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EkspertBookerMobileApp.ViewModels
{
    public class PonudaDetaljiViewModel : BaseViewModel
    {
        private readonly APIService _ponudeService = new APIService("Ponude");
        int ponudaId;

        Ponuda _ponuda;
        public Ponuda Ponuda
        {
            get { return _ponuda; }
            set { SetProperty(ref _ponuda, value); }
        }
        public PonudaDetaljiViewModel(int _ponudaId)
        {
            ponudaId = _ponudaId;
            InitCommand = new Command(async () => await Init());
        }

        public ICommand InitCommand { get; set; }

        public async Task Init()
        {
            Ponuda = await _ponudeService.GetById<Ponuda>(ponudaId);
            if (Ponuda == null)
            {
                Application.Current.MainPage.DisplayAlert("Greška", "Greška u učitavanju ponude", "Dalje...");
                Application.Current.MainPage = new EkspertMainPage();
            }
            if (Ponuda.Status == 0)
            {
                PonudaStanjeText = "ODBIJENA";
                PonudaStanjeColor = Color.Red;
                PoslodavacAkcijaText = "Vrijeme odbijanja:";
                VrijemePoslodavacAkcije = Ponuda.VrijemeOdbijanja;
                PoslodavacAkcijaVrijemeVisible = true;
                if(!string.IsNullOrWhiteSpace(Ponuda.PoslodavacKomentar))
                {
                    PoslodavacKomentarVisible = true;
                }
            }
            if (Ponuda.Status == 1)
            {
                if (LoggedUser.Role == "Poslodavac")
                FormPrihvatiVisible = true;
                PonudaStanjeText = "AKTIVNA";
                PonudaStanjeColor = Color.Orange;
                PoslodavacAkcijaVrijemeVisible = false;
            }
            if (Ponuda.Status == 2)
            {
                PonudaStanjeText = "PRIHVAĆENA";
                PonudaStanjeColor = Color.ForestGreen;
                PoslodavacAkcijaText = "Vrijeme prihvatanja:";
                VrijemePoslodavacAkcije = Ponuda.VrijemePrihvatanja;
                PoslodavacAkcijaVrijemeVisible = true;
                if (!string.IsNullOrWhiteSpace(Ponuda.PoslodavacKomentar))
                {
                    PoslodavacKomentarVisible = true;
                }
            }

        }

        bool _poslodavacKomentarVisible = false;
        public bool PoslodavacKomentarVisible
        {
            get { return _poslodavacKomentarVisible; }
            set { SetProperty(ref _poslodavacKomentarVisible, value); }
        }

        Color _ponudaStanjeColor;
        public Color PonudaStanjeColor {
            get { return _ponudaStanjeColor; }
            set { SetProperty(ref _ponudaStanjeColor, value); }
        }

        string _poslodavacKomentar = null;
        public string PoslodavacKomentar
        {
            get { return _poslodavacKomentar; }
            set { SetProperty(ref _poslodavacKomentar, value); }
        }

        bool _formPrihvatiVisible = false;
        public bool FormPrihvatiVisible
        {
            get { return _formPrihvatiVisible; }
            set { SetProperty(ref _formPrihvatiVisible, value); }
        }

        string _ponudaStanjeText;
        public string PonudaStanjeText
        {
            get { return _ponudaStanjeText; }
            set { SetProperty(ref _ponudaStanjeText, value); }
        }

        string _poslodavacAkcijaText;
        public string PoslodavacAkcijaText
        {
            get { return _poslodavacAkcijaText; }
            set { SetProperty(ref _poslodavacAkcijaText, value); }
        }

        DateTime? _vrijemePoslodavacAkcije;
        public DateTime? VrijemePoslodavacAkcije
        {
            get { return _vrijemePoslodavacAkcije; }
            set { SetProperty(ref _vrijemePoslodavacAkcije, value); }
        }

        bool _poslodavacAkcijaVrijemeVisible = false;
        public bool PoslodavacAkcijaVrijemeVisible
        {
            get { return _poslodavacAkcijaVrijemeVisible; }
            set { SetProperty(ref _poslodavacAkcijaVrijemeVisible, value); }
        }

        public async Task PotvrdiPonudu()
        {
            PonudaUpsertRequest request = new PonudaUpsertRequest
            {
                Cijena = Ponuda.Cijena,
                EkspertId = Ponuda.EkspertId,
                Naslov = Ponuda.Naslov,
                VrijemePonude = Ponuda.VrijemePonude,
                VrijemePrihvatanja = DateTime.Now,
                OpisPonude = Ponuda.OpisPonude,
                PoslodavacKomentar = PoslodavacKomentar,
                ProjektId = Ponuda.ProjektId,
                Status = 2
            };
            try
            {
                var result = _ponudeService.Update<Ponuda>(Ponuda.PonudaId, request);
                if (result != null)
                {
                    Application.Current.MainPage.DisplayAlert("Info", "Ponuda uspješno PRIHVAĆENA!", "Super :)");
                }
            } 
            catch
            {
                Application.Current.MainPage.DisplayAlert("Greška", "Greška prilikom prihvatanja ponude...!", "Dalje :/ ...");
            }
        }

        public async Task OdbijPonudu()
        {
            PonudaUpsertRequest request = new PonudaUpsertRequest
            {
                Cijena = Ponuda.Cijena,
                EkspertId = Ponuda.EkspertId,
                Naslov = Ponuda.Naslov,
                VrijemePonude = Ponuda.VrijemePonude,
                VrijemeOdbijanja = DateTime.Now,
                OpisPonude = Ponuda.OpisPonude,
                ProjektId = Ponuda.ProjektId,
                PoslodavacKomentar = PoslodavacKomentar,
                Status = 0
            };
            try
            {
                var result = _ponudeService.Update<Ponuda>(Ponuda.PonudaId, request);
                if (result != null)
                {
                    Application.Current.MainPage.DisplayAlert("Info", "Ponuda uspješno ODBIJENA!", "Dalje...");
                }
            }
            catch
            {
                Application.Current.MainPage.DisplayAlert("Greška", "Greška prilikom prihvatanja ponude...!", "Dalje :/ ...");
            }
        }

    }
}