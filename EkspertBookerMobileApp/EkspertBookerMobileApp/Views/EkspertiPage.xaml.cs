using EkspertBooker.Model;
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
    public partial class EkspertiPage : ContentPage
    {
        private EkspertiViewModel viewModel;
        public EkspertiPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new EkspertiViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            bool uspio = await viewModel.Init();
            if (!uspio) PageExtensions.LoadPageError();
        }

        private async void EkspertiList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Ekspert selected = e.SelectedItem as Ekspert;
            if (selected == null) return;
            await Navigation.PushAsync(new EkspertProfilPage(selected.KorisnikId));
        }
    }
}