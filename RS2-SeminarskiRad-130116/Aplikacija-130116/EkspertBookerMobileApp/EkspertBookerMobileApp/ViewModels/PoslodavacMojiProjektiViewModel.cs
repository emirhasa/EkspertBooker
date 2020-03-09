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
    public class PoslodavacMojiProjektiViewModel : BaseViewModel
    {
        private readonly APIService _projektiService = new APIService("Projekti");
        private readonly APIService _notifikacijePoslodavciService = new APIService("NotifikacijePoslodavci");

        public ObservableCollection<NotifikacijaPoslodavac> ObavijestiList { get; set; } = new ObservableCollection<NotifikacijaPoslodavac>();
        public ObservableCollection<Projekt> AktivniProjektiList { get; set; } = new ObservableCollection<Projekt>();
        public ObservableCollection<Projekt> LicitacijaProjektiList { get; set; } = new ObservableCollection<Projekt>();
        public ObservableCollection<Projekt> ZavrseniProjektiList { get; set; } = new ObservableCollection<Projekt>();


        public ICommand PrikaziObavijestiCommand { get; set; }
        public ICommand AktivniProjektiCommand { get; set; }
        public ICommand LicitacijaProjektiCommand { get; set; }
        public ICommand ZavrseniProjektiCommand { get; set; }



        bool _obavijestiVisible = false;
        public bool ObavijestiVisible
        {
            get { return _obavijestiVisible; }
            set { SetProperty(ref _obavijestiVisible, value); }
        }

        bool _aktivniProjektiVisible;
        public bool AktivniProjektiVisible
        {
            get { return _aktivniProjektiVisible; }
            set { SetProperty(ref _aktivniProjektiVisible, value); }
        }

        bool _licitacijaProjektiVisible;
        public bool LicitacijaProjektiVisible
        {
            get { return _licitacijaProjektiVisible; }
            set { SetProperty(ref _licitacijaProjektiVisible, value); }
        }

        bool _zavrseniProjektiVisible;
        public bool ZavrseniProjektiVisible
        {
            get { return _zavrseniProjektiVisible; }
            set { SetProperty(ref _zavrseniProjektiVisible, value); }
        }

        public PoslodavacMojiProjektiViewModel()
        {
            PrikaziObavijestiCommand = new Command(async () => await PrikaziObavijestiClick());
            AktivniProjektiCommand = new Command(async () => await UcitajAktivneProjekte());
            LicitacijaProjektiCommand = new Command(async () => await UcitajLicitacijaProjekte());
            ZavrseniProjektiCommand = new Command(async () => await UcitajZavrseneProjekte());
        }

        public async Task PrikaziObavijestiClick()
        {
            ObavijestiList.Clear();
            var obavijesti_list = await _notifikacijePoslodavciService.Get<List<NotifikacijaPoslodavac>>(new NotifikacijePoslodavciSearchRequest
            {
                PoslodavacId = LoggedUser.logovaniKorisnik.KorisnikId,
            });

            if (!ObavijestiVisible)
            {
                if (obavijesti_list.Count > 0)
                {
                    foreach (var obavijest in obavijesti_list)
                    {
                        ObavijestiList.Add(obavijest);
                    }
                    ObavijestiVisible = !ObavijestiVisible;
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Info", "Trenutno nemate nijednu obavijest!", "OK");
                }
            }
            else ObavijestiVisible = !ObavijestiVisible;
        }

        public async Task UcitajAktivneProjekte()
        {
            if (AktivniProjektiVisible)
            {
                AktivniProjektiVisible = false;
                AktivniProjektiList.Clear();
            }
            else
            {
                var aktivni_projekti = await _projektiService.Get<List<Projekt>>(new ProjektiSearchRequest
                {
                    PoslodavacId = LoggedUser.logovaniKorisnik.KorisnikId,
                    StanjeId = "Aktivan"
                });
                if (aktivni_projekti.Count > 0)
                {
                    foreach (var projekat in aktivni_projekti)
                    {
                        AktivniProjektiList.Add(projekat);
                    }
                    AktivniProjektiVisible = true;
                } else
                {
                    Application.Current.MainPage.DisplayAlert("Info", "Trenutno nemate aktivnih projekata! Prihvatite ponudu na projektu da postane aktivan...", "OK");
                }
            }
        }

        public async Task UcitajLicitacijaProjekte()
        {
            if (LicitacijaProjektiVisible)
            {
                LicitacijaProjektiVisible = false;
                LicitacijaProjektiList.Clear();
            }
            else
            {
                var licitacija_projekti = await _projektiService.Get<List<Projekt>>(new ProjektiSearchRequest
                {
                    PoslodavacId = LoggedUser.logovaniKorisnik.KorisnikId,
                    StanjeId = "Licitacija"
                });
                if (licitacija_projekti.Count > 0)
                {
                    foreach (var projekat in licitacija_projekti)
                    {
                        LicitacijaProjektiList.Add(projekat);
                    }
                    LicitacijaProjektiVisible = true;
                } else
                {
                    Application.Current.MainPage.DisplayAlert("Info", "Trenutno nemate licitacija(bidding - ponude) projekata, sačekajte da neki ekspert dostavi ponudu...", "OK");
                }
            }
        }

        public async Task UcitajZavrseneProjekte()
        {
            if (ZavrseniProjektiVisible)
            {
                ZavrseniProjektiVisible = false;
                ZavrseniProjektiList.Clear();
            }
            else
            {
                var zavrseni_projekti = await _projektiService.Get<List<Projekt>>(new ProjektiSearchRequest
                {
                    PoslodavacId = LoggedUser.logovaniKorisnik.KorisnikId,
                    StanjeId = "Zavrsen"
                });
                if (zavrseni_projekti.Count > 0)
                {
                    foreach (var projekat in zavrseni_projekti)
                    {
                        ZavrseniProjektiList.Add(projekat);
                    }
                    ZavrseniProjektiVisible = true;
                } else
                {
                    Application.Current.MainPage.DisplayAlert("Info", "Trenutno nemate završenih projekata! Označite aktivan projekt završenim...", "Dalje");
                }
            }
        }
        
    }
}
