using EkspertBooker.Model;
using EkspertBookerMobileApp.Validation;
using EkspertBookerMobileApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EkspertBookerMobileApp.ViewModels
{
    public class ProjektUpsertViewModel : BaseViewModel
    {
        public ProjektUpsertViewModel(int? _projektId = null)
        {
            projektId = _projektId;
            InitCommand = new Command(async () => await Init());
            SubmitCommand = new Command(async () => await SubmitProjekt());
        }

        PoslodavacMainPage RootPage { get => Application.Current.MainPage as PoslodavacMainPage; }

        public int? projektId = null;
        string _title = "NOVI PROJEKAT";

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        public ObservableCollection<Kategorija> KategorijeList { get; set; } = new ObservableCollection<Kategorija>();
        private readonly APIService _kategorijeService = new APIService("Kategorije");
        private readonly APIService _projektiService = new APIService("Projekti");
        public ICommand InitCommand { get; set; }
        public ICommand SubmitCommand { get; set; }

        public bool _nazivErrorVisible = false;
        public bool _kratkiOpisErrorVisible = false;
        public bool _trajanjeErrorVisible = false;
        public bool _budzetErrorVisible = false;
        public bool _kategorijaErrorVisible = false;
        public bool _validationTriggered = false;

        public bool NazivErrorVisible
        {
            get { return _nazivErrorVisible; }
            set { SetProperty(ref _nazivErrorVisible, value); }
        }

        public bool KratkiOpisErrorVisible
        {
            get { return _kratkiOpisErrorVisible; }
            set { SetProperty(ref _kratkiOpisErrorVisible, value); }
        }

        public bool TrajanjeErrorVisible
        {
            get { return _trajanjeErrorVisible; }
            set { SetProperty(ref _trajanjeErrorVisible, value); }
        }

        public bool BudzetErrorVisible
        {
            get { return _budzetErrorVisible; }
            set { SetProperty(ref _budzetErrorVisible, value); }
        }

        public bool KategorijaErrorVisible
        {
            get { return _kategorijaErrorVisible; }
            set { SetProperty(ref _kategorijaErrorVisible, value); }
        }


        string _naziv = null;
        string _kratkiOpis = null;
        DateTime? _selectedDatumPocetka = null;
        int? _trajanje = null;
        int? _budzet = null;
        Kategorija _selectedKategorija = null;
        bool _hitan = false;
        string _detaljniOpis = null;

        public string Naziv
        {
            get { return _naziv; }
            set
            {
                SetProperty(ref _naziv, value);
            }
        }
        public string KratkiOpis
        {
            get { return _kratkiOpis; }
            set
            {
                SetProperty(ref _kratkiOpis, value);
            }
        }
        public DateTime MinDatumPocetka
        {
            get { return DateTime.Now; }
        }
        public DateTime MaxDatumPocetka
        {
            get { return DateTime.Now.AddDays(365); }
        }

        public DateTime? _datumObjave;
        public DateTime? DatumObjave
        {
            get { return _datumObjave; }
            set { SetProperty(ref _datumObjave, value); }
        }
        public DateTime? SelectedDatumPocetka
        {
            get { return _selectedDatumPocetka; }
            set
            {
                SetProperty(ref _selectedDatumPocetka, value);
            }
        }
        public int? Trajanje
        {
            get { return _trajanje; }
            set
            {
                SetProperty(ref _trajanje, value);
            }
        }
        public int? Budzet
        {
            get { return _budzet; }
            set
            {
                SetProperty(ref _budzet, value);
            }
        }
        public string DetaljniOpis
        {
            get { return _detaljniOpis; }
            set
            {
                SetProperty(ref _detaljniOpis, value);
            }
        }
        public Kategorija SelectedKategorija
        {
            get { return _selectedKategorija; }
            set
            {
                SetProperty(ref _selectedKategorija, value);
                KategorijaErrorVisible = false;
            }
        }

        public bool Hitan
        {
            get { return _hitan; }
            set
            {
                SetProperty(ref _hitan, value);
            }
        }

        public async Task Init()
        {
            if (KategorijeList.Count == 0)
            {
                var kategorije = await _kategorijeService.Get<List<Kategorija>>(null);
                foreach (Kategorija kategorija in kategorije)
                {
                    KategorijeList.Add(kategorija);
                }
            }
        }

        public async Task UcitajProjektInfo()
        {
            Title = "UREDI PROJEKAT";
            Projekt p = await _projektiService.GetById<Projekt>(projektId);
            Naziv = p.Naziv;
            KratkiOpis = p.KratkiOpis;
            DetaljniOpis = p.DetaljniOpis;
            Trajanje = p.TrajanjeDana;
            Budzet = p.Budzet;
            DatumObjave = p.DatumObjave;
            SelectedDatumPocetka = p.DatumPocetka;
            if (p.Hitan == true) Hitan = true; else Hitan = false;
            SelectedKategorija = await _kategorijeService.GetById<Kategorija>(p.KategorijaId);
            //SelectedKategorijaIndex = p.KategorijaId;
        }

        public async Task<bool> SubmitProjekt()
        {
            if (IsValid())
            {
                Projekt submit_projekt = new Projekt
                {
                    Naziv = Naziv,
                    KratkiOpis = KratkiOpis,
                    DatumPocetka = SelectedDatumPocetka,
                    TrajanjeDana = Trajanje,
                    Budzet = Budzet,
                    KategorijaId = SelectedKategorija.KategorijaId,
                    Hitan = Hitan,
                    PoslodavacId = LoggedUser.logovaniKorisnik.KorisnikId,
                    DatumObjave = DatumObjave,
                    StanjeId = "Licitacija"
                };
                if (!string.IsNullOrWhiteSpace(DetaljniOpis)) submit_projekt.DetaljniOpis = DetaljniOpis;
                try
                {
                    if (projektId == null)
                    {
                        //novi projekat
                        submit_projekt.DatumObjave = DateTime.Now;
                        Projekt submit = await _projektiService.Insert<Projekt>(submit_projekt);
                        await Application.Current.MainPage.DisplayAlert("Info", "Projekat uspješno dodan!", "Dalje...");
                        Application.Current.MainPage = new PoslodavacMainPage();
                        return true;
                    } else
                    {
                        //uredi postojeci
                        Projekt submit = await _projektiService.Update<Projekt>(projektId, submit_projekt);
                        await Application.Current.MainPage.DisplayAlert("Info", "Projekat uspješno uređen!", "Dalje...");
                        Application.Current.MainPage = new PoslodavacMainPage();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Application.Current.MainPage.DisplayAlert("Greška", ex.Message, "Dalje...");
                    return false;
                }
            } else
            {
                Application.Current.MainPage.DisplayAlert("Greška", "Unesite obavezna polja", "Dalje...");
                return false;
            }
        }


        public bool IsValid()
        {
            _validationTriggered = true;
            bool pronadjena_greska = false;

            if(!ValidationRules.IsNullOrEmptyRule(_naziv))
            {
                NazivErrorVisible = true;
                pronadjena_greska = true;
            }

            if(!ValidationRules.IsNullOrEmptyRule(_kratkiOpis))
            {
                KratkiOpisErrorVisible = true;
                pronadjena_greska = true;
            }

            if(!ValidationRules.IsNullOrEmptyRule(_trajanje.ToString()))
            {
                TrajanjeErrorVisible = true;
                pronadjena_greska = true;
            }

            if(!ValidationRules.IsNullOrEmptyRule(_budzet.ToString()))
            {
                BudzetErrorVisible = true;
                pronadjena_greska = true;
            }

            if (int.TryParse(Budzet.ToString(), out int budzet))
            {
                if (budzet < 10 || budzet > 500000)
                {
                    BudzetErrorVisible = true;
                    pronadjena_greska = true;
                }
            }
            else
            {
                BudzetErrorVisible = true;
                pronadjena_greska = true;
            }

            if (_selectedKategorija == null )
            {
                KategorijaErrorVisible = true;
                pronadjena_greska = true;           
            } else
            {
                if (_selectedKategorija.KategorijaId == null)
                {
                    KategorijaErrorVisible = true;
                    pronadjena_greska = true;
                }
            }
            return !pronadjena_greska;
        }

    }
}
