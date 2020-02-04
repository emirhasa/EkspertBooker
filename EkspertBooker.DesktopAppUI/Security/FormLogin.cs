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

namespace EkspertBooker.DesktopAppUI.Security
{
    public partial class FormLogin : Form
    {
        APIService _service = new APIService("Korisnici");
        public FormLogin()
        {
            InitializeComponent();
        }

        private async void buttonLogin_Click(object sender, EventArgs e)
        {
            if (ValidateChildren())
            {
                try
                {
                    APIService.Username = textBoxUsername.Text;
                    APIService.Password = textBoxLozinka.Text;
                    await _service.GetById<dynamic>(null);
                    FormIndex frm = new FormIndex();
                    Dispose(false);
                    frm.Show();
                    MessageBox.Show("Uspjesan login!");
                }
                catch (FlurlHttpException er)
                {
                    if (er.Call.Response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        MessageBox.Show("Pogrešan password ili lozinka!", "Authentication", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show("Server error!", "Authentication", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBoxUsername_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxUsername.Text))
            {
                errorProvider.SetError(textBoxUsername, Properties.Resources.errorObaveznoPolje);
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(textBoxUsername, null);
            }
        }

        private void textBoxLozinka_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLozinka.Text))
            {
                errorProvider.SetError(textBoxLozinka, Properties.Resources.errorObaveznoPolje);
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(textBoxLozinka, null);
            }
        }
    }
}
