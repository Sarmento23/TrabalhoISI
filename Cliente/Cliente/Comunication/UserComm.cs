using Cliente.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.Comunication
{
    public static class UserComm
    {
        private static string postUsers = "Users/";


        public static async Task<List<User>> GetUsers()
        {
            List<User> aux = null;
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
                        HttpResponseMessage response = await client.GetAsync(postUsers);

                        if (response.IsSuccessStatusCode)
                        {
                            //Added
                            aux = JsonConvert.DeserializeObject<List<User>>(await response.Content.ReadAsStringAsync());
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

        public static async Task<User> GetUser(int id)
        {
            User aux = null;
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
                        HttpResponseMessage response = await client.GetAsync(postUsers + id);

                        if (response.IsSuccessStatusCode)
                        {
                            //Added
                            aux = JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
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

        public static async Task<bool> PutUser(int id, User user)
        {
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
                        var stringContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync(postUsers + id, stringContent);

                        if (response.IsSuccessStatusCode)
                        {
                            return true;
                        }
                        else return false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static async Task<User> PostUser(User user)
        {
            User aux = null;
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
                        var stringContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync(postUsers, stringContent);

                        if (response.IsSuccessStatusCode)
                        {
                            aux = JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
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


        public static async Task<bool> DeleteUser(int id)
        {
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
                        HttpResponseMessage response = await client.DeleteAsync(postUsers + id);

                        if (response.IsSuccessStatusCode)
                        {
                            return true;
                        }
                        else return false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
