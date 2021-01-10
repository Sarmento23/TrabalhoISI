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
    public static class SectorComm
    {
        const string postSectors = "Sectors/";
        public static async Task<List<Sector>> GetSectors()
        {
            List<Sector> aux = null;
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
                        HttpResponseMessage response = await client.GetAsync(postSectors);

                        if (response.IsSuccessStatusCode)
                        {
                            aux = JsonConvert.DeserializeObject<List<Sector>>(await response.Content.ReadAsStringAsync());
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
        public static async Task<Sector> GetSector(int id)
        {
            Sector aux = null;
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
                        HttpResponseMessage response = await client.GetAsync(postSectors + id);

                        if (response.IsSuccessStatusCode)
                        {
                            aux = JsonConvert.DeserializeObject<Sector>(await response.Content.ReadAsStringAsync());
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

        public static async Task<bool> PutSector(int id, Sector sector)
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
                        var stringContent = new StringContent(JsonConvert.SerializeObject(sector), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync(postSectors + id, stringContent);

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

        public static async Task<Sector> PostSector(Sector sector)
        {
            Sector aux = null;
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
                        var stringContent = new StringContent(JsonConvert.SerializeObject(sector), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync(postSectors, stringContent);

                        if (response.IsSuccessStatusCode)
                        {
                            aux = JsonConvert.DeserializeObject<Sector>(await response.Content.ReadAsStringAsync());
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

        public static async Task<bool> DeleteSector(int id)
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
                        HttpResponseMessage response = await client.DeleteAsync(postSectors + id);

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
