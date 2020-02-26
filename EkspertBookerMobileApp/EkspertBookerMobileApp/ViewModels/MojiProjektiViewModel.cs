using EkspertBooker.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EkspertBookerMobileApp.ViewModels
{
    public class MojiProjektiViewModel : BaseViewModel
    {
        private readonly APIService _projektiService = new APIService("Projekti");
        public ObservableCollection<Projekt> MojiProjektiList { get; set; } = new ObservableCollection<Projekt>();

        public ICommand InitCommand { get; set; }
        public ICommand MyCommand { get; set; }

        public MojiProjektiViewModel()
        {
            InitCommand = new Command(async () => await Init());
            MyCommand = new Command(async () => await Command());
        }

        public async Task Init()
        {
            MojiProjektiList.Clear();
            var projekti = await _projektiService.Get<List<Projekt>>(new ProjektiSearchRequest { PoslodavacId = LoggedUser.logovaniKorisnik.KorisnikId });
            foreach(Projekt projekt in projekti)
            {
                MojiProjektiList.Add(projekt);
            }
        }

        public async Task Command()
        {
            //Application.Current.MainPage.DisplayAlert("Clicked", "CV image clicked", "Great!");
        }
        
    }
}
