using EkspertBooker.Model;
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
            await model.Init();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Projekt;
            if (item == null)
                return;

            await Navigation.PushAsync(new ProjektDetailPage(item.ProjektId));

            // Manually deselect item.
            ProjektiListView.SelectedItem = null;
        }
    }
}