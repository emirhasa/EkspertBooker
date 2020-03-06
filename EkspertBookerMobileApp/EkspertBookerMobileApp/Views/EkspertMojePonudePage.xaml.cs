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
    public partial class EkspertMojePonudePage : ContentPage
    {
        private EkspertMojePonudeViewModel model;
        public EkspertMojePonudePage()
        {
            BindingContext = model = new EkspertMojePonudeViewModel();
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await model.Init();
        }
        private void PrihvacenePonudeListView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
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

        private void AktivnePonudeListView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
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

        private void OdbijenePonudeListView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
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

        private void Button_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            Application.Current.MainPage.DisplayAlert(button.ClassId, button.ClassId, "OK");
        }
        private async void PrihvacenaPonuda_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Ponuda ponuda = e.SelectedItem as Ponuda;
            if (ponuda == null) return;

            await Navigation.PushAsync(new EkspertProjektDetailPage(ponuda.ProjektId));

            PrihvacenePonudeListView.SelectedItem = null;
        }

        private async void AktivnaPonuda_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Ponuda ponuda = e.SelectedItem as Ponuda;
            if (ponuda == null) return;

            await Navigation.PushAsync(new EkspertProjektDetailPage(ponuda.ProjektId));

            AktivnePonudeListView.SelectedItem = null;
        }

        private async void OdbijenaPonuda_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Ponuda ponuda = e.SelectedItem as Ponuda;
            if (ponuda == null) return;

            await Navigation.PushAsync(new EkspertProjektDetailPage(ponuda.ProjektId));

            OdbijenePonudeListView.SelectedItem = null;
        }
    }
}