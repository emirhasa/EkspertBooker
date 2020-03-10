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
    public partial class InsertRecenzijaOPoslodavcuPage : ContentPage
    {
        private InsertRecenzijaOPoslodavcuViewModel model;
        private EkspertProjektDetailPage _rootPage;
        public InsertRecenzijaOPoslodavcuPage(int projektId, EkspertProjektDetailPage rootPage)
        {
            InitializeComponent();
            BindingContext = model = new InsertRecenzijaOPoslodavcuViewModel(projektId);
            _rootPage = rootPage;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            bool uspio = await model.Init();
            if (!uspio) PageExtensions.LoadPageError();
        }

        private async void Ocjena_TextChanged(object Sender, TextChangedEventArgs e)
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
                    ((Entry)Sender).Text = e.NewTextValue;
                }
                catch
                {
                    await Application.Current.MainPage.DisplayAlert("Greška", "Molimo unosite samo pozitivne cjelobrojne vrijednosti u polje Ocjena", "OK");
                    ((Entry)Sender).Text = null;
                }
            }
            else
            {
                ((Entry)Sender).Text = null;
            }
        }

        private async void ButtonSubmit_Clicked(object Sender, EventArgs e)
        {
            if (model.IsValid())
            {
                ErrorLabel.IsVisible = false;
                bool uspio = await model.Submit();
                if(uspio)
                {
                    _rootPage.model.Init();
                    Navigation.PopModalAsync();
                }
            }
            else
            {
                ErrorLabel.IsVisible = true;
            }
        }

    }
}