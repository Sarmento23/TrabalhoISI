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
    public static class TicketsComm
    {
        const string postTicket = "Tickets/";
        public static async Task<List<Ticket>> GetTickets()
        {
            List<Ticket> aux = null;
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
                        HttpResponseMessage response = await client.GetAsync(postTicket);

                        if (response.IsSuccessStatusCode)
                        {
                            //Added
                            aux = JsonConvert.DeserializeObject<List<Ticket>>(await response.Content.ReadAsStringAsync());
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

        public static async Task<Ticket> GetTicket(int id)
        {
            Ticket aux = null;
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
                        HttpResponseMessage response = await client.GetAsync(postTicket + id);

                        if (response.IsSuccessStatusCode)
                        {
                            //Added
                            aux = JsonConvert.DeserializeObject<Ticket>(await response.Content.ReadAsStringAsync());
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

        public static async Task<bool> PutTicket(int id, Ticket ticket)
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
                        var stringContent = new StringContent(JsonConvert.SerializeObject(ticket), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync(postTicket + id, stringContent);

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

        public static async Task<Ticket> PostTicket(Ticket ticket)
        {
            Ticket aux = null;
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
                        var stringContent = new StringContent(JsonConvert.SerializeObject(ticket), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync(postTicket, stringContent);

                        if (response.IsSuccessStatusCode)
                        {
                            aux = JsonConvert.DeserializeObject<Ticket>(await response.Content.ReadAsStringAsync());
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

        public static async Task<bool> DeleteTicket(int id)
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
                        HttpResponseMessage response = await client.DeleteAsync(postTicket + id);

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

