
using EkspertBooker.Model;
using EkspertBooker.Model.Requests;
using EkspertBookerMobileApp.Helper;
using EkspertBookerMobileApp.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EkspertBookerMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MojProfilEkspertPage : ContentPage
    {
        private MojProfilEkspertViewModel model;
        private MediaFile _profilnaSlika;
        private APIService _korisniciSlikeService = new APIService("KorisniciSlike");
        public MojProfilEkspertPage()
        {
            InitializeComponent();
            BindingContext = model = new MojProfilEkspertViewModel();
        }

        protected override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                await model.Init();
            }
            catch
            {
                Application.Current.MainPage.DisplayAlert("test", "test", "ok");
            }
            //if(model.TrenutniEkspert.EkspertStrucnaKategorija != null)
            //{
            //    Picker picker = StrucnaKategorijaPicker;
            //    foreach(var kategorija in picker.Items)
            //    {
            //        if(kategorija == model.TrenutniEkspert.EkspertStrucnaKategorija.Naziv)
            //        {
            //            picker.SelectedIndex = picker.Items.IndexOf(kategorija);
            //        }
            //    }
            //} 
        }

        private async void Uredi_Clicked(object sender, EventArgs e)
        {
            await PageScrollView.ScrollToAsync(UrediForm, ScrollToPosition.Start, true);
        }

        private async void Recenzije_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EkspertListaRecenzijaPage(LoggedUser.logovaniKorisnik.KorisnikId)); 
        }

        private async void Postavke_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EkspertSettingsPage());
        }
        private void ZahtjevZaRecenziju_Clicked(object sender, EventArgs e)
        {
            
        }

        private async void UrediSubmit_Clicked(object sender, EventArgs e)
        {

            var uspio = await model.SacuvajPromjene();
            if (uspio)
            {
                OnAppearing();
                Application.Current.MainPage.DisplayAlert("Info", "Promjene sačuvane!", "OK");
                EntryIme.Text = null;
                EntryPrezime.Text = null;
                EntryEmail.Text = null;
                EntryTelefon.Text = null;
                model.ValidationTriggered = false;
                UrediFormErrorLabel.IsVisible = false;
                SlikaErrorLabel.IsVisible = false;
                await PageScrollView.ScrollToAsync(PageScrollView, ScrollToPosition.Start, true);
            } else
            {
                UrediFormErrorLabel.IsVisible = true;
            }
            
        }

        private void Entry_TextChanged(object sender, EventArgs e)
        {
            if (model.ValidationTriggered)
            {
                if (!string.IsNullOrWhiteSpace(EntryIme.Text) || !string.IsNullOrWhiteSpace(EntryPrezime.Text)
                    || !string.IsNullOrWhiteSpace(EntryEmail.Text) || !string.IsNullOrWhiteSpace(EntryTelefon.Text))
                {
                    UrediFormErrorLabel.IsVisible = false;

                } else
                {
                    UrediFormErrorLabel.IsVisible = true;
                }
            }
        }

        private void EmailEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            Entry_TextChanged(sender, e);
            if (!string.IsNullOrWhiteSpace(EntryEmail.Text))
            {
                try
                {
                    string text = e.NewTextValue;
                    MailAddress email = new MailAddress(text);
                    MailErrorLabel.IsVisible = false;
                }
                catch
                {
                    MailErrorLabel.IsVisible = true;
                }
            } else
            {
                MailErrorLabel.IsVisible = false;
            }
        }

        private async void ButtonUploadSlike_Clicked(object sender, EventArgs e)
        {
            try
            {
                await CrossMedia.Current.Initialize();


                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await DisplayAlert("Info", "Photo Picker nije dostupan na uređaju!", "OK");
                    return;
                }

                _profilnaSlika = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    PhotoSize = PhotoSize.Custom,
                    CustomPhotoSize = 50,
                    CompressionQuality = 70,
                    SaveMetaData = true
                });

                if (_profilnaSlika == null)
                {
                    UploadImagePrikaz.Source = null;
                    ImageFrame.IsVisible = false;
                    if (SlikaErrorLabel.IsVisible) SlikaErrorLabel.IsVisible = false;
                    return;
                }
                else SlikaErrorLabel.IsVisible = false;

                if ((_profilnaSlika.GetStream().Length / 1024) > 1536)
                {
                    Application.Current.MainPage.DisplayAlert("Prevelika slika", "Vaša slika(iako kompresovana i smanjena)" +
                        " ima " + _profilnaSlika.GetStream().Length / 1024 + " KB, molimo uploadujte sliku manje veličine", "OK");
                    _profilnaSlika = null;
                    ImageFrame.IsVisible = false;
                    UploadImagePrikaz.Source = null;
                    return;
                }

                ImageFrame.IsVisible = true;
                UploadImagePrikaz.Source = ImageSource.FromStream(() =>
                {
                    if (_profilnaSlika != null)
                    {
                        return _profilnaSlika.GetStream();
                    }
                    else
                        return null;
                });
                
            } 
            catch(Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Greška", ex.Message, "OK");
            }
        }

        private async void ButtonTakePhoto_Clicked(object sender, EventArgs e)
        {
            try
            {
                await CrossMedia.Current.Initialize();

                if(!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("Info", "No camera available!", "OK");
                    return;
                }

                _profilnaSlika = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    SaveToAlbum = true,
                    AllowCropping = true,
                    PhotoSize = PhotoSize.Custom,
                    CustomPhotoSize = 50,
                    CompressionQuality = 70,
                    Directory = "EkspertBookerProfilne",
                    Name = "profilna_ekspert_" + LoggedUser.logovaniKorisnik.KorisnickoIme + ".jpg"
                });

                if (_profilnaSlika == null)
                {
                    UploadImagePrikaz.Source = null;
                    ImageFrame.IsVisible = false;
                    if (SlikaErrorLabel.IsVisible) SlikaErrorLabel.IsVisible = false;
                    return;
                }
                else SlikaErrorLabel.IsVisible = false;
                if ((_profilnaSlika.GetStream().Length / 1024) > 1536)
                {
                    Application.Current.MainPage.DisplayAlert("Prevelika slika", "Vaša slika(iako kompresovana i smanjena)" +
                        " ima " + _profilnaSlika.GetStream().Length / 1024 + " KB, molimo uploadujte sliku manje veličine", "OK");
                    _profilnaSlika = null;
                    ImageFrame.IsVisible = false;
                    UploadImagePrikaz.Source = null;
                    return;
                }
                ImageFrame.IsVisible = true;
                UploadImagePrikaz.Source = ImageSource.FromStream(() =>
                {
                    if (_profilnaSlika != null)
                    {
                        return _profilnaSlika.GetStream();
                    }
                    else return null;
                });
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Greška", ex.Message, "OK");
            }
        }

        private async void ButtonSubmitPhoto_Clicked(object sender, EventArgs e)
        {
            if (_profilnaSlika == null)
            {
                SlikaErrorLabel.IsVisible = true;
            } else
            {
                //upload image
                byte[] slika = MediaFileToByteArray.GetByteArray(_profilnaSlika);
                KorisnikSlikaUpsertRequest request = new KorisnikSlikaUpsertRequest
                {
                    KorisnikId = LoggedUser.logovaniKorisnik.KorisnikId,
                    ProfilnaSlika = slika
                };
                if (model.TrenutniKorisnik.KorisnikSlika != null)
                {
                    //put
                    try
                    {
                        await _korisniciSlikeService.Update<KorisnikSlika>(model.TrenutniKorisnik.KorisnikSlika.KorisnikId, request);
                    } 
                    catch(Exception ex)
                    {
                        await Application.Current.MainPage.DisplayAlert("Greška", ex.Message, "OK");
                        return;
                    }
                } else
                {
                    //post
                    try
                    {
                        await _korisniciSlikeService.Insert<KorisnikSlika>(request);
                    }
                    catch (Exception ex)
                    {
                        await Application.Current.MainPage.DisplayAlert("Greška", ex.Message, "OK");
                        return;
                    }
                }
                OnAppearing();
                await Application.Current.MainPage.DisplayAlert("Info", "Nova slika sačuvana!", "OK");
                UrediFormErrorLabel.IsVisible = false;
                _profilnaSlika = null;
                SlikaErrorLabel.IsVisible = false;
                ImageFrame.IsVisible = false;
                await PageScrollView.ScrollToAsync(PageScrollView, ScrollToPosition.Start, true);
            }
        }

        private void StrucnaKategorijaPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = StrucnaKategorijaPicker.SelectedItem as Kategorija;
            if (selected == null) return;
            if (model.TrenutniEkspert.EkspertStrucnaKategorija != null)
            {
                if (selected.KategorijaId != model.TrenutniEkspert.EkspertStrucnaKategorija.KategorijaId)
                {
                    UrediFormErrorLabel.IsVisible = false;
                }
            } else
            {
                UrediFormErrorLabel.IsVisible = false;
            }
        }
    }
}