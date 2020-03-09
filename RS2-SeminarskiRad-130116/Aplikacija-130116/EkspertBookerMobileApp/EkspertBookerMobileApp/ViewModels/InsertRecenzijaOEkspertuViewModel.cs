using EkspertBooker.Model;
using EkspertBooker.Model.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EkspertBookerMobileApp.ViewModels
{
    public class InsertRecenzijaOEkspertuViewModel : BaseViewModel
    {
        private readonly APIService _projektiService = new APIService("Projekti");
        private readonly APIService _recenzijeOEkspertiService = new APIService("RecenzijeOEksperti");
        Projekt _projekt;
        int _projektId;
        public Projekt Projekt
        {
            get { return _projekt; }
            set { SetProperty(ref _projekt, value); }
        }
        public InsertRecenzijaOEkspertuViewModel(int projektId)
        {
            _projektId = projektId;
            InitCommand = new Command(async () => await Init());
        }

        public ICommand InitCommand { get; set; }

        public async Task Init()
        {
            //try - catch, if projekt stanje != zavrsen pop page or...? etc
            Projekt = await _projektiService.GetById<Projekt>(_projektId);
        }

        string _komentar;
        public string Komentar
        {
            get { return _komentar; }
            set { SetProperty(ref _komentar, value); }
        }

        int? _ocjena;
        public int? Ocjena
        {
            get { return _ocjena; }
            set { SetProperty(ref _ocjena, value); }
        }

        public async Task<bool> Submit()
        {
            if (IsValid())
            {
                RecenzijaOEkspertUpsertRequest request = new RecenzijaOEkspertUpsertRequest
                {
                    EkspertId = Projekt.EkspertId,
                    PoslodavacId = Projekt.PoslodavacId,
                    ProjektId = Projekt.ProjektId,
                    Komentar = Komentar,
                    Ocjena = (int)Ocjena
                };
                try
                {
                    var result = _recenzijeOEkspertiService.Insert<RecenzijaOEkspert>(request);
                    Application.Current.MainPage.DisplayAlert("Info", "Recenzija sačuvana!", "OK");
                    return true;
                }
                catch
                {
                    Application.Current.MainPage.DisplayAlert("Greška", "Problem prilikom slanja recenzije, žao nam je...", "OK");
                    return false;
                }
            }
            else return false;
        }

        //bool _errorVisible;
        //public bool ErrorVisible
        //{
        //    get { return _errorVisible; }
        //    set { SetProperty(ref _errorVisible, value); }
        //}
        public bool IsValid()
        {
            if(Ocjena >= 5 && Ocjena <= 10) {
                return true;
            } else
            {
                return false;
            }
        }
    }
}
