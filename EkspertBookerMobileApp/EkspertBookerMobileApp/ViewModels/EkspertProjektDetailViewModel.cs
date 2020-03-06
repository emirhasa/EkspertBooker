using EkspertBooker.Model;
using EkspertBooker.Model.Requests;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EkspertBookerMobileApp.ViewModels
{
    public class EkspertProjektDetailViewModel : BaseViewModel
    {
        private readonly APIService _projektiService = new APIService("Projekti");
        private readonly APIService _ponudeService = new APIService("Ponude");
        int _projektId;
        Projekt _projekt;
        Ponuda _aktivnaEkspertPonuda;
        public Projekt Projekt
        {
            get { return _projekt; }
            set { SetProperty(ref _projekt, value); }
        }

        public Ponuda AktivnaEkspertPonuda
        {
            get { return _aktivnaEkspertPonuda; }
            set { SetProperty(ref _aktivnaEkspertPonuda, value); }
        }

        public EkspertProjektDetailViewModel(int projektId)
        {
            _projektId = projektId;
            InitCommand = new Command(async () => await Init());
        }

        public ICommand InitCommand { get; set; }

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

        public ObservableCollection<Ponuda> PonudeList { get; set; } = new ObservableCollection<Ponuda>();

        public async Task Init()
        {
            //try - catch? if Projekt == null?
            Projekt = await _projektiService.GetById<Projekt>(_projektId);
            var ponude = await _ponudeService.Get<List<Ponuda>>(new PonudeSearchRequest
            {
                EkspertId = LoggedUser.logovaniKorisnik.KorisnikId,
                ProjektId = Projekt.ProjektId
            });
            if(ponude.Count > 0)
            {
                foreach(Ponuda ponuda in ponude)
                {
                    if (ponuda.Status != 1)
                        PonudeList.Add(ponuda);
                    else AktivnaEkspertPonuda = ponuda;
                }
            }
            if(AktivnaEkspertPonuda != null)
            {
                AktivnaPonudaVisible = true;
                NemaPonudeVisible = false;
            } else
            {
                AktivnaPonudaVisible = false;
                NemaPonudeVisible = true;
            }
            DetaljniOpisVisible = !string.IsNullOrWhiteSpace(Projekt.DetaljniOpis);
            DatumPocetkaVisible = !string.IsNullOrWhiteSpace(Projekt.DatumPocetka.ToString());
            if (!DatumPocetkaVisible)
            {
                DatumPocetkaAlternateVisible = true;
            }
            else
            {
                DatumPocetkaAlternateVisible = false;
            }
        }

        bool _aktivnaPonudaVisible;
        public bool AktivnaPonudaVisible
        {
            get { return _aktivnaPonudaVisible; }
            set { SetProperty(ref _aktivnaPonudaVisible, value); }
        }

        bool _nemaPonudeVisible;
        public bool NemaPonudeVisible
        {
            get { return _nemaPonudeVisible; }
            set { SetProperty(ref _nemaPonudeVisible, value); }
        }

        public async Task<bool> ObrisiAktivnuPonudu()
        {
            if (AktivnaEkspertPonuda != null)
            {
                var result = await _ponudeService.Delete<bool>(AktivnaEkspertPonuda.PonudaId);
                if (result) return true; else return false;
            }
            else return false;
        }
    }
}
