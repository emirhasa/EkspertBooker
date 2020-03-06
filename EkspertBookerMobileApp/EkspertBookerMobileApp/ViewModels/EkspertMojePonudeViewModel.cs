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
    public class EkspertMojePonudeViewModel : BaseViewModel
    {
        private readonly APIService _ponudeService = new APIService("Ponude");
        string _title = "Moje ponude";
        public string Title
        {
            get { return _title; }
        }

        bool _prihvacenePonudeVisible = false;
        public bool PrihvacenePonudeVisible
        {
            get { return _prihvacenePonudeVisible; }
            set { SetProperty(ref _prihvacenePonudeVisible, value); }
        }

        bool _aktivnePonudeVisible = false;
        public bool AktivnePonudeVisible
        {
            get { return _aktivnePonudeVisible; }
            set { SetProperty(ref _aktivnePonudeVisible, value); }
        }

        bool _odbijenePonudeVisible = false;
        public bool OdbijenePonudeVisible
        {
            get { return _odbijenePonudeVisible; }
            set { SetProperty(ref _odbijenePonudeVisible, value); }
        }

        public ICommand PrihvacenePonudeCommand { get; set; }
        public ICommand AktivnePonudeCommand { get; set; }
        public ICommand OdbijenePonudeCommand { get; set; }
        public ICommand InitCommand { get; set; }

        public class PonudaStatic
        {
            public string Naziv = "Ponuda naziv";
            public string Opis = "Opis test, ovo je test opis ponude";
        }

        public ObservableCollection<Ponuda> AktivnePonudeList { get; set; } = new ObservableCollection<Ponuda>();
        public ObservableCollection<Ponuda> OdbijenePonudeList { get; set; } = new ObservableCollection<Ponuda>();
        public ObservableCollection<Ponuda> PrihvacenePonudeList { get; set; } = new ObservableCollection<Ponuda>();

        public EkspertMojePonudeViewModel()
        {
            InitCommand = new Command(async () => await Init());
            PrihvacenePonudeCommand = new Command(async () => await PrihvacenePonudeClick());
            AktivnePonudeCommand = new Command(async () => await AktivnePonudeClick());
            OdbijenePonudeCommand = new Command(async () => await OdbijenePonudeClick());
            PonudaStatic ponuda = new PonudaStatic();
        }

        public async Task Init()
        {
            //some logic
        }

        public async Task PrihvacenePonudeClick()
        {
            PrihvacenePonudeList.Clear();
            var prihvacene_ponude_list = await _ponudeService.Get<List<Ponuda>>(new PonudeSearchRequest
            {
                EkspertId = LoggedUser.logovaniKorisnik.KorisnikId,
                Status = 2
            });

            if (prihvacene_ponude_list.Count > 0)
            {
                foreach (var ponuda in prihvacene_ponude_list)
                {
                    PrihvacenePonudeList.Add(ponuda);
                }
                PrihvacenePonudeVisible = !PrihvacenePonudeVisible;
            } else
            {
                Application.Current.MainPage.DisplayAlert("Info", "Još nemate prihvaćenih ponuda... pošaljite ponudu na neki projekt, i sačekajte da je poslodavac odobri!", "OK");
            }
        }

        public async Task AktivnePonudeClick()
        {
            AktivnePonudeList.Clear();
            var aktivne_ponude_list = await _ponudeService.Get<List<Ponuda>>(new PonudeSearchRequest
            {
                EkspertId = LoggedUser.logovaniKorisnik.KorisnikId,
                Status = 1
            });

            if (aktivne_ponude_list.Count > 0) 
            {
                foreach (var ponuda in aktivne_ponude_list)
                {
                    AktivnePonudeList.Add(ponuda);
                }
                AktivnePonudeVisible = !AktivnePonudeVisible;
            } else
            {
                Application.Current.MainPage.DisplayAlert("Info", "Još nemate aktivnih ponuda... pošaljite ponudu na neki projekt i sačekajte odgovor poslodavca!", "OK");
            }
        }

        public async Task OdbijenePonudeClick()
        {
            OdbijenePonudeList.Clear();
            var odbijene_ponude_list = await _ponudeService.Get<List<Ponuda>>(new PonudeSearchRequest
            {
                EkspertId = LoggedUser.logovaniKorisnik.KorisnikId,
                Status = 0
            });

            if (odbijene_ponude_list.Count > 0)
            {
                foreach (var ponuda in odbijene_ponude_list)
                {
                    OdbijenePonudeList.Add(ponuda);
                }
                OdbijenePonudeVisible = !OdbijenePonudeVisible;
            } else
            {
                Application.Current.MainPage.DisplayAlert("Info", "Nemate odbijenih ponuda...!", "OK");
            }
        }
    }
}
