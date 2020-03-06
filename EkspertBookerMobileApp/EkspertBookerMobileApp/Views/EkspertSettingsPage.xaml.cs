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
    public partial class EkspertSettingsPage : ContentPage
    {

        private EkspertSettingsViewModel model;
        public EkspertSettingsPage()
        {
            InitializeComponent();
            BindingContext = model = new EkspertSettingsViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await model.Init();
        }

        private async void Pretplata_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var checkbox = sender as CheckBox;
            if (e.Value == true)
            {
                checkbox.Color = Color.Red;
            }
            else
            {
                checkbox.Color = Color.LightSlateGray;
            }
        }

        private void KategorijeListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            CustomKategorija selected = e.SelectedItem as CustomKategorija;
            selected.isChecked = !selected.isChecked;
        }

        private async void ButtonPotvrdi_Clicked(object sender, EventArgs e)
        {
            await model.UpdatePretplate();
        }

        private async void ButtonMojProfil_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MojProfilEkspertPage());
        }

        private async void ButtonPonude_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EkspertMojePonudePage());
        }

    }
}