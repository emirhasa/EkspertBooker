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
    public class ProjektPonudeViewModel : BaseViewModel
    {
        private readonly APIService _ponudeService = new APIService("Ponude");
        private readonly APIService _projektiService = new APIService("Projekti");
        int projektId;
        Projekt _projekt;
        bool _imaAktivnih = false;
        public bool ImaAktivnih
        {
            get { return _imaAktivnih; }
            set { SetProperty(ref _imaAktivnih, value); }
        }

        bool _imaOdbijenih = false;
        public bool ImaOdbijenih
        {
            get { return _imaOdbijenih; }
            set
            {
                SetProperty(ref _imaOdbijenih, value);
            }
        }


        bool _imaPrihvacena;
        public bool ImaPrihvacena
        {
            get { return _imaPrihvacena; }
            set { SetProperty(ref _imaPrihvacena, value);
            }
        }

        bool _nemaPrihvacena;
        public bool NemaPrihvacena
        {
            get { return _nemaPrihvacena; }
            set { SetProperty(ref _nemaPrihvacena, value); }
        }

        bool _nemaAktivnih;
        public bool NemaAktivnih
        {
            get { return _nemaAktivnih; }
            set { SetProperty(ref _nemaAktivnih, value); }
        }

        bool _nemaOdbijenih;

        public bool NemaOdbijenih
        {
            get { return _nemaOdbijenih; }
            set { SetProperty(ref _nemaOdbijenih, value); }
        }

        public Projekt Projekt
        {
            get { return _projekt; }
            set { SetProperty(ref _projekt, value); }
        }

        public ObservableCollection<Ponuda> PrihvacenaPonudaList { get; set; } = new ObservableCollection<Ponuda>();
        public ObservableCollection<Ponuda> AktivnePonudeList { get; set; } = new ObservableCollection<Ponuda>();
        public ObservableCollection<Ponuda> OdbijenePonudeList { get; set; } = new ObservableCollection<Ponuda>();

        Ponuda _prihvacenaPonuda;

        public Ponuda PrihvacenaPonuda
        {
            get { return _prihvacenaPonuda; }
            set { SetProperty(ref _prihvacenaPonuda, value); }
        }

        public ProjektPonudeViewModel(int _projektId)
        {
            projektId = _projektId;
            InitCommand = new Command(async () => await Init());
        }

        public ICommand InitCommand { get; set; }

        public async Task Init()
        {
            Projekt = await _projektiService.GetById<Projekt>(projektId);
            var prihvacena = await _ponudeService.Get<List<Ponuda>>(new PonudeSearchRequest
            {
                ProjektId = projektId,
                Status = 2
            });
            if (prihvacena.Count > 0)
            {
                PrihvacenaPonudaList.Clear();
                PrihvacenaPonuda = prihvacena[0];
                PrihvacenaPonudaList.Add(PrihvacenaPonuda);
                ImaPrihvacena = true;
                NemaPrihvacena = false;
            } else
            {
                ImaPrihvacena = false;
                NemaPrihvacena = true;
            }
        }

        public async Task UcitajAktivnePonude()
        {
            AktivnePonudeList.Clear();
            var aktivne_list = await _ponudeService.Get<List<Ponuda>>(new PonudeSearchRequest
            {
                ProjektId = projektId,
                Status = 1
            });
            if (aktivne_list.Count > 0)
            {
                ImaAktivnih = true;
                NemaAktivnih = false;
                foreach (Ponuda aktivna in aktivne_list)
                {
                    AktivnePonudeList.Add(aktivna);
                }
            } else
            {
                ImaAktivnih = false;
                NemaAktivnih = true;
            }
        }

        public async Task UcitajOdbijenePonude()
        {
            OdbijenePonudeList.Clear();
            var odbijene_list = await _ponudeService.Get<List<Ponuda>>(new PonudeSearchRequest
            {
                ProjektId = projektId,
                Status = 0
            });
            if (odbijene_list.Count > 0)
            {
                ImaOdbijenih = true;
                NemaOdbijenih = false;
                foreach (Ponuda odbijena in odbijene_list)
                {
                    OdbijenePonudeList.Add(odbijena);
                }
            } else
            {
                ImaOdbijenih = false;
                NemaOdbijenih = true;
            }
        }


    }
}
