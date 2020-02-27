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
    public class ProjektiViewModel : BaseViewModel
    {

        public ProjektiViewModel()
        {
            InitCommand = new Command(async () => await Init());
        }

        private readonly APIService _projektiService = new APIService("Projekti");
        private readonly APIService _kategorijeService = new APIService("Kategorije");

        public ObservableCollection<Projekt> ProjektiList { get; set; } = new ObservableCollection<Projekt>();
        public ObservableCollection<Kategorija> KategorijeList { get; set; } = new ObservableCollection<Kategorija>();
        Kategorija _selectedKategorija = null;

        public Kategorija SelectedKategorija
        {
            get { return _selectedKategorija; }
            set
            {
                SetProperty(ref _selectedKategorija, value);
                if (value != null)
                {
                    InitCommand.Execute(null);
                }
            }
        }

        public ICommand InitCommand { get; set; }

        public async Task Init()
        {
            if (KategorijeList.Count == 0)
            {
                var kategorijeList = await _kategorijeService.Get<List<Kategorija>>(null);
                foreach (var kategorija in kategorijeList)
                {
                    KategorijeList.Add(kategorija);
                }
            }

            ProjektiList.Clear();
            List<Projekt> lista;
            if (SelectedKategorija != null)
            {
                ProjektiSearchRequest search = new ProjektiSearchRequest
                {
                    KategorijaId = SelectedKategorija.KategorijaId
                };

                lista = await _projektiService.Get<List<Projekt>>(search);
                foreach (var item in lista)
                {
                    ProjektiList.Add(item);

                }
            }
            else
            {
                //lista = await _proizvodiService.Get<List<Proizvod>>(null);
            }

        }
    }
}
