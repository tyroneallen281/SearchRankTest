using SearchRankDomain;
using SearchRankService.Interfaces;
using SearchRankService.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SearchRankService.Service
{
    public class SearchClientService : ISearchClientService
    {
        private readonly HttpClient _httpClient;

        public SearchClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<XDocument> GetPageDataAsync(string searchUrl)
        {
            var result = await _httpClient.GetStringAsync(searchUrl);
            if (string.IsNullOrEmpty(result))
            {
                throw new Exception("No Results Found");
            }

            return XDocument.Parse(result);
        }
    }
}
