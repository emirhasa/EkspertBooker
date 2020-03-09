using EkspertBookerMobileApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EkspertBookerMobileApp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class PoslodavacMenuPage : ContentPage
    {
        PoslodavacMainPage RootPage { get => Application.Current.MainPage as PoslodavacMainPage; }
        List<PoslodavacMenuItem> menuItems;
        public PoslodavacMenuPage()
        {
            InitializeComponent();

            menuItems = new List<PoslodavacMenuItem>
            {
                new PoslodavacMenuItem {Id = PoslodavacMenuItemType.MojiProjekti, Title="Moji Projekti" },
                new PoslodavacMenuItem {Id = PoslodavacMenuItemType.NoviProjekat, Title="Novi Projekat" },
                new PoslodavacMenuItem {Id = PoslodavacMenuItemType.MojProfil, Title="Moj Profil" },
                new PoslodavacMenuItem {Id = PoslodavacMenuItemType.Eksperti, Title="Eksperti" },
                new PoslodavacMenuItem {Id = PoslodavacMenuItemType.Postavke, Title="Postavke" },
                new PoslodavacMenuItem {Id = PoslodavacMenuItemType.Logout, Title="Logout" }
            };

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((PoslodavacMenuItem)e.SelectedItem).Id;
                if (id != (int)PoslodavacMenuItemType.Logout)
                    await RootPage.NavigateFromMenu(id);
                else
                {
                    LoggedUser.logovaniKorisnik = null;
                    Application.Current.MainPage = new LoginPage();
                }
            };
        }
    }
}