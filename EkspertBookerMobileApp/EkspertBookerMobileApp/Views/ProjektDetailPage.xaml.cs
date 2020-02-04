using EkspertBooker.Model;
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
    public partial class ProjektDetailPage : ContentPage
    {
        public ProjektDetailPage(Projekt projekt)
        {
            InitializeComponent();
            Projekt _projekt = projekt;
            DisplayAlert("Naslov", _projekt.Naziv, "OK");
        }
    }
}