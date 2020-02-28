
using EkspertBookerMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EkspertBookerMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MojProfilEkspertPage : ContentPage
    {
        private MojProfilEkspertViewModel model;
        public MojProfilEkspertPage()
        {
            InitializeComponent();
            BindingContext = model = new MojProfilEkspertViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await model.Init();
        }

        private async void Uredi_Clicked(object sender, EventArgs e)
        {
            await PageScrollView.ScrollToAsync(UrediForm, ScrollToPosition.Start, true);
        }

        private void Recenzije_Clicked(object sender, EventArgs e)
        {

        }

        private void ZahtjevZaRecenziju_Clicked(object sender, EventArgs e)
        {
            
        }

        private async void UrediSubmit_Clicked(object sender, EventArgs e)
        {

            var uspio = await model.SacuvajPromjene();
            if (uspio)
            {
                OnAppearing();
                Application.Current.MainPage.DisplayAlert("Info", "Promjene sačuvane!", "OK");
                EntryIme.Text = null;
                EntryPrezime.Text = null;
                EntryEmail.Text = null;
                EntryTelefon.Text = null;
                model.ValidationTriggered = false;
                UrediFormErrorLabel.IsVisible = false;

            }
            
        }

        private void Entry_TextChanged(object sender, EventArgs e)
        {
            if (model.ValidationTriggered)
            {
                if (!string.IsNullOrWhiteSpace(EntryIme.Text) || !string.IsNullOrWhiteSpace(EntryPrezime.Text)
                    || !string.IsNullOrWhiteSpace(EntryEmail.Text) || !string.IsNullOrWhiteSpace(EntryTelefon.Text))
                {
                    UrediFormErrorLabel.IsVisible = false;
                } else
                {
                    UrediFormErrorLabel.IsVisible = true;
                }
            }
        }

        private void EmailEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            Entry_TextChanged(sender, e);
            if (!string.IsNullOrWhiteSpace(EntryEmail.Text))
            {
                try
                {
                    string text = e.NewTextValue;
                    MailAddress email = new MailAddress(text);
                    MailErrorLabel.IsVisible = false;
                }
                catch
                {
                    MailErrorLabel.IsVisible = true;
                }
            } else
            {
                MailErrorLabel.IsVisible = false;
            }
        }

    }
}