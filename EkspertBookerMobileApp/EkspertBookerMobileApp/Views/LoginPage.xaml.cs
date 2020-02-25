using EkspertBookerMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EkspertBookerMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private LoginViewModel model;
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = model = new LoginViewModel();
        }

        private void Username_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (model._validationTriggered)
            {
                if (!string.IsNullOrWhiteSpace(e.NewTextValue))
                {
                    UsernameErrorLabel.IsVisible = false;
                } else
                {
                    UsernameErrorLabel.IsVisible = true;
                }
            }
            return;
        }

        private void Password_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (model._validationTriggered)
            {
                if(!string.IsNullOrWhiteSpace(e.NewTextValue))
                {
                    PasswordErrorLabel.IsVisible = false;
                } else
                {
                    PasswordErrorLabel.IsVisible = true;
                }
            }
            return;
        }

    }
}