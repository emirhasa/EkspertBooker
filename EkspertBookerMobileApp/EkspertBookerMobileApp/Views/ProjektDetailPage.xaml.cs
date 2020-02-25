using EkspertBooker.Model;
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
    public partial class ProjektDetailPage : ContentPage
    {
        private ProjektDetailViewModel viewModel;
        public ProjektDetailPage(ProjektDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            viewModel.Init();
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            viewModel.Init();
        }

        private void DostaviPonudu_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PonudaInsertPage(viewModel.Projekt.ProjektId));
        }

        private void UrediProjekatButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ProjektUpsertPage(viewModel.Projekt.ProjektId));
        }

        private void PrikaziPonude_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ProjektPonudePage(viewModel.Projekt.ProjektId));
        }
    }
}