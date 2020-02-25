using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using EkspertBookerMobileApp.Models;

namespace EkspertBookerMobileApp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class PoslodavacMainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public PoslodavacMainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;
            var korisnik = LoggedUser.logovaniKorisnik;
            //MenuPages.Add((int)MenuItemType.Browse, (NavigationPage)Detail);
        }

        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)PoslodavacMenuItemType.MojiProjekti:
                        MenuPages.Add(id, new NavigationPage(new MojiProjektiPage()));
                        break;
                    case (int)PoslodavacMenuItemType.Eksperti:
                        MenuPages.Add(id, new NavigationPage(new EkspertiPage()));
                        break;
                    case (int)PoslodavacMenuItemType.NoviProjekat:
                        MenuPages.Add(id, new NavigationPage(new ProjektUpsertPage()));
                        break;
                    case (int)PoslodavacMenuItemType.MojProfil:
                        MenuPages.Add(id, new NavigationPage(new MojProfilPoslodavacPage()));
                        break;
                    case (int)PoslodavacMenuItemType.Postavke:
                        MenuPages.Add(id, new NavigationPage(new PoslodavacSettingsPage()));
                        break;
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}