﻿using EkspertBooker.Model;
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
            //operate on budzet as NewTextValue is readonly
            var budzet = e.NewTextValue;
            if (!string.IsNullOrWhiteSpace(budzet))
            {
                //remove any letters
                while (!budzet.ToCharArray().All(c => char.IsDigit(c)))
                {
                    budzet = budzet.Remove(budzet.Length - 1);
                }
                ((Entry)sender).Text = budzet;

                //if we still have numbers inside, remove forward 0s
                if (!string.IsNullOrWhiteSpace(budzet))
                {
                    while (budzet[0] == '0')
                    {
                        budzet = budzet.Remove(0, 1);
                        if (budzet.Length == 0) break;
                    }
                    //now validate what remains
                    ((Entry)sender).Text = budzet;
                    if (!ValidationRules.IsNullOrEmptyRule(budzet))
                    {
                        BudzetErrorLabel.IsVisible = true;
                        return;
                    }


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
                    //if the text passed all the validations up 'til here it's proper, no error label visible...
                    BudzetErrorLabel.IsVisible = false;
                    //keep updating text entry where needed to bind the proper value in the view model
                    ((Entry)sender).Text = budzet;
                    return;
                }
            } else
            {
                ((Entry)sender).Text = null;
                BudzetErrorLabel.IsVisible = true;
                return;
            }
        }

        private async void Submit_Clicked(object sender, EventArgs e)
        {
            bool uspio = await model.SubmitProjekt();
            if (uspio) Application.Current.MainPage = new PoslodavacMainPage();
        }
    }
}