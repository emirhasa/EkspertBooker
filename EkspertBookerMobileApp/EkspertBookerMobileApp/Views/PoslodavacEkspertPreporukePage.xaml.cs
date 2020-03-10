using EkspertBooker.Model;
using EkspertBooker.Model.Requests;
using EkspertBookerMobileApp.Helper;
using EkspertBookerMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EkspertBookerMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PoslodavacEkspertPreporukePage : ContentPage
    {

        private PoslodavacEkspertiPreporukeViewModel model;
        private readonly APIService _notifikacijeEkspertiService = new APIService("NotifikacijeEksperti");
        public PoslodavacEkspertPreporukePage(int projektId)
        {
            InitializeComponent();
            BindingContext = model = new PoslodavacEkspertiPreporukeViewModel(projektId);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            bool uspio = await model.Init();
            if (!uspio) PageExtensions.LoadPageError();
        }

        private async void PreporukeListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            EkspertPreporuka selected = e.SelectedItem as EkspertPreporuka;
            if (selected == null) return;
            try
            {
                var result = await _notifikacijeEkspertiService.Insert<NotifikacijaEkspert>(new NotifikacijaEkspertUpsertRequest
                {
                    EkspertId = selected.Ekspert.KorisnikId,
                    ProjektId = model._projektId,
                    Poruka = "Dobili ste zahtjev za ponudu od poslodavca, kliknite na obavijest da otvorite projekat."
                });
                if (result != null)
                {
                    Application.Current.MainPage.DisplayAlert("Info", "Uspješno ste poslali zahtjev za ponudu", "OK");
                    Navigation.PopAsync();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Greška", "Greška prilikom slanja zahtjeva", "OK");
                    Navigation.PopAsync();
                }
            }
            catch
            {
                Application.Current.MainPage.DisplayAlert("Greška", "Greška prilikom slanja zahtjeva", "OK");
                Navigation.PopAsync();
            }
        }
    }
}