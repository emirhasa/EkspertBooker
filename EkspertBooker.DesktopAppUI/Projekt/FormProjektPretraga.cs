using EkspertBooker.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EkspertBooker.DesktopAppUI.Projekt
{
    public partial class FormProjektPretraga : Form
    {
        private readonly APIService _projektiService = new APIService("Projekti");
        private readonly APIService _serviceKategorije = new APIService("Kategorije");
        public FormProjektPretraga()
        {
            InitializeComponent();
        }

        private async void buttonPrikazi_Click(object sender, EventArgs e)
        {
            ProjektiSearchRequest request = new ProjektiSearchRequest();
            if(!string.IsNullOrWhiteSpace(textBoxNaziv.Text))
            {
                request.Naziv = textBoxNaziv.Text;
            }

            if(comboBoxKategorija.SelectedIndex != 0)
            {
                int _selectedKategorijaId = 0;
                if (comboBoxKategorija.SelectedIndex > 0)
                {
                    int.TryParse(comboBoxKategorija.SelectedValue.ToString(), out _selectedKategorijaId);
                    if (_selectedKategorijaId != 0)
                    {
                        request.KategorijaId = _selectedKategorijaId;
                    }
                }
            }

            if (checkBoxHitan.Checked == true)
            {
                request.Hitan = true;
            }
            else request.Hitan = null;
            
            var projekt_lista = await _projektiService.Get<List<Model.Projekt>>(null);
            dataGridViewProjekti.AutoGenerateColumns = false;
            dataGridViewProjekti.DataSource = projekt_lista;

        }

        private void dataGridViewProjekti_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var row_id = dataGridViewProjekti.SelectedCells[0].RowIndex;
            var selected_id = (int)dataGridViewProjekti.Rows[row_id].Cells[0].Value;
            if(selected_id > 0 )
            {
                FormProjektDetalji forma = new FormProjektDetalji(selected_id);
                forma.Show();
            } else
            {
                MessageBox.Show("Greska prilikom ucitanja projekt info");
                FormProjektDetalji forma = new FormProjektDetalji();
                forma.Show();
            }
        }

        private async void FormProjektPretraga_Load(object sender, EventArgs e)
        {
            await LoadKategorije();
        }

        private async Task LoadKategorije()
        {
            var result = await _serviceKategorije.Get<List<Model.Kategorija>>(null);
            result.Insert(0, new Model.Kategorija());
            comboBoxKategorija.DataSource = result;
            comboBoxKategorija.DisplayMember = "Naziv";
            comboBoxKategorija.ValueMember = "KategorijaId";
        }


    }
}
