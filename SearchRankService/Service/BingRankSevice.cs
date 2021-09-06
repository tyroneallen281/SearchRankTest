using SearchRankDomain;
using SearchRankService.Interfaces;
using SearchRankService.Models;
using SearchRankUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SearchRankService.Service
{
    public class BingRankSevice : ISearchRankService
    {

        public ISearchClientService _searchClient;
        private const string _searchUrl = "https://www.bing.com/search?q={0}&count=100&qs=n";
        const string regexUrlPattern = "(?<=<li class=\"b_algo\">)(.*?)(<\\/li>)";
        const string regexUrlPattern2 = "(?<=href=\")(.*?)(\")";
        public Regex regex,regex2;
        public BingRankSevice(ISearchClientService searchClient)
        {
            _searchClient = searchClient;
            regex = new Regex(regexUrlPattern,
          RegexOptions.Compiled | RegexOptions.IgnoreCase);
            regex2 = new Regex(regexUrlPattern2,
          RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        public async Task<RankResultModel> GetRanksAsync(string searchTerms, string domain)
        {
            var rankResult = new RankResultModel("Bing");
            var terms = searchTerms.StringSplit();
            foreach (var term in terms)
            {
               rankResult.Ranks.Add(await getTermRankAsync(term, domain));
            };
            return rankResult;
        }

        private async Task<RankModel> getTermRankAsync(string searchTerm, string domain)
        {
            var rank = 0;
            try
            {
                var clientData = await _searchClient.GetPageDataAsync(String.Format(_searchUrl, searchTerm.Replace(" ", "+")));
                rank = processPageData(clientData, domain);
            }
            catch (Exception ex)
            {
                rank = -1;
            }

            return new RankModel()
            {
                Term = searchTerm,
                Rank = rank
            };
        }



        private int processPageData(string data, string domain)
        {
            var urls = new List<string>();
            MatchCollection matches = regex.Matches(data);
            var headers = matches.Cast<Match>().Select(m => m.Value).ToList();
            if (!headers.Any())
            {
                return 0;
            }
            foreach (var header in headers)
            {
                var hrefs = regex2.Matches(header);
                if (hrefs.Any())
                {
                    urls.Add(hrefs.First().Value.CleanUrl());
                }
            
            }

            return urls.IndexOf(domain)+1;
        }

    }
}
