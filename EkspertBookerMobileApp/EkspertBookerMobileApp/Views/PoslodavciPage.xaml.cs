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
    public partial class PoslodavciPage : ContentPage
    {
        private PoslodavciViewModel viewModel;
        public PoslodavciPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new PoslodavciViewModel();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await viewModel.Init();
        }
    }
}