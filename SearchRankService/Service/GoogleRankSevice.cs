using SearchRankDomain;
using SearchRankService.Interfaces;
using SearchRankService.Models;
using SearchRankUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SearchRankService.Service
{
    public class GoogleRankSevice : ISearchRankService
    {
        public ISearchClientService _searchClient;
        private const string _searchUrl = "https://www.google.com/search?q={0}&num=100";
        const string regexUrlPattern = @"(?<=<a href=\""\/url\?q=)(.*?)(?=&amp;)";

        public GoogleRankSevice(ISearchClientService searchClient)
        {
            _searchClient = searchClient;
        }

        public async Task<RankResultModel> GetRanksAsync(string searchTerms, string domain)
        {
            var rankResult = new RankResultModel("Google");
            var terms = searchTerms.StringSplit();
            Parallel.ForEach(terms, async term => 
            {
                rankResult.Ranks.Add(await getTermRankAsync(term, domain));
            });
            return rankResult;
        }

        private async Task<RankModel> getTermRankAsync(string searchTerm, string domain)
        {
            var rank = 0;
            try
            {
                 WebClient webclient = new WebClient();
                string data = webclient.DownloadString(new Uri(string.Format(_searchUrl, searchTerm.Replace(" ", "+"))));
                if (string.IsNullOrEmpty(data))
                {
                    throw new Exception("No Results Found");
                }
               rank = processPageData(data, domain);
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

       

        private  int processPageData(string data, string domain)
        {

            Regex regex = new Regex(regexUrlPattern);
            MatchCollection matches = regex.Matches(data);
            var urls = matches.Cast<Match>().Select(m => m.Value.CleanUrl()).ToList();
            if (!urls.Any())
            {
                return 0;
            }

            return urls.IndexOf(domain)+1;
        }

    }
}
