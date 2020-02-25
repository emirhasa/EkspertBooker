using EkspertBooker.Model;
using EkspertBookerMobileApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EkspertBookerMobileApp.ViewModels
{
    public class PonudaDetaljiViewModel : BaseViewModel
    {
        private readonly APIService _ponudeService = new APIService("Ponude");
        int ponudaId;

        Ponuda _ponuda;
        public Ponuda Ponuda
        {
            get { return _ponuda; }
            set { SetProperty(ref _ponuda, value); }
        }
        public PonudaDetaljiViewModel(int _ponudaId)
        {
            ponudaId = _ponudaId;
            InitCommand = new Command(async () => await Init());
        }

        public ICommand InitCommand { get; set; }

        public async Task Init()
        {
            Ponuda = await _ponudeService.GetById<Ponuda>(ponudaId);
            if(Ponuda == null)
            {
                Application.Current.MainPage.DisplayAlert("Greška", "Greška u učitavanu ponude", "Dalje...");
                Application.Current.MainPage = new EkspertMainPage();
            } 
        }
    }
}
