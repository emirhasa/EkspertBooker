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
    public class PoslodavciViewModel : BaseViewModel
    {
        public ObservableCollection<Poslodavac> PoslodavciList { get; set; }
        private APIService _poslodavciService = new APIService("Poslodavci");

        public PoslodavciViewModel()
        {
            PoslodavciList = new ObservableCollection<Poslodavac>();
            InitCommand = new Command(async () => await Init());   
        }

        public ICommand InitCommand { get; set; }

        public async Task<bool> Init ()
        {
            try
            {
                PoslodavciList.Clear();
                var list = await _poslodavciService.Get<List<Poslodavac>>(null);
                foreach (Poslodavac poslodavac in list)
                {
                    PoslodavciList.Add(poslodavac);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
