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
            await viewModel.Init();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            Ekspert selected_ekspert = (Ekspert)args.SelectedItem;
            await DisplayAlert("test", selected_ekspert.KorisnikId.ToString(), "test");
        }
    }
}