using EkspertBooker.Model.Requests;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EkspertBooker.DesktopAppUI.Recenzije
{
    public partial class FormRecenzijaDetalji : Form
    {
        private readonly APIService _serviceProjekti = new APIService("Projekti");
        private readonly APIService _servicePoslodavci = new APIService("Poslodavci");
        private readonly APIService _serviceEksperti = new APIService("Eksperti");
        private readonly APIService _serviceKorisnici = new APIService("Korisnici");
        public FormRecenzijaDetalji()
        {
            InitializeComponent();
            comboBoxEksperti.DisplayMember = "Korisnik";
            comboBoxPoslodavci.DisplayMember = "Korisnik";
            comboBoxProjekti.DisplayMember = "Naziv";

            comboBoxEksperti.ValueMember = "EkspertId";
            comboBoxPoslodavci.ValueMember = "PoslodavacId";
            comboBoxProjekti.ValueMember = "ProjektId";
        }

        private void FormRecenzijaDetalji_Load(object sender, EventArgs e)
        {
            LoadComboBox();
        }

        private async void LoadComboBox()
        {
            List<Model.Projekt> lista_projekti = await _serviceProjekti.Get<List<Model.Projekt>>(null);
            List<Model.Poslodavac> lista_poslodavci = await _servicePoslodavci.Get<List<Model.Poslodavac>>(null);
            List<Model.Ekspert> lista_eksperti = await _serviceEksperti.Get<List<Model.Ekspert>>(null);

            foreach (var item in lista_eksperti)
            {
                item.Korisnik = await _serviceKorisnici.GetById<Model.Korisnik>(item.KorisnikUloga.KorisnikId);
            }

            foreach (var item in lista_poslodavci)
            {
                item.Korisnik = await _serviceKorisnici.GetById<Model.Korisnik>(item.KorisnikUloga.KorisnikId);
            }

            foreach (Model.Projekt item in lista_projekti)
            {
                comboBoxProjekti.Items.Add(item);
            }

            foreach (Model.Poslodavac item in lista_poslodavci)
            {
                comboBoxPoslodavci.Items.Add(item);
            }

            foreach (Model.Ekspert item in lista_eksperti)
            {
                comboBoxEksperti.Items.Add(item);
            }

        }

        private void numericUpDownOcjena_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(numericUpDownOcjena.Value.ToString()))
            {
                errorProvider.SetError(numericUpDownOcjena, Properties.Resources.errorObaveznoPolje);
                e.Cancel = true;
                return;
            }  else
            {
                errorProvider.SetError(numericUpDownOcjena, null);
            } 

            if(numericUpDownOcjena.Value < 1 || numericUpDownOcjena.Value > 10)
            {
                errorProvider.SetError(numericUpDownOcjena, "Ocjena mora biti izmedju 1 i 10");
                e.Cancel = true;
                return;
            } else
            {
                errorProvider.SetError(numericUpDownOcjena, null);
            }
        }

        private async void buttonSacuvaj_Click(object sender, EventArgs e)
        {
            if(ValidateChildren())
            {
                if (radioButtonEkspert.Checked == true)
                {
                    RecenzijaOEkspertUpsertRequest request = new RecenzijaOEkspertUpsertRequest();
                    if (!string.IsNullOrEmpty(textBoxKomentar.Text)) request.Komentar = textBoxKomentar.Text;
                    request.Ocjena = int.Parse(numericUpDownOcjena.Value.ToString());

                    Model.Projekt selected_projekt = (Model.Projekt)comboBoxProjekti.SelectedItem;
                    request.ProjektId = selected_projekt.ProjektId;

                    Model.Poslodavac selected_poslodavac = (Model.Poslodavac)comboBoxPoslodavci.SelectedItem;
                    request.PoslodavacId = selected_poslodavac.KorisnikId;

                    Model.Ekspert selected_ekspert = (Model.Ekspert)comboBoxEksperti.SelectedItem;
                    request.EkspertId = selected_ekspert.KorisnikId;

                    APIService recenzije = new APIService("RecenzijeOEksperti");
                    bool uspio = false;
                    try
                    {
                        var result = await recenzije.Insert<Model.RecenzijaOEkspert>(request);
                        if(result!= null) uspio = true;
                    } 
                    catch(FlurlHttpException ex)
                    {
                        if (ex.Call.HttpStatus == System.Net.HttpStatusCode.Conflict)
                        {
                            MessageBox.Show("Konflikt, vjerovatno vec POSTOJI recenzija o EKSPERTU za dati projekat!");
                        }
                    }
                    if (uspio) {
                        MessageBox.Show("Operacija uspjesna!");
                        Dispose(false);
                        FormRecenzijaDetalji forma = new FormRecenzijaDetalji();
                        forma.Show();
                    }
                    
                } else
                {
                    RecenzijaOPoslodavacUpsertRequest request = new RecenzijaOPoslodavacUpsertRequest();
                    if (!string.IsNullOrEmpty(textBoxKomentar.Text)) request.Komentar = textBoxKomentar.Text;
                    request.Ocjena = int.Parse(numericUpDownOcjena.Value.ToString());

                    Model.Projekt selected_projekt = (Model.Projekt)comboBoxProjekti.SelectedItem;
                    request.ProjektId = selected_projekt.ProjektId;

                    Model.Poslodavac selected_poslodavac = (Model.Poslodavac)comboBoxPoslodavci.SelectedItem;
                    request.PoslodavacId = selected_poslodavac.KorisnikId;

                    Model.Ekspert selected_ekspert = (Model.Ekspert)comboBoxEksperti.SelectedItem;
                    request.EkspertId = selected_ekspert.KorisnikId;

                    bool uspio = false;
                    APIService recenzije = new APIService("RecenzijeOPoslodavci");
                    try
                    {
                        var result = await recenzije.Insert<Model.RecenzijaOPoslodavac>(request);
                        if (result != null) uspio = true;
                    }
                    catch(FlurlHttpException ex)
                    {
                        if(ex.Call.HttpStatus == System.Net.HttpStatusCode.Conflict)
                        {
                            MessageBox.Show("Konflikt, vjerovatno vec POSTOJI recenzija o POSLODAVCU za dati projekat!");
                        }
                    }
                    if (uspio)
                    {
                        MessageBox.Show("Operacija uspjesna!");
                        Dispose(false);
                        FormRecenzijaDetalji forma = new FormRecenzijaDetalji();
                        forma.Show();
                    }
                }
            }
        }

        private void comboBoxProjekti_Validating(object sender, CancelEventArgs e)
        {
            if(comboBoxProjekti.SelectedIndex == -1)
            {
                errorProvider.SetError(comboBoxProjekti, Properties.Resources.errorObaveznoPolje);
                e.Cancel = true;
            } else
            {
                errorProvider.SetError(comboBoxProjekti, null);
            }
        }

        private void comboBoxEksperti_Validating(object sender, CancelEventArgs e)
        {
            if (comboBoxEksperti.SelectedIndex == -1)
            {
                errorProvider.SetError(comboBoxEksperti, Properties.Resources.errorObaveznoPolje);
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(comboBoxEksperti, null);
            }
        }

        private void comboBoxPoslodavci_Validating(object sender, CancelEventArgs e)
        {
            if (comboBoxPoslodavci.SelectedIndex == -1)
            {
                errorProvider.SetError(comboBoxPoslodavci, Properties.Resources.errorObaveznoPolje);
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(comboBoxPoslodavci, null);
            }
        }

        private void comboBoxEksperti_SelectedValueChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(comboBoxEksperti.SelectedValue.ToString());
        }
    }
}
