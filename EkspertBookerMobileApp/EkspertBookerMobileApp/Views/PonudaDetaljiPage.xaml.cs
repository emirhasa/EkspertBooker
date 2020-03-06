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
        public PonudaDetaljiPage(int ponudaId)
        {
            InitializeComponent();
            BindingContext = model = new PonudaDetaljiViewModel(ponudaId);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await model.Init();
        }

        private async void ButtonPrihvati_Clicked(object sender, EventArgs e)
        {
            var confirm = await DisplayAlert("Potvrda", "Sigurno želite PRIHVATITI ponudu? Projekt postaje aktivan, ostale ponude bivaju odbijene!", "DA!", "NE, nazad!");
            if(confirm)
            {
                model.PotvrdiPonudu();
                FinalPrihvatiForm.IsVisible = false;
                Navigation.PopAsync();
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
                Navigation.PopAsync();
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