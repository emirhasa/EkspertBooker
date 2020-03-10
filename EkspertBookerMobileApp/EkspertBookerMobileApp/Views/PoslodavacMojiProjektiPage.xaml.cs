using EkspertBooker.Model;
using EkspertBookerMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Reflection;
using EkspertBookerMobileApp.ContentViews;

namespace EkspertBookerMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PoslodavacMojiProjektiPage : ContentPage
    {
        private PoslodavacMojiProjektiViewModel model;
        public PoslodavacMojiProjektiPage()
        {
            InitializeComponent();
            BindingContext = model = new PoslodavacMojiProjektiViewModel();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void ObavijestiListView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsVisible")
            {
                ListView listview = sender as ListView;
                if (listview.IsVisible == true)
                {
                    listview.FadeTo(1, 500, Easing.SpringIn);
                }
                else listview.FadeTo(0, 500, null);
            }
        }

        private async void Obavijest_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            NotifikacijaPoslodavac notif = e.SelectedItem as NotifikacijaPoslodavac;
            if (notif == null) return;

            await Navigation.PushAsync(new PoslodavacProjektDetailPage(notif.ProjektId));

            ObavijestiListView.SelectedItem = null;
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Projekt;
            if (item == null) return;

            await Navigation.PushAsync(new PoslodavacProjektDetailPage(item.ProjektId));
        }

        private async void CVImageButton_Clicked(object sender, EventArgs e)
        {
            CVImageButton button = sender as CVImageButton;
            await Task.Delay(80);
            await button.FadeTo(0, 125);
            await Task.Delay(80);
            await button.FadeTo(1, 125);
        }

        private void AktivniProjekt_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Projekt projekt = e.SelectedItem as Projekt;
            if (projekt == null) return;
            Navigation.PushAsync(new PoslodavacProjektDetailPage(projekt.ProjektId));
            AktivniProjektiListView.SelectedItem = null;
        }

        private void LicitacijaProjekt_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Projekt projekt = e.SelectedItem as Projekt;
            if (projekt == null) return;
            Navigation.PushAsync(new PoslodavacProjektDetailPage(projekt.ProjektId));
            LicitacijaProjektiListView.SelectedItem = null;
        }

        private void ZavrseniProjekt_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Projekt projekt = e.SelectedItem as Projekt;
            if (projekt == null) return;
            Navigation.PushAsync(new PoslodavacProjektDetailPage(projekt.ProjektId));
            ZavrseniProjektiListView.SelectedItem = null;
        }

    }
}