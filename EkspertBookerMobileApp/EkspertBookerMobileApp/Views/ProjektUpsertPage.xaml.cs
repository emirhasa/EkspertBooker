using EkspertBooker.Model;
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
    public partial class ProjektUpsertPage : ContentPage
    {
        private ProjektUpsertViewModel model;
        public ProjektUpsertPage(int? _projektId = null)
        {
            InitializeComponent();
            BindingContext = model = new ProjektUpsertViewModel(_projektId);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await model.Init();
            if (model.projektId != null)
            {
                await model.UcitajProjektInfo();

                //setuj selected index pickera na onu koji odgovara projektu
                var kategorije = KategorijaPicker.Items;
                Kategorija selectedKategorija = model.SelectedKategorija;
                foreach (var kategorija in kategorije)
                {
                    if (kategorija == selectedKategorija.Naziv) KategorijaPicker.SelectedIndex = KategorijaPicker.Items.IndexOf(kategorija);
                }
            }
        }

        private void CheckBox_HitanChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value == true)
            {
                model.Hitan = true;
            }
            else model.Hitan = false;
        }

        //should implement this kind of check in ViewModel on property set, if validation is triggered... better containment
        private void Naziv_TextChanged(object sender, TextChangedEventArgs e)
        {   
            if(model._validationTriggered)
            {
                if(!ValidationRules.IsNullOrEmptyRule(e.NewTextValue))
                {
                    NazivErrorLabel.IsVisible = true;
                } else
                {
                    NazivErrorLabel.IsVisible = false;
                }
            }
            return;
        }

        private void KratkiOpis_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (model._validationTriggered)
            {
                if (!ValidationRules.IsNullOrEmptyRule(e.NewTextValue))
                {
                    KratkiOpisErrorLabel.IsVisible = true;
                }
                else
                {
                    KratkiOpisErrorLabel.IsVisible = false;
                }
            }
            return;
        }

        private void Trajanje_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (model._validationTriggered)
            {
                if (!ValidationRules.IsNullOrEmptyRule(e.NewTextValue))
                {
                    TrajanjeErrorLabel.IsVisible = true;
                }
                else
                {
                    TrajanjeErrorLabel.IsVisible = false;
                }
            }
            return;
        }

        private void Budzet_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (model._validationTriggered)
            {
                //remove any forward 0s if necessary
                var budzet = e.NewTextValue;
                if (!string.IsNullOrWhiteSpace(budzet))
                {
                    while (budzet[0] == '0')
                    {
                        budzet = budzet.Remove(0, 1);
                        if (budzet.Length == 0) break;
                    }
                    if (!ValidationRules.IsNullOrEmptyRule(budzet))
                    {
                        BudzetErrorLabel.IsVisible = true;
                        BudzetErrorLabel.Text = "Unesite budžet!";
                        return;
                    }
                    ((Entry)sender).Text = budzet;
                    if (int.Parse(budzet) < 10 || int.Parse(budzet) > 500000)
                    {
                        BudzetErrorLabel.IsVisible = true;
                        BudzetErrorLabel.Text = "Budžet treba biti veći od 10 ili manji od 500000!";
                        if (int.Parse(budzet) > 500000)
                        {
                            budzet = 500000.ToString();
                        }
                        ((Entry)sender).Text = budzet;
                        return;
                    }
                    BudzetErrorLabel.IsVisible = false;
                } else
                {
                    BudzetErrorLabel.IsVisible = true;
                    return;
                }

            }
            return;
        }

        private async void Submit_Clicked(object sender, EventArgs e)
        {
            await model.SubmitProjekt();
            await Navigation.PopModalAsync();
        }
    }
}