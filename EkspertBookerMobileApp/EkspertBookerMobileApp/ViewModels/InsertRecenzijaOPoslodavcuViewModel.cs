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
    public class InsertRecenzijaOPoslodavcuViewModel : BaseViewModel
    {
        private readonly APIService _projektiService = new APIService("Projekti");
        private readonly APIService _recenzijeOPoslodavciService = new APIService("RecenzijeOPoslodavci");
        Projekt _projekt;
        int _projektId;
        public Projekt Projekt
        {
            get { return _projekt; }
            set { SetProperty(ref _projekt, value); }
        }
        public InsertRecenzijaOPoslodavcuViewModel(int projektId)
        {
            _projektId = projektId;
            InitCommand = new Command(async () => await Init());
        }

        public ICommand InitCommand { get; set; }

        public async Task<bool> Init()
        {
            try
            {
                Projekt = await _projektiService.GetById<Projekt>(_projektId);
                if (Projekt != null) return true;
                else return false;
            }
            catch
            {
                return false;
            }
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
                RecenzijaOPoslodavacUpsertRequest request = new RecenzijaOPoslodavacUpsertRequest
                {
                    PoslodavacId = Projekt.PoslodavacId,
                    EkspertId = Projekt.EkspertId,
                    ProjektId = Projekt.ProjektId,
                    Komentar = Komentar,
                    Ocjena = (int)Ocjena
                };
                try
                {
                    var result = _recenzijeOPoslodavciService.Insert<RecenzijaOPoslodavac>(request);
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

        public bool IsValid()
        {
            try
            {
                if (Ocjena >= 5 && Ocjena <= 10)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            } catch
            {
                return false;
            }
        }

    }
}
