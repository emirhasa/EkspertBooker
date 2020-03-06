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
        private PoslodavacProjektDetailViewModel viewModel;
        public PoslodavacProjektDetailPage(int projektId)
        {
            InitializeComponent();
            BindingContext = viewModel = new PoslodavacProjektDetailViewModel(projektId);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await viewModel.Init();
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
                Application.Current.MainPage.DisplayAlert("Info", "Detalji su dostupni kada projekt postane aktivan ili po završetku projekta!", "OK");
            }
        }

    }
}