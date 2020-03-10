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
    public partial class EkspertProfilPage : ContentPage
    {
        private EkspertProfilViewModel model;
        public EkspertProfilPage(int ekspertId)
        {
            InitializeComponent();
            BindingContext = model = new EkspertProfilViewModel(ekspertId);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            bool uspio = await model.Init();
            if (!uspio) PageExtensions.LoadPageError();
        }

        private async void Recenzije_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new EkspertListaRecenzijaPage(model.Ekspert.KorisnikId));
        }
    }
}