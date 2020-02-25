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
        bool _postojePonude = false;
        public bool PostojePonude
        {
            get { return _postojePonude; }
            set { SetProperty(ref _postojePonude, value); }
        }
        public Projekt Projekt
        {
            get { return _projekt; }
            set { SetProperty(ref _projekt, value); }
        }
                
        public ObservableCollection<Ponuda> PonudeList { get; set; } = new ObservableCollection<Ponuda>();
        public ProjektPonudeViewModel(int _projektId)
        {
            projektId = _projektId;
            InitCommand = new Command(async () => await Init());
        }

        public ICommand InitCommand { get; set; }

        public async Task Init()
        {
            Projekt = await _projektiService.GetById<Projekt>(projektId);
            PonudeList.Clear();
            var list = await _ponudeService.Get<List<Ponuda>>(new PonudeSearchRequest
            {
                ProjektId = projektId
            });
            foreach(Ponuda ponuda in list)
            {
                PonudeList.Add(ponuda);
            }
            if(list.Count > 0)
            {
                PostojePonude = true;
            }
        }

    }
}
