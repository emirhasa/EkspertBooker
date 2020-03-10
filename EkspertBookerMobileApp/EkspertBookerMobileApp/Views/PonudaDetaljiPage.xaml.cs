using EkspertBookerMobileApp.ContentViews;
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
    public partial class PonudaDetaljiPage : ContentPage
    {
        private PonudaDetaljiViewModel model;
        private ProjektPonudePage _rootPage = null;
        public PonudaDetaljiPage(int ponudaId, ProjektPonudePage rootPage = null)
        {
            InitializeComponent();
            BindingContext = model = new PonudaDetaljiViewModel(ponudaId);
            _rootPage = rootPage;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            bool uspio = await model.Init();
            if (!uspio) PageExtensions.LoadPageError();
        }

        private async void ButtonPrihvati_Clicked(object sender, EventArgs e)
        {
            var confirm = await DisplayAlert("Potvrda", "Sigurno želite PRIHVATITI ponudu? Projekt postaje aktivan, ostale ponude bivaju odbijene!", "DA!", "NE, nazad!");
            if(confirm)
            {
                model.PotvrdiPonudu();
                FinalPrihvatiForm.IsVisible = false;
                if (_rootPage != null)
                {
                    _rootPage.model.UcitajAktivnePonude();
                    _rootPage.model.UcitajOdbijenePonude();
                }
                Navigation.PopModalAsync();
            } else
            {
                KomentarEntry.Text = null;
            }
        }

        private async void ButtonOdbij_Clicked(object sender, EventArgs e)
        {
            var confirm = await DisplayAlert("Potvrda", "Jeste li sigurni da želite ODBITI ponudu?", "DA!", "NE, nazad!");
            if (confirm)
            {
                model.OdbijPonudu();
                FinalPrihvatiForm.IsVisible = false;
                if (_rootPage != null)
                {
                    _rootPage.model.UcitajAktivnePonude();
                    _rootPage.model.UcitajOdbijenePonude();
                }
                Navigation.PopModalAsync();
            }
            else
            {
                KomentarEntry.Text = null;
            }
        }

        private async void EkspertDetaljno_Clicked(object sender, EventArgs e)
        {
            CVImageButton button = sender as CVImageButton;
            var actualColor = button.BgColor;
            var newColor = Color.WhiteSmoke;

            // Here is the effective use of the smooth background color change animation
            await button.ChangeBackgroundColorTo(newColor, 100, Easing.CubicOut);
            await button.ChangeBackgroundColorTo(actualColor, 80, Easing.SinOut);

            await Navigation.PushModalAsync(new EkspertProfilPage(model.Ponuda.EkspertId));
        }

    }
}