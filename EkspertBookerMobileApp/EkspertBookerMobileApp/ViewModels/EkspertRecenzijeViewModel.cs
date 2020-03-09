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
    public class EkspertRecenzijeViewModel : BaseViewModel
    {
        int _ekspertId;
        private APIService _recenzijeService = new APIService("RecenzijeOEksperti");

        public ObservableCollection<RecenzijaOEkspert> RecenzijeList { get; set; } = new ObservableCollection<RecenzijaOEkspert>();
        public EkspertRecenzijeViewModel(int ekspertId)
        {
            _ekspertId = ekspertId;
            InitCommand = new Command(async () => await Init());
        }

        public ICommand InitCommand { get; set; }

        public async Task Init() {
            if (RecenzijeList.Count > 0) RecenzijeList.Clear();
            var recenzije_list = await _recenzijeService.Get<List<RecenzijaOEkspert>>(new RecenzijeOEkspertiSearchRequest
            {
                EkspertId = _ekspertId
            });
            if (recenzije_list.Count > 0)
            {
                ImaRecenzija = true;
                NemaRecenzijaTooltip = false;
                foreach (RecenzijaOEkspert recenzija in recenzije_list)
                {
                    if (string.IsNullOrWhiteSpace(recenzija.Komentar)) recenzija.Komentar = "Nema komentara";
                    RecenzijeList.Add(recenzija);
                }

            } else
            {
                ImaRecenzija = false;
                NemaRecenzijaTooltip = true;
            }
        }


        bool _imaRecenzija;
        public bool ImaRecenzija
        {
            get { return _imaRecenzija; }
            set { SetProperty(ref _imaRecenzija, value); }
        }

        //trebalo bi rijesiti u samom page
        bool _nemaRecenzijaTooltip;
        public bool NemaRecenzijaTooltip
        {
            get { return _nemaRecenzijaTooltip; }
            set { SetProperty(ref _nemaRecenzijaTooltip, value); }
        }
    }
}
