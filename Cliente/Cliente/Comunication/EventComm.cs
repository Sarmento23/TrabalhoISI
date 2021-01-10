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
    public static class EventComm
    {
        const string postEvents = "Events";
        public static async Task<List<Event>> GetEvents()
        {
            List<Event> aux = null;
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
                        HttpResponseMessage response = await client.GetAsync(postEvents);

                        if (response.IsSuccessStatusCode)
                        {
                            aux = JsonConvert.DeserializeObject<List<Event>>(await response.Content.ReadAsStringAsync());
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

        public static async Task<Event> GetEvent(int id)
        {
            Event aux = null;
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
                        HttpResponseMessage response = await client.GetAsync(postEvents);

                        if (response.IsSuccessStatusCode)
                        {
                            aux = JsonConvert.DeserializeObject<Event>(await response.Content.ReadAsStringAsync());
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

        public static async Task<bool> PutEvent(int id, Event @event)
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
                        var stringContent = new StringContent(JsonConvert.SerializeObject(@event), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync(postEvents + id, stringContent);

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

        public static async Task<Event> PostEvent(Event @event)
        {
            Event aux = null;
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
                        var stringContent = new StringContent(JsonConvert.SerializeObject(@event), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync(postEvents, stringContent);

                        if (response.IsSuccessStatusCode)
                        {
                            aux = JsonConvert.DeserializeObject<Event>(await response.Content.ReadAsStringAsync());
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

        public static async Task<bool> DeleteEvent(int id)
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
                        HttpResponseMessage response = await client.DeleteAsync(postEvents + id);

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

