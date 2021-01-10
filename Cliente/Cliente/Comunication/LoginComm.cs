using Cliente.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.Comunication
{
    public static class LoginComm
    {
        const string postLogin = "Login";
        public static async Task<UserLogin> Authentication (Login login)
        {
            UserLogin aux = null;
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    using (var client = new HttpClient(httpClientHandler))
                    {
                        client.BaseAddress = new Uri("https://localhost:44367/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        //POST
                        var stringContent = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync(postLogin, stringContent);

                        if (response.IsSuccessStatusCode)
                        {
                            //Added
                            aux = JsonConvert.DeserializeObject<UserLogin>(await response.Content.ReadAsStringAsync());
                        }
                        else return null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return aux;
        }
    }
}
