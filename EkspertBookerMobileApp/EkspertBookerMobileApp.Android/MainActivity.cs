using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.Content;
using Android;
using Android.Support.V4.App;
using static Android.Support.V4.App.ActivityCompat;
using Android.Util;
using Android.Support.Design.Widget;
using Android.Content;

namespace EkspertBookerMobileApp.Droid
{

    [Activity(Label = "EkspertBookerMobileApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IOnRequestPermissionsResultCallback
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
            Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
            if (!(ContextCompat.CheckSelfPermission(this, Manifest.Permission.Camera) == (int)Permission.Granted && ContextCompat.CheckSelfPermission(this, Manifest.Permission.WriteExternalStorage) == (int)Permission.Granted))
            {
                ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.Camera, Manifest.Permission.WriteExternalStorage }, 1);
            }

            //check permissions for read external storage and internet
            //these permissions are enabled by default in debug mode. for release, this needs to be explicitly requested

            //if (!(ContextCompat.CheckSelfPermission(this, Manifest.Permission.ReadExternalStorage) == (int)Permission.Granted))
            //{
            //    ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.ReadExternalStorage }, 2);
            //}


        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            if (requestCode == 1)
            {
                string poruka = null;
                // Received permission result for camera & storage permission.

                if (grantResults[0] == Permission.Granted)
                {
                    //kamera odobrena
                    poruka+= "Kamera permisija odobrena, možete koristiti kameru za novu profilnu sliku! ";
                } else
                {
                    poruka += "Kamera permisija nije odobrena, NE možete koristiti kameru za novu profilnu sliku! ";
                }

                if (grantResults[1] == Permission.Granted)
                {
                    //galerija odobrena
                    poruka += "Storage permisija odobrena, možete učitati profilnu sliku iz galerije! ";
                }
                else
                {
                    poruka += "Storage permisija nije odobrena, NE možete učitati profilnu sliku iz galerije! ";
                }

                Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                AlertDialog alert = dialog.Create();
                alert.SetTitle("Permisija");
                alert.SetMessage(poruka);
                alert.SetButton("OK", (c, ev) =>
                {
                    // Ok button click task  
                });
                if (grantResults[0] == Permission.Denied || grantResults[1] == Permission.Denied)
                {
                    alert.SetButton2("Odobri permisije opet?", (c, ev) =>
                    {
                        ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.Camera, Manifest.Permission.WriteExternalStorage }, 1);
                    });
                    alert.Show();
                } else
                {
                    alert.Show();
                }
            }
        }
    }
}