using EkspertBookerMobileApp.Validation;
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
    public partial class PonudaUpdatePage : ContentPage
    {
        private PonudaUpdateViewModel model;
        public PonudaUpdatePage(int ponudaId)
        {
            InitializeComponent();
            BindingContext = model = new PonudaUpdateViewModel(ponudaId);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await model.Init();
        }

        private void Naslov_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (model.ValidationTriggered)
            {
                model.NaslovErrorVisible = string.IsNullOrWhiteSpace(e.NewTextValue);
            }
        }

        private void OpisPonude_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (model.ValidationTriggered)
            {
                model.OpisPonudeErrorVisible = string.IsNullOrWhiteSpace(e.NewTextValue);
            }
        }

        private void Cijena_TextChanged(object sender, TextChangedEventArgs e)
        {
            var cijena = e.NewTextValue;
            if (!e.NewTextValue.ToCharArray().All(c => char.IsDigit(c)))
            {
                cijena = cijena.Remove(cijena.Length - 1);
                ((Entry)sender).Text = cijena;
            }
            if (model.ValidationTriggered)
            {
                //remove any forward 0s if necessary
                if (!string.IsNullOrWhiteSpace(cijena))
                {
                    while (cijena[0] == '0')
                    {
                        cijena = cijena.Remove(0, 1);
                        if (cijena.Length == 0) break;
                    }
                    if (!ValidationRules.IsNullOrEmptyRule(cijena))
                    {
                        model.CijenaErrorVisible = true;
                        //CijenaErrorLabel.Text = "Unesite cijenu!";
                        return;
                    }
                    ((Entry)sender).Text = cijena;
                    if (int.Parse(cijena) < 10 || int.Parse(cijena) > 500000)
                    {
                        CijenaErrorLabel.IsVisible = true;
                        //CijenaErrorLabel.Text = "Cijena treba biti veća od 10 ili manja od 500000!";
                        if (int.Parse(cijena) > 500000)
                        {
                            cijena = 500000.ToString();
                        }
                        ((Entry)sender).Text = cijena;
                        return;
                    }
                    CijenaErrorLabel.IsVisible = false;
                    return;
                }
                else
                {
                    CijenaErrorLabel.IsVisible = true;
                    return;
                }
            }
            
        }
    }
}