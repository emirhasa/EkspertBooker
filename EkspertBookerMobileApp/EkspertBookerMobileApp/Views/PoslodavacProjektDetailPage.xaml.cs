using EkspertBooker.Model;
using EkspertBookerMobileApp.ContentViews;
using EkspertBookerMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EkspertBookerMobileApp.Helper;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EkspertBookerMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PoslodavacProjektDetailPage : ContentPage
    {
        public PoslodavacProjektDetailViewModel viewModel;
        private readonly APIService _recenzijeService = new APIService("RecenzijeOEksperti");
        public PoslodavacProjektDetailPage(int projektId)
        {
            InitializeComponent();
            BindingContext = viewModel = new PoslodavacProjektDetailViewModel(projektId);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //try - catch instead?
            bool uspio = await viewModel.Init();
            if (!uspio) PageExtensions.LoadPageError();
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            viewModel.Init();
        }

        private async void DostaviPonudu_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PonudaInsertPage(viewModel.Projekt.ProjektId));
        }

        private async void UrediButton_Clicked(object sender, EventArgs e)
        {
            CVImageButton button = sender as CVImageButton;

            var actualColor = button.BgColor;
            var newColor = Color.WhiteSmoke;

            // Here is the effective use of the smooth background color change animation
            await button.ChangeBackgroundColorTo(newColor, 100, Easing.CubicOut);
            await button.ChangeBackgroundColorTo(actualColor, 80, Easing.SinOut);

            await Navigation.PushModalAsync(new ProjektUpsertPage(viewModel.Projekt.ProjektId));
        }

        private async void Ponude_Clicked(object sender, EventArgs e)
        {
            CVImageButton button = sender as CVImageButton;
            var actualColor = button.BgColor;
            var newColor = Color.WhiteSmoke;

            // Here is the effective use of the smooth background color change animation
            await button.ChangeBackgroundColorTo(newColor, 100, Easing.CubicOut);
            await button.ChangeBackgroundColorTo(actualColor, 80, Easing.SinOut);

            Navigation.PushAsync(new ProjektPonudePage(viewModel.Projekt.ProjektId));
        }

        private async void Detaljno_Clicked(object sender, EventArgs e)
        {
            CVImageButton button = sender as CVImageButton;
            var actualColor = button.BgColor;
            var newColor = Color.WhiteSmoke;

            // Here is the effective use of the smooth background color change animation
            await button.ChangeBackgroundColorTo(newColor, 100, Easing.CubicOut);
            await button.ChangeBackgroundColorTo(actualColor, 80, Easing.SinOut);

            if ((viewModel.Projekt.StanjeId == "Aktivan") || (viewModel.Projekt.StanjeId == "Zavrsen"))
            Navigation.PushAsync(new ProjektDetailDetaljnoPage(viewModel.Projekt.ProjektId));
            else
            {
                Application.Current.MainPage.DisplayAlert("Info", "Detalji su dostupni kada projekt postane aktivan ili po završetku projekta! Projekat će biti aktivan nakon prihvatanja jedne ponude.", "OK");
            }
        }

        private async void PoslodavacDetalji_Clicked(object sender, EventArgs e)
        {
            CVImageButtonAlt button = sender as CVImageButtonAlt;

            var actualColor = button.BgColor;
            var newColor = Color.WhiteSmoke;

            // Here is the effective use of the smooth background color change animation
            await button.ChangeBackgroundColorTo(newColor, 100, Easing.CubicOut);
            await button.ChangeBackgroundColorTo(actualColor, 80, Easing.SinOut);

            await Navigation.PushModalAsync(new PoslodavacProfilPage(viewModel.Projekt.PoslodavacId));
        }

        private async void ZavrsiProjekat_Clicked(object sender, EventArgs e)
        {
            if(viewModel.Projekt.StanjeId == "Aktivan")
            {
                bool uspio = await viewModel.ZavrsiProjekat();
                if(uspio)
                {
                    //this is probably just lazy refresh
                    Navigation.InsertPageBefore(new PoslodavacProjektDetailPage(viewModel.Projekt.ProjektId), this);
                    await Navigation.PopAsync();
                }
            } else
            {
                Application.Current.MainPage.DisplayAlert("Info", "Samo aktivan projekat se može završiti! Da bi aktivirali projekt, prihvatite neku od ekspert ponuda!", "OK");
            }
        }

        private async void OstaviRecenziju_Clicked(object sender, EventArgs e)
        {
            //poslodavac ostavlja recenziju za eksperta
            if(viewModel.Projekt.StanjeId == "Zavrsen")
            {
                if (viewModel.Projekt.RecenzijaOEkspert == null)
                {
                    //otvori modal/page za ostavljanje recenzije
                    Navigation.PushModalAsync(new InsertRecenzijaOEkspertuPage(viewModel.Projekt.ProjektId, this));
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Greška", "Recenzija o ekspertu već postoji", "OK");
                }
            } else
            {
                Application.Current.MainPage.DisplayAlert("Info", "Možete ostaviti recenziju o ekspertu nakon što se projekat završi...", "OK");
            }
        }

        private async void Preporuka_Clicked(object sender, EventArgs e)
        {
            if(viewModel.Projekt.StanjeId == "Licitacija")
            {
                Navigation.PushAsync(new PoslodavacEkspertPreporukePage(viewModel.Projekt.ProjektId));
            } else
            {
                Application.Current.MainPage.DisplayAlert("Info", "Možete koristiti sistem za preporuku eksperata samo ako je projekt u fazi licitacije", "OK");
            }
        }
    }
}