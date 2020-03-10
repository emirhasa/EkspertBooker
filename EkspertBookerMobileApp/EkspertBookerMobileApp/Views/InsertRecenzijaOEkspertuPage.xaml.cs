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
    public partial class InsertRecenzijaOEkspertuPage : ContentPage
    {
        private InsertRecenzijaOEkspertuViewModel model;
        private PoslodavacProjektDetailPage _rootPage;
        public InsertRecenzijaOEkspertuPage(int projektId, PoslodavacProjektDetailPage rootPage)
        {
            InitializeComponent();
            BindingContext = model = new InsertRecenzijaOEkspertuViewModel(projektId);
            _rootPage = rootPage;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            bool uspio = await model.Init();
            if (!uspio) PageExtensions.LoadPageError();
        }

        private async void ButtonSubmit_Clicked(object sender, EventArgs e)
        {
            if (model.IsValid())
            {
                ErrorLabel.IsVisible = false;
                bool uspio = await model.Submit();
                if (uspio)
                {
                    //refreshuj stanje u root page koji je pozvao modal, da se ne bi desilo da root page pogresno "misli" da recenzija o ekspertu ne postoji dok awaita request
                    _rootPage.viewModel.Init();
                    Navigation.PopModalAsync();
                }
            } else
            {
                ErrorLabel.IsVisible = true;
            } 
        }

        private async void Ocjena_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                try
                {
                    int Ocjena = int.Parse(EntryOcjena.Text);
                    if (Ocjena >= 5 && Ocjena <= 10)
                    {
                        ErrorLabel.IsVisible = false;
                    }
                    else
                    {
                        ErrorLabel.IsVisible = true;
                    }
                    ((Entry)sender).Text = e.NewTextValue;
                }
                catch
                {
                    await Application.Current.MainPage.DisplayAlert("Greška", "Molimo unosite samo pozitivne cjelobrojne vrijednosti u polje Ocjena", "OK");
                    ((Entry)sender).Text = null;
                }
            } else
            {
                ((Entry)sender).Text = null;
            }
        }
    }
}