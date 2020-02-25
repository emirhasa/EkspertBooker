using EkspertBooker.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EkspertBookerMobileApp.ViewModels
{
    public class ProjektDetailViewModel : BaseViewModel
    {
        private readonly APIService _projektiService = new APIService("Projekti");
        Projekt _projekt;
        public Projekt Projekt
        {
            get { return _projekt; }
            set { SetProperty(ref _projekt, value); }
        }
        public ProjektDetailViewModel(Projekt projekt)
        {
            if (projekt != null)
            {
                Title = projekt.Naziv;
                Projekt = projekt;
            } 
        }

        public ICommand InitCommand { get; set; }
        public async Task Init()
        {
            Projekt = await _projektiService.GetById<Projekt>(Projekt.ProjektId);
        }

    }

}
