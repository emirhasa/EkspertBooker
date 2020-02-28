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

        private void Uredi_Clicked(object sender, EventArgs e)
        {

        }

        private void Recenzije_Clicked(object sender, EventArgs e)
        {

        }

        private void ZahtjevZaRecenziju_Clicked(object sender, EventArgs e)
        {

        }
    }
}