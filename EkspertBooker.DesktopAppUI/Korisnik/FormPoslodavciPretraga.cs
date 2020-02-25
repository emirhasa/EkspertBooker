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
    public partial class FormPoslodavciPretraga : Form
    {
        private readonly APIService _servicePoslodavci = new APIService("Poslodavci");
        private readonly APIService _serviceRecenzije = new APIService("RecenzijeOPoslodavci");
        public FormPoslodavciPretraga()
        {
            InitializeComponent();
            numericUpDownBrojProjekata.Enabled = false;
            dataGridViewRecenzije.AutoGenerateColumns = false;
            dataGridViewPoslodavci.AutoGenerateColumns = false;
        }

        private async void buttonPrikazi_Click(object sender, EventArgs e)
        {
            if (checkBoxUkljuciFilter.Checked == true)
            {
                if (int.TryParse(numericUpDownBrojProjekata.Value.ToString(), out int broj_zavrsenih))
                {
                    PoslodavciSearchRequest request = new PoslodavciSearchRequest
                    {
                        BrojZavrsenihProjekata = broj_zavrsenih
                    };
                    var result = await _servicePoslodavci.Get<List<Model.Poslodavac>>(request);
                    if (result.Count > 0)
                    {
                        dataGridViewPoslodavci.DataSource = result;
                    }
                }
                else MessageBox.Show("Ne moze se parsati vrijednost iz numeric up down?");

            }
            else
            {
                var result = await _servicePoslodavci.Get<List<Model.Poslodavac>>(null);
                if (result.Count > 0)
                {
                    dataGridViewPoslodavci.DataSource = result;
                }
            }
        }

        private void checkBoxUkljuciFilter_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBoxUkljuciFilter.Checked == true)
            {
                numericUpDownBrojProjekata.Enabled = true;
            }
            else
            {
                numericUpDownBrojProjekata.Enabled = false;
            }

        }

        private async void dataGridViewPoslodavci_Click(object sender, EventArgs e)
        {

            if (int.TryParse(dataGridViewPoslodavci.CurrentRow.Cells[0].Value.ToString(), out int poslodavac_id))
            {
                //uspjesno parsan ekspert id, ucitaj recenzije

                var recenzije_lista = await _serviceRecenzije.Get<List<Model.RecenzijaOPoslodavac>>(new RecenzijeOPoslodavciSearchRequest
                {
                    PoslodavacId = poslodavac_id
                });

                if (recenzije_lista.Count == 0)
                {
                    MessageBox.Show("Korisnik jos nema recenzija!");
                }

                dataGridViewRecenzije.DataSource = recenzije_lista;
            }
        }
    }
}
