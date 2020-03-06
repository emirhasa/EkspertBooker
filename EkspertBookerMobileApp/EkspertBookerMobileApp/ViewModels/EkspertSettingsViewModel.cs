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
    public class EkspertSettingsViewModel : BaseViewModel
    {

        private APIService _kategorijeService = new APIService("Kategorije");
        private APIService _ekspertKategorijeService = new APIService("EkspertiKategorijePretplate");

        public ObservableCollection<CustomKategorija> KategorijeList { get; set; } = new ObservableCollection<CustomKategorija>();
        public EkspertSettingsViewModel()
        {
            InitCommand = new Command(async () => await Init());
        }

        public ICommand InitCommand { get; set; }

        public async Task Init()
        {
            KategorijeList.Clear();
            var kategorije = await _kategorijeService.Get<List<Kategorija>>(null);
            var custom_kategorije = new List<CustomKategorija>();
            foreach(Kategorija kategorija in kategorije)
            {
                custom_kategorije.Add(new CustomKategorija
                {
                    kategorija = kategorija,
                    isChecked = false,
                    checkboxColor = Color.LightSlateGray
                });
            }
            //optimizovati putem sortiranja nizova, pa inkrementarnog poređenja ili putem hashset
            var ekspert_pretplate = await _ekspertKategorijeService.Get<List<EkspertKategorijaPretplata>>(new EkspertKategorijeSearchRequest
            {
                EkspertId = LoggedUser.logovaniKorisnik.KorisnikId
            });

            foreach(var kategorija in custom_kategorije)
            {
                foreach(var pretplata in ekspert_pretplate)
                {
                    if (pretplata.KategorijaId == kategorija.kategorija.KategorijaId)
                    {
                        kategorija.isChecked = true;
                        kategorija.checkboxColor = Color.Red;
                        kategorija.EkspertPretplataId = pretplata.EkspertKategorijaPretplataId;
                    }
                }
            }

            foreach(var kategorija in custom_kategorije)
            {
                KategorijeList.Add(kategorija);
            }
        }

        public async Task UpdatePretplate()
        {
            Collection<int> nove_pretplate = new Collection<int>();
            foreach(var kategorija in KategorijeList)
            {
                if (kategorija.isChecked == true) nove_pretplate.Add(kategorija.kategorija.KategorijaId);
            }

            EkspertKategorijeUploadRequest request = new EkspertKategorijeUploadRequest
            {
                EkspertKategorije = nove_pretplate
            };
            try
            {
                var result = await _ekspertKategorijeService.Update<List<EkspertKategorijaPretplata>>(LoggedUser.logovaniKorisnik.KorisnikId, request);
                Application.Current.MainPage.DisplayAlert("Info", "Promjene sačuvane!", "OK");
            }
            catch(Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Greška", ex.Message, "Dalje...");
            }
        }
    }
}

public class CustomKategorija
{
    public Kategorija kategorija { get; set; }
    public bool isChecked { get; set; }
    public Color checkboxColor { get; set; }
    public int? EkspertPretplataId { get; set; }
}