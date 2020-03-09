using EkspertBooker.Model;
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

namespace EkspertBooker.DesktopAppUI.Projekt
{
    public partial class FormPonudePretraga : Form
    {
        private readonly APIService _serviceProjekti = new APIService("Projekti");
        private readonly APIService _servicePonude = new APIService("Ponude");

        public FormPonudePretraga()
        {
            InitializeComponent();
            dataGridViewPonude.AutoGenerateColumns = false;
            dataGridViewProjekti.AutoGenerateColumns = false;
        }

        private async void FormPonudePretraga_Load(object sender, EventArgs e)
        {

            dataGridViewProjekti.DataSource = await _serviceProjekti.Get<List<Model.Projekt>>(new ProjektiSearchRequest
            {
                StanjeId = "Licitacija"
            });


        }

        private async void dataGridViewProjekti_Click(object sender, EventArgs e)
        {
            if (int.TryParse(dataGridViewProjekti.CurrentRow.Cells[0].Value.ToString(), out int projekt_id))
            {
                //uspjesno parsan ekspert id, ucitaj recenzije

                var ponude_lista = await _servicePonude.Get<List<Model.Ponuda>>(new PonudeSearchRequest
                {
                    ProjektId = projekt_id
                });

                if (ponude_lista.Count == 0)
                {
                    MessageBox.Show("Projekt nema ponuda!");
                }

                dataGridViewPonude.DataSource = ponude_lista;
            }
        }

        private async void dataGridViewPonude_Click(object sender, EventArgs e)
        {
            if (int.TryParse(dataGridViewPonude.CurrentRow.Cells[0].Value.ToString(), out int ponuda_id))
            {
                //uspjesno parsan ekspert id, ucitaj recenzije

                var ponuda = await _servicePonude.GetById<Model.Ponuda>(ponuda_id);

                if (ponuda == null)
                {
                    MessageBox.Show("Greska u ucitanju ponude!");
                }
                else
                {
                    FormUrediPonudu forma = new FormUrediPonudu(ponuda_id);
                    forma.Show();
                }
            }
        }

        private void D(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
