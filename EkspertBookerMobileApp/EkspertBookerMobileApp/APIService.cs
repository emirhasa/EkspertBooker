using EkspertBooker.Model;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EkspertBookerMobileApp
{   
    //TODO raise more specific exceptions on not found or bad request etc. and throw them, then catch those specific exceptions in upper layers of application
    public class APIService
    {
        private readonly string _route;

        public static string Username { get; set; }
        public static string Password { get; set; }


#if DEBUG
        public static string _apiUrl = _apiUrl = Properties.Resources.API_URL_localhost;
#endif
#if RELEASE
        public static string _apiUrl = _apiUrl = Properties.Resources.API_URL_TestRelease;
#endif

        public APIService(string route)
        {
            _route = route;
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                if (DeviceInfo.DeviceType == DeviceType.Physical)
                {
                    //android device
                    //setovati u properties - resources
#if DEBUG
                    _apiUrl = Properties.Resources.API_URL_AndroidDevice;
#endif
#if RELEASE
                    _apiUrl = Properties.Resources.API_URL_TestRelease;
#endif
                }
                else
                {
                    //android emulator
#if DEBUG
                    _apiUrl = Properties.Resources.API_URL_AndroidEmulator;
#endif
#if RELEASE
                    _apiUrl = Properties.Resources.API_URL_TestRelease;
#endif
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
            try
            {
                var url = $"{_apiUrl}/{_route}";
                var result = await url.WithBasicAuth(Username, Password).PostJsonAsync(insert).ReceiveJson<T>();
                return result;
            }
            catch
            {
                //todo: throw special exception to know it's failed in api communication
                throw;
            }
        }

        public async Task<T> GetById<T>(object id)
        {
            try
            {
                var url = $"{_apiUrl}/{_route}/{id}";
                var result = await url.WithBasicAuth(Username, Password).GetJsonAsync<T>();
                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<T> Update<T>(object id, object update)
        {
            try
            {
                var url = $"{_apiUrl}/{_route}/{id}";
                var result = await url.WithBasicAuth(Username, Password).PutJsonAsync(update).ReceiveJson<T>();
                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<T> Delete<T>(object id)
        {
            try
            {
                var url = $"{_apiUrl}/{_route}/{id}";
                var result = await url.WithBasicAuth(Username, Password).SendJsonAsync(HttpMethod.Delete, id).ReceiveJson<T>();
                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> DownloadFile(object id)
        {
            try
            {
                var url = $"{_apiUrl}/{_route}/{id}";
                var result = await url.WithBasicAuth(Username, Password).DownloadFileAsync("C:\\FIT", "test");
                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}


