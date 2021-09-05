using SearchRankDomain;
using SearchRankService.Interfaces;
using SearchRankService.Models;
using SearchRankUtilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SearchRankService.Service
{
    public class GoogleRankSevice : ISearchRankService
    {
        public ISearchClientService _searchClient;
        private const string _searchUrl = "https://www.google.co.uk/search?q={0}&start={1}";

        public GoogleRankSevice(ISearchClientService searchClient)
        {
            _searchClient = searchClient;
        }

        public async Task<RankResultModel> GetRanksAsync(string searchTerm)
        {
            var rankResult = new RankResultModel("Google");
            var terms = searchTerm.StringSplit();
            Parallel.ForEach(terms, async term => 
            {
                rankResult.Ranks.Add(await getTermRankAsync(term));
            });
            return rankResult;
        }

        private async Task<RankModel> getTermRankAsync(string searchTerm)
        {
            var rank = 0;
            var skip = 0;
            while (rank == 0 && skip < 100)
            {
                var clientData = await _searchClient.GetPageDataAsync(String.Format(_searchUrl, searchTerm, skip.ToString()));
                rank = processPageData(clientData);
                skip += 10;
            }

            return new RankModel()
            {
                Term = searchTerm,
                Rank = rank
            };
        }

        private  int processPageData(XDocument data)
        {

            return 0;
        }

    }
}
