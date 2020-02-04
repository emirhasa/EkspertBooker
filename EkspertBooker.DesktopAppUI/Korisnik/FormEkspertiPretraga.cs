using EkspertBooker.Model.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EkspertBooker.DesktopAppUI.Korisnik
{
    public partial class FormEkspertiPretraga : Form
    {
        private readonly APIService _serviceEksperti = new APIService("Eksperti");
        private readonly APIService _serviceRecenzije = new APIService("RecenzijeOEksperti");
        public FormEkspertiPretraga()
        {
            InitializeComponent();
            numericUpDownBrojProjekata.Enabled = false;
            dataGridViewRecenzije.AutoGenerateColumns = false;
            dataGridViewEksperti.AutoGenerateColumns = false;
        }

        private async void buttonPrikazi_Click(object sender, EventArgs e)
        {
            if(checkBoxUkljuciFilter.Checked == true)
            {
                if (int.TryParse(numericUpDownBrojProjekata.Value.ToString(), out int broj_zavrsenih))
                {
                    EkspertiSearchRequest request = new EkspertiSearchRequest
                    {
                        BrojZavrsenihProjekata = broj_zavrsenih
                    };
                    var result = await _serviceEksperti.Get<List<Model.Ekspert>>(request);
                    if (result != null)
                    {
                        dataGridViewEksperti.DataSource = result;
                    }
                }
                else MessageBox.Show("Ne moze se parsati vrijednost iz numeric up down?");
            } else
            {
                var result = await _serviceEksperti.Get<List<Model.Ekspert>>(null);
                if (result != null)
                {
                    dataGridViewEksperti.AutoGenerateColumns = false;
                    dataGridViewEksperti.DataSource = result;
                }
            }
        }

        private void checkBoxUkljuciFilter_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxUkljuciFilter.Checked == true)
            {
                numericUpDownBrojProjekata.Enabled = true;
            } else
            {
                numericUpDownBrojProjekata.Enabled = false;
            }
        }

        private async void dataGridViewEksperti_Click(object sender, EventArgs e)
        {
            if(int.TryParse(dataGridViewEksperti.SelectedRows[0].Cells[0].Value.ToString(), out int ekspert_id))
            {
                //uspjesno parsan ekspert id, ucitaj recenzije

                var recenzije_lista = await _serviceRecenzije.Get<List<Model.RecenzijaOEkspert>>(new RecenzijeOEkspertiSearchRequest
                {
                    EkspertId = ekspert_id
                });

                if(recenzije_lista.Count == 0) 
                {
                    MessageBox.Show("Korisnik jos nema recenzija!");
                }

                dataGridViewRecenzije.DataSource = recenzije_lista;
            }
        }
    }
}
