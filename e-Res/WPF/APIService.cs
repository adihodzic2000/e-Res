
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using WPF.Helper;

namespace WPF
{
    public class APIService
    {
        //private string _resource = null;
        public string _endpoint = "https://localhost:7071/api/";
        public string _endpointImage = "https://localhost:7071";

        public static string Username = null;
        public static string Password = null;
        public static string FullName = null;
        public static string LogoPath = null;
        public static Guid MyId = new Guid();
        public static string Token = null;
        public static Guid? CompanyId = null;


        //public APIService(string resource)
        //{
        //    _resource = resource;
        //}

        public async Task<Core.Message> Get(string _resource, object search = null)
        {
            var query = "";
            if (search != null)
            {
                query = await search.ToQueryString();
            }

            var list = await $"{_endpoint}{_resource}?{query}".WithHeader("Authorization", Token).GetJsonAsync<Core.Message>();

            return list;
        }

        public async Task<Core.Message> GetById(string _resource, object id)
        {
            var result = await $"{_endpoint}{_resource}/{id}".WithHeader("Authorization", Token).GetJsonAsync<Core.Message>();

            return result;
        }
        public async Task DeleteById( string _resource, Guid id)
        {
            var result = await $"{_endpoint}{_resource}/{id}".WithHeader("Authorization", Token).DeleteAsync();

        }

        public async Task<T> Post<T>(string _resource, object request)
        {
            try
            {
                var result = await $"{_endpoint}{_resource}".WithHeader("Authorization", Token).PostJsonAsync(request).ReceiveJson<T>();
                return result;
            }
            catch (FlurlHttpException ex)
            {
                var errors = await ex.GetResponseJsonAsync<Dictionary<string, string[]>>();

                var stringBuilder = new StringBuilder();
                foreach (var error in errors)
                {
                    stringBuilder.AppendLine($"{error.Key}, ${string.Join(",", error.Value)}");
                }

                System.Windows.Forms.MessageBox.Show(stringBuilder.ToString(), "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return default(T);
            }

        }
       
        public async Task<Core.Message> Put(string _resource, object id, object request)
        {
            var result = await $"{_endpoint}{_resource}/{id}".WithHeader("Authorization", Token).PutJsonAsync(request).ReceiveJson<Core.Message>();

            return result;
        }
    }
}
