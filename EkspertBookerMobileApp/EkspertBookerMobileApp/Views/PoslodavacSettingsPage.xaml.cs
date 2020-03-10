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
    public partial class PoslodavacSettingsPage : ContentPage
    {
        private PoslodavacSettingsViewModel model;
        public PoslodavacSettingsPage()
        {
            InitializeComponent();
            BindingContext = model = new PoslodavacSettingsViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            bool uspio = await model.Init();
            if (!uspio) PageExtensions.LoadPageError();
        }

        private async void ButtonPotvrdi_Clicked(object sender, EventArgs e)
        {
            await model.UpdateSettings();
        }
    }
}