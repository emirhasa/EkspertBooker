using EkspertBooker.Model;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EkspertBookerMobileApp
{   
    public class APIService
    {
        private readonly string _route;

        public static string Username { get; set; }
        public static string Password { get; set; }

#if DEBUG
        private string _apiUrl = "http://localhost:55518/api";
#endif
#if RELEASE
    private string _apiUrl = "https://test.com";
#endif

        public APIService(string route)
        {
            _route = route;
            if(DeviceInfo.Platform == DevicePlatform.Android)
            {
                if (DeviceInfo.DeviceType == DeviceType.Physical)
                {
                    //android device
                    _apiUrl = "http://192.168.0.11:55518/api";
                }
                else
                {
                    //android emulator
                    _apiUrl = "http://10.0.2.2:55518/api";
                }
            }
        }

        public async Task<T> Get<T>(object search)
        {
            var url = $"{_apiUrl}/{_route}";

            try
            {
                if (search != null)
                {
                    url += "?";
                    url += await search.ToQueryString();
                }

                return await url.WithBasicAuth(Username, Password).GetJsonAsync<T>();
            }
            catch (FlurlHttpException ex)
            {
                if (ex.Call.HttpStatus == System.Net.HttpStatusCode.Unauthorized)
                {
                    //MessageBox.Show("Niste authentificirani");
                    await Application.Current.MainPage.DisplayAlert("Greška", "Niste authentificirani/pogrešan Username ili Lozinka", "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Greška", "Server error", "OK");
                }
                throw;
            }
        }


        public async Task<T> Insert<T>(object insert)
        {
            var url = $"{_apiUrl}/{_route}";
            var result = await url.WithBasicAuth(Username, Password).PostJsonAsync(insert).ReceiveJson<T>();
            return result;
        }

        public async Task<T> GetById<T>(object id)
        {
            var url = $"{_apiUrl}/{_route}/{id}";
            //url += id.ToQueryString(); //Dodavanje query parametara u GET request
            var result = await url.WithBasicAuth(Username, Password).GetJsonAsync<T>();
            return result;
        }

        public async Task<T> Update<T>(object id, object update)
        {
            var url = $"{_apiUrl}/{_route}/{id}";
            var result = await url.WithBasicAuth(Username, Password).PutJsonAsync(update).ReceiveJson<T>();
            return result;
        }
    }
}


