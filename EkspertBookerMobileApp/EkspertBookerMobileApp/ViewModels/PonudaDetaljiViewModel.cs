using EkspertBooker.Model;
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
            if(Ponuda == null)
            {
                Application.Current.MainPage.DisplayAlert("Greška", "Greška u učitavanu ponude", "Dalje...");
                Application.Current.MainPage = new EkspertMainPage();
            } 
        }

        string _poslodavacKomentar = null;

        public string PoslodavacKomentar
        {
            get { return _poslodavacKomentar; }
            set { SetProperty(ref _poslodavacKomentar, value); }
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
                    Application.Current.MainPage.DisplayAlert("Info", "Ponuda uspješno prihvaćena!", "Super :)");
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
                    Application.Current.MainPage.DisplayAlert("Info", "Ponuda uspješno prihvaćena!", "Super :)");
                }
            }
            catch
            {
                Application.Current.MainPage.DisplayAlert("Greška", "Greška prilikom prihvatanja ponude...!", "Dalje :/ ...");
            }
        }

    }
}
