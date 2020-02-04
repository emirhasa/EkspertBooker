using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EkspertBookerMobileApp.Services;
using EkspertBookerMobileApp.Views;

namespace EkspertBookerMobileApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
