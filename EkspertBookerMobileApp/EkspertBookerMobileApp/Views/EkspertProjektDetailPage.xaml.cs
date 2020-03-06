﻿using EkspertBookerMobileApp.ContentViews;
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
    public partial class EkspertProjektDetailPage : ContentPage
    {

        //TODO: provjeri postoji li ponuda, ako ima, napiši da postoji te stavi button za UREĐIVANJE - open postojeća ponuda edit
        //ako ne postoji, button je sličan ali otvara formu za novu ponudu
        //ako je projekt aktivan/neaktivan - i dalje stoje info o ponudi ako postoji - u tom slucaju = vidi detalje
        private EkspertProjektDetailViewModel model;
        public EkspertProjektDetailPage(int projektId)
        {
            InitializeComponent();
            BindingContext = model = new EkspertProjektDetailViewModel(projektId);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await model.Init();
        }

        private async void MojaPonuda_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PonudaInsertPage(model.Projekt.ProjektId));
        }

        private async void UrediButton_Clicked(object sender, EventArgs e)
        {
            CVImageButton button = sender as CVImageButton;

            var actualColor = button.BgColor;
            var newColor = Color.WhiteSmoke;

            // Here is the effective use of the smooth background color change animation
            await button.ChangeBackgroundColorTo(newColor, 100, Easing.CubicOut);
            await button.ChangeBackgroundColorTo(actualColor, 80, Easing.SinOut);

            await Navigation.PushModalAsync(new ProjektUpsertPage(model.Projekt.ProjektId));
        }

        private async void Ponude_Clicked(object sender, EventArgs e)
        {
            CVImageButton button = sender as CVImageButton;
            var actualColor = button.BgColor;
            var newColor = Color.WhiteSmoke;

            // Here is the effective use of the smooth background color change animation
            await button.ChangeBackgroundColorTo(newColor, 100, Easing.CubicOut);
            await button.ChangeBackgroundColorTo(actualColor, 80, Easing.SinOut);

            Navigation.PushAsync(new ProjektPonudePage(model.Projekt.ProjektId));
        }

        private async void Detaljno_Clicked(object sender, EventArgs e)
        {
            CVImageButton button = sender as CVImageButton;
            var actualColor = button.BgColor;
            var newColor = Color.WhiteSmoke;

            // Here is the effective use of the smooth background color change animation
            await button.ChangeBackgroundColorTo(newColor, 100, Easing.CubicOut);
            await button.ChangeBackgroundColorTo(actualColor, 80, Easing.SinOut);

            if ((model.Projekt.StanjeId == "Aktivan") || (model.Projekt.StanjeId == "Zavrsen"))
                Navigation.PushAsync(new ProjektDetailDetaljnoPage(model.Projekt.ProjektId));
            else
            {
                Application.Current.MainPage.DisplayAlert("Info", "Detalji su dostupni kada projekt postane aktivan ili po završetku projekta!", "OK");
            }
        }

        private async void PrijasnjaPonuda_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        private async void NovaPonuda_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new PonudaInsertPage(model.Projekt.ProjektId));
        }

        private async void UrediPonudu_Clicked(object sender, EventArgs e)
        {

        }

        private async void ObrisiPonudu_Clicked(object sender, EventArgs e)
        {
            var confirm = await DisplayAlert("Potvrda", "Sigurno želite obrisati ponudu?", "DA!", "NE, nazad!");
            if (confirm)
            {
                var uspio =  await model.ObrisiAktivnuPonudu();
                if (uspio)
                {
                    Application.Current.MainPage.DisplayAlert("Info", "Uspješno obrisana ponuda!", "OK");
                    Navigation.InsertPageBefore(new EkspertProjektDetailPage(model.Projekt.ProjektId), this);
                    Navigation.PopAsync();
                } else
                {
                    Application.Current.MainPage.DisplayAlert("Greška", "Greška prilikom brisanja ponude!", "OK");
                }
            }
        }

    }
}