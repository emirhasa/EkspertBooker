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
    public class EkspertiViewModel : BaseViewModel
    {
        public ObservableCollection<Ekspert> EkspertiList { get; set; }
        public APIService ekspertiService = new APIService("Eksperti");
        public EkspertiViewModel()
        {
            EkspertiList = new ObservableCollection<Ekspert>();
            InitCommand = new Command(async () => await Init());
        }

        public ICommand InitCommand { get; set; }

        public async Task Init()
        {
            EkspertiList.Clear();
            var list = await ekspertiService.Get<List<Ekspert>>(null);
            if (list != null)
            {
                foreach (var ekspert in list)
                {
                    EkspertiList.Add(ekspert);
                }
            }
        }
    }
}
