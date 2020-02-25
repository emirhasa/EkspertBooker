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
    public partial class PonudaDetaljiPage : ContentPage
    {
        private PonudaDetaljiViewModel model;
        public PonudaDetaljiPage(int ponudaId)
        {
            InitializeComponent();
            BindingContext = model = new PonudaDetaljiViewModel(ponudaId);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await model.Init();
        }
    }
}