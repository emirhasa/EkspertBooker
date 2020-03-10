using EkspertBooker.Model;
using EkspertBookerMobileApp.Helper;
using EkspertBookerMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EkspertBookerMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProjektiPage : ContentPage
    {
        private ProjektiViewModel model;
        public ProjektiPage()
        {
            InitializeComponent();
            BindingContext = model = new ProjektiViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            bool uspio = await model.Init();
            if (!uspio) PageExtensions.LoadPageError();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Projekt;
            if (item == null)
                return;
            if (LoggedUser.Role == "Poslodavac")
            {
                await Navigation.PushAsync(new PoslodavacProjektDetailPage(item.ProjektId));
            } else
            {
                await Navigation.PushAsync(new EkspertProjektDetailPage(item.ProjektId));
            }

            // Manually deselect item.
            ProjektiListView.SelectedItem = null;
        }
    }
}