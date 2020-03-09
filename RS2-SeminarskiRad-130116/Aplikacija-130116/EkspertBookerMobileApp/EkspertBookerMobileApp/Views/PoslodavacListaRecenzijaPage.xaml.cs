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
    public partial class PoslodavacListaRecenzijaPage : ContentPage
    {
        private PoslodavacListaRecenzijaViewModel model;
        public PoslodavacListaRecenzijaPage(int poslodavacId)
        {
            InitializeComponent();
            BindingContext = model = new PoslodavacListaRecenzijaViewModel(poslodavacId);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await model.Init();
        }
    }
}