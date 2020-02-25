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
    public partial class EkspertMenuPage : ContentPage
    {

        EkspertMainPage RootPage { get => Application.Current.MainPage as EkspertMainPage; }
        List<EkspertMenuItem> menuItems;
        public EkspertMenuPage()
        {
            InitializeComponent();

            menuItems = new List<EkspertMenuItem>
            {
                new EkspertMenuItem {Id = EkspertMenuItemType.MojePonude, Title="Moje Ponude" },
                new EkspertMenuItem {Id = EkspertMenuItemType.Projekti, Title="Projekti" },
                new EkspertMenuItem {Id = EkspertMenuItemType.MojProfil, Title="Moj Profil" },
                new EkspertMenuItem {Id = EkspertMenuItemType.Poslodavci, Title="Poslodavci" },
                new EkspertMenuItem {Id = EkspertMenuItemType.Postavke, Title="Postavke" },
                new EkspertMenuItem {Id = EkspertMenuItemType.Logout, Title="Logout" }
            };

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((EkspertMenuItem)e.SelectedItem).Id;
                if (id != (int)EkspertMenuItemType.Logout)
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