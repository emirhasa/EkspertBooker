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
    public class PoslodavacListaRecenzijaViewModel : BaseViewModel
    {
        int _poslodavacId;
        private APIService _recenzijeService = new APIService("RecenzijeOPoslodavci");
        public ObservableCollection<RecenzijaOPoslodavac> RecenzijeList { get; set; } = new ObservableCollection<RecenzijaOPoslodavac>();

        public PoslodavacListaRecenzijaViewModel(int poslodavacId)
        {
            _poslodavacId = poslodavacId;
            InitCommand = new Command(async () => await Init());
        }

        public ICommand InitCommand { get; set; }

        public async Task<bool> Init()
        {
            if(RecenzijeList.Count > 0) RecenzijeList.Clear();
            try
            {
                var recenzije_list = await _recenzijeService.Get<List<RecenzijaOPoslodavac>>(new RecenzijeOPoslodavciSearchRequest
                {
                    PoslodavacId = _poslodavacId
                });

                if (recenzije_list != null)
                {
                    ImaRecenzija = true;
                    NemaRecenzijaTooltip = false;
                    foreach (RecenzijaOPoslodavac recenzija in recenzije_list)
                    {
                        if (recenzija.Komentar == null) recenzija.Komentar = "Nema komentara";
                        RecenzijeList.Add(recenzija);
                    }
                } else
                {
                    ImaRecenzija = false;
                    NemaRecenzijaTooltip = true;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        bool _imaRecenzija;
        public bool ImaRecenzija
        {
            get { return _imaRecenzija; }
            set { SetProperty(ref _imaRecenzija, value); }
        }

        bool _nemaRecenzijaTooltip;

        public bool NemaRecenzijaTooltip
        {
            get { return _nemaRecenzijaTooltip; }
            set { SetProperty(ref _nemaRecenzijaTooltip, value); }
        }
    }
}
