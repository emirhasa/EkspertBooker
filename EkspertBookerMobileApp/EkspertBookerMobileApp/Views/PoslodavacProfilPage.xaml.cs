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
    public partial class PoslodavacProfilPage : ContentPage
    {
        private PoslodavacProfiLViewModel model;
        public PoslodavacProfilPage(int korisnikId)
        {
            InitializeComponent();
            BindingContext = model = new PoslodavacProfiLViewModel(korisnikId);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await model.Init();
        }
    }
}