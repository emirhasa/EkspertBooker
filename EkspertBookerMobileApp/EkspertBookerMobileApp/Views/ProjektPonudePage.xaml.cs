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
    public partial class ProjektPonudePage : ContentPage
    {
        private ProjektPonudeViewModel model;
        public ProjektPonudePage(int projektId)
        {
            InitializeComponent();
            BindingContext = model = new ProjektPonudeViewModel(projektId);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await model.Init();
        }
    }
}