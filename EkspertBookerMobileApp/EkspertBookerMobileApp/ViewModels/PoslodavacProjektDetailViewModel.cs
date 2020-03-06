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
    public class PoslodavacProjektDetailViewModel : BaseViewModel
    {
        private readonly APIService _projektiService = new APIService("Projekti");
        Projekt _projekt;
        int projektId;
        public Projekt Projekt
        {
            get { return _projekt; }
            set { SetProperty(ref _projekt, value); }
        }

        bool _urediButtonVisible = false;
        public bool UrediButtonVisible
        {
            get { return _urediButtonVisible; }
            set { SetProperty(ref _urediButtonVisible, value); }
        }

        bool _detaljniOpisVisible = false;
        public bool DetaljniOpisVisible
        {
            get { return _detaljniOpisVisible; }
            set { SetProperty(ref _detaljniOpisVisible, value); }
        }

        bool _datumPocetkaVisible = false;
        public bool DatumPocetkaVisible
        {
            get { return _datumPocetkaVisible; }
            set { SetProperty(ref _datumPocetkaVisible, value); }
        }

        bool _datumPocetkaAlternateVisible = false;
        public bool DatumPocetkaAlternateVisible
        {
            get { return _datumPocetkaAlternateVisible; }
            set { SetProperty(ref _datumPocetkaAlternateVisible, value); }
        }

        public PoslodavacProjektDetailViewModel(int _projektId)
        {
            projektId = _projektId;
            InitCommand = new Command(async () => await Init());
        }

        public ICommand InitCommand { get; set; }
        public async Task Init()
        {
            Projekt = await _projektiService.GetById<Projekt>(projektId);
            if (Projekt.StanjeId == "Licitacija") UrediButtonVisible = true; else UrediButtonVisible = false;
            DetaljniOpisVisible = !string.IsNullOrWhiteSpace(Projekt.DetaljniOpis);
            DatumPocetkaVisible = !string.IsNullOrWhiteSpace(Projekt.DatumPocetka.ToString());
            if(!DatumPocetkaVisible)
            {
                DatumPocetkaAlternateVisible = true;
            } else
            {
                DatumPocetkaAlternateVisible = false;
            }
        }

    }

}
