using EkspertBookerMobileApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace EkspertBookerMobileApp.Helper
{
    public static class PageExtensions
    {
        //ugl. prilikom hvatanja exceptiona prilikom Init, funkcionise na nivou svih platformi - alternativno moguce je uraditi general unhandled exception handling ali specific za svaku platformu
        //potrebno je svakako uraditi platform - specific handling all unhandled exceptions, radi razlicitih vrsta exceptiona koji se mogu pojaviti(ne samo u Viewmodel Init i sl.)
        public static void LoadPageError()
        {
            Application.Current.MainPage.DisplayAlert("Greška", "Problem prilikom učitavanja stranice. Možda imate problem sa konekcijom? Pokušajte opet kasnije...", "OK");
            if (LoggedUser.Role == "Poslodavac") Application.Current.MainPage = new PoslodavacMainPage();
            else Application.Current.MainPage = new EkspertMainPage();
        }

        public static bool IsModal(this Page page)
        {
            for (int i = 0; i < page.Navigation.ModalStack.Count(); i++)
            {
                if (page == page.Navigation.ModalStack[i])
                    return true;
            }
            return false;
        }
    }
}
