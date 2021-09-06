using SearchRankDomain;
using SearchRankService.Interfaces;
using SearchRankService.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SearchRankService.Service
{
    public class SearchClientService : ISearchClientService
    {
        public HttpClient _httpClient;
        public SearchClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

       
        public async Task<string> GetPageDataAsync(string searchUrl)
        {
            // string data = await _httpClient.GetStringAsync(searchUrl);
            var request = new HttpRequestMessage(HttpMethod.Get, searchUrl);
            request.Headers.Add("User-Agent", "PostmanRuntime/7.28.3"); 
            var resp = await _httpClient.SendAsync(request);
           
            if (resp.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("No Results Found");
            }

            var data = await resp.Content.ReadAsStringAsync();
            return data;
        }
    }
}
