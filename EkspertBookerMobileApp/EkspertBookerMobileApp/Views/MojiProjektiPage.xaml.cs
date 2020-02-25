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
            Application.Current.MainPage.DisplayAlert("Test", "test", "test");
            await model.Command();
        }
    }
}