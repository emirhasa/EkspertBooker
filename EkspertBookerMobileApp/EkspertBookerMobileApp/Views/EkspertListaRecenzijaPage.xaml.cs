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
    public partial class EkspertListaRecenzijaPage : ContentPage
    {
        private EkspertRecenzijeViewModel model;
        public EkspertListaRecenzijaPage(int EkspertId)
        {
            InitializeComponent();
            BindingContext = model = new EkspertRecenzijeViewModel(EkspertId);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            bool uspio = await model.Init();
            if (!uspio) PageExtensions.LoadPageError();
        }
    }
}