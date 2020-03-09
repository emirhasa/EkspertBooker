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
    public partial class EkspertMainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public EkspertMainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            //MenuPages.Add((int)EkspertMenuItemType.Projekti, (NavigationPage)Detail);
        }

        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)EkspertMenuItemType.MojePonude:
                        MenuPages.Add(id, new NavigationPage(new EkspertMojePonudePage()));
                        break;
                    case (int)EkspertMenuItemType.Projekti:
                        MenuPages.Add(id, new NavigationPage(new ProjektiPage()));
                        break;
                    case (int)EkspertMenuItemType.Poslodavci:
                        MenuPages.Add(id, new NavigationPage(new PoslodavciPage()));
                        break;
                    case (int)EkspertMenuItemType.MojProfil:
                        MenuPages.Add(id, new NavigationPage(new MojProfilEkspertPage()));
                        break;
                    case (int)EkspertMenuItemType.Postavke:
                        MenuPages.Add(id, new NavigationPage(new EkspertSettingsPage()));
                        break;
                    case (int)EkspertMenuItemType.Logout:
                        Application.Current.MainPage = new LoginPage();
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