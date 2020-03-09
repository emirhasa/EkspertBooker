using EkspertBooker.Model;
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
    public partial class ProjektPonudePage : ContentPage
    {
        public ProjektPonudeViewModel model;
        public ProjektPonudePage(int projektId)
        {
            InitializeComponent();
            BindingContext = model = new ProjektPonudeViewModel(projektId);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await model.Init();
            await model.UcitajOdbijenePonude();
            await model.UcitajAktivnePonude();
            
            if (PonudeFilterPicker.Items.Count == 0)
            {
                PonudeFilterPicker.Items.Add("Aktivne");
                PonudeFilterPicker.Items.Add("Odbijene");
                PonudeFilterPicker.SelectedItem = "Aktivne";
            }

            if(PonudeFilterPicker.SelectedItem == "Aktivne")
            {
                AktivnePonude.IsVisible = true;
                OdbijenePonude.IsVisible = false;
            } else
            {
                AktivnePonude.IsVisible = false;
                OdbijenePonude.IsVisible = true;
            }
        }

        private async void AktivnaPonuda_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Ponuda ponuda = e.SelectedItem as Ponuda;
            if (ponuda == null) return;

            await Navigation.PushModalAsync(new PonudaDetaljiPage(ponuda.PonudaId, this));

            AktivnePonudeListView.SelectedItem = null;
        }


        private async void OdbijenaPonuda_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Ponuda ponuda = e.SelectedItem as Ponuda;
            if (ponuda == null) return;
            await Navigation.PushModalAsync(new PonudaDetaljiPage(ponuda.PonudaId, this));

            AktivnePonudeListView.SelectedItem = null;
        }

        private async void PonudeFilterPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker filter = sender as Picker;
            if (filter.SelectedItem == "Aktivne")
            {
                await model.UcitajAktivnePonude();
                AktivnePonude.IsVisible = true;
                OdbijenePonude.IsVisible = false;
            } else
            {
                await model.UcitajOdbijenePonude();
                AktivnePonude.IsVisible = false;
                OdbijenePonude.IsVisible = true;
            }
        }

        private void PrihvacenaPonuda_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Ponuda prihvacena = e.SelectedItem as Ponuda;
            if (prihvacena == null) return;
            Navigation.PushModalAsync(new PonudaDetaljiPage(prihvacena.PonudaId));
            PrihvacenaPonudaListView.SelectedItem = null;
        }

    }
}