using EkspertBooker.Model;
using EkspertBooker.Model.Requests;
using EkspertBookerMobileApp.ViewModels;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EkspertBookerMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProjektDetailDetaljnoPage : ContentPage
    {
        private ProjektDetailDetaljnoViewModel model;
        FileData _uploadedFile = null;
        string _fileExtension = null;
        string _fileNaziv = null;
        byte[] _fileContent = null;

        private APIService _projektDetaljiPriloziService = new APIService("ProjektDetaljiPrilozi");
        private APIService _ponudeService = new APIService("Ponude");
        public ProjektDetailDetaljnoPage(int projektId)
        {
            InitializeComponent();
            BindingContext = model = new ProjektDetailDetaljnoViewModel(projektId);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await model.Init();
        }

        private async void UrediButton_Clicked(object sender, EventArgs e)
        {
            PageScrollView.ScrollToAsync(UrediForm, ScrollToPosition.Start, true);
        }

        private async void NoviPrilog_Clicked(object sender, EventArgs e)
        {
            PageScrollView.ScrollToAsync(PrilogForm, ScrollToPosition.Start, true);
        }

        private async void PrihvacenaPonuda_Clicked(object sender, EventArgs e)
        {
            var prihvacena_ponuda = await _ponudeService.Get<Ponuda>(new PonudeSearchRequest
            {
                ProjektId = model.Projekt.ProjektId,
                Status = 2
            });
            if (prihvacena_ponuda == null)
            {
                Application.Current.MainPage.DisplayAlert("Greška", "Problem prilikom učitavanja prihvaćene ponude za projekt", "OK");
            } else
            {
                Navigation.PushAsync(new PonudaDetaljiPage(prihvacena_ponuda.PonudaId));
            }
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(model.ValidationTriggered)
            {
                if(!string.IsNullOrWhiteSpace(e.NewTextValue))
                {
                    UrediFormErrorLabel.IsVisible = false;
                } else
                {
                    UrediFormErrorLabel.IsVisible = true;
                }
            }
        }

        private async void ButtonPotvrdiPromjene_Clicked(object sender, EventArgs e)
        {
            var uspio = await model.SacuvajPromjene();
            if(uspio)
            {
                Application.Current.MainPage.DisplayAlert("Info", "Promjene uspješno sačuvane!", "OK");
                var page_refresh = new ProjektDetailDetaljnoPage(model.Projekt.ProjektId);
                Navigation.InsertPageBefore(page_refresh, this);
                Navigation.PopAsync();
            } else
            {
                UrediFormErrorLabel.IsVisible = true;
            }
        }

        private async void ButtonUploadPrilog_Clicked(object sender, EventArgs e)
        {
            try
            {
                UploadPrilogErrorLabel.IsVisible = false;
                FileData fileData = await CrossFilePicker.Current.PickFile();
                if (fileData == null)
                {
                    EntryPrilogNaziv.Text = null;
                    PrilogOriginalniNaziv.Text = null;
                    _uploadedFile = null;
                    _fileContent = null;
                    _fileExtension = null;
                    _fileNaziv = null;
                    EntryPrilogNaziv.IsEnabled = false;
                    return; // user canceled file picking
                }
                else
                {
                    string fileName = fileData.FileName;
                    _uploadedFile = fileData;
                    _fileContent = fileData.DataArray;
                    _fileExtension = Path.GetExtension(fileName);
                    _fileNaziv = Path.GetFileNameWithoutExtension(fileName);
                    PrilogOriginalniNaziv.Text = _fileNaziv;
                    EntryPrilogNaziv.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Info", "Izuzetak prilikom biranja datoteke: " + ex.ToString(), "OK");
            }
        }

        private async void ButtonDownloadPrilog_Clicked(object sender, EventArgs e)
        {
            ProjektDetaljiPrilogSearchRequest search = new ProjektDetaljiPrilogSearchRequest
            {
                ProjektDetaljiId = model.ProjektDetalji.ProjektId,
                GetOnlyMetaData = false
            };
            Device.OpenUri(new Uri(APIService._apiUrl + "/ProjektDetaljiPrilozi/" + "?" + await search.ToQueryString()));
        }

        private async void ButtonPotvrdiPrilogUpload_Clicked(object sender, EventArgs e)
        {
            //todo: staviti u funkciju
            if (_uploadedFile != null)
            {
                UploadPrilogErrorLabel.IsVisible = false;
                ProjektDetaljiPrilogUpsertRequest request = new ProjektDetaljiPrilogUpsertRequest
                {
                    Prilog = _fileContent,
                    ProjektDetaljiId = model.ProjektDetalji.ProjektId,
                    PrilogEkstenzija = _fileExtension
                };
                if(!string.IsNullOrWhiteSpace(EntryPrilogNaziv.Text))
                {
                    request.PrilogNaziv = EntryPrilogNaziv.Text;
                } else
                {
                    request.PrilogNaziv = _fileNaziv;
                }
                try
                {
                    var result = await _projektDetaljiPriloziService.Update<ProjektDetaljiPrilog>(model.ProjektDetalji.ProjektId, request);
                    Application.Current.MainPage.DisplayAlert("Info", "Prilog uspješno uploadovan!", "OK");
                    Navigation.InsertPageBefore(new ProjektDetailDetaljnoPage(model.Projekt.ProjektId), this);
                    Navigation.PopAsync();
                }
                catch(Exception ex)
                {
                    Application.Current.MainPage.DisplayAlert("Greška", ex.Message, "OK");
                }
            } else
            {
                UploadPrilogErrorLabel.IsVisible = true;
            }
        }
    }
}