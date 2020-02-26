using EkspertBooker.Model;
using EkspertBookerMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Reflection;
using EkspertBookerMobileApp.ContentViews;

namespace EkspertBookerMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MojiProjektiPage : ContentPage
    {
        private MojiProjektiViewModel model { get; set; } 
        public MojiProjektiPage()
        {
            InitializeComponent();
            BindingContext = model = new MojiProjektiViewModel();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await model.Init();
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Projekt;
            if (item == null) return;

            await Navigation.PushAsync(new ProjektDetailPage(new ProjektDetailViewModel(item)));
        }

        private async void CVImageButton_Clicked(object sender, EventArgs e)
        {
            CVImageButton button = sender as CVImageButton;
            await Task.Delay(80);
            await button.FadeTo(0, 125);
            await Task.Delay(80);
            await button.FadeTo(1, 125);
        }
    }
}