using SearchRankService.Models;
using System;
using System.Collections.Generic;

namespace SearchRankDomain
{
    public class RankResultModel
    {
        public RankResultModel(string provider)
        {
            SearchProviderName = provider;
            Ranks = new List<RankModel>();
        }
        public List<RankModel> Ranks { get; set; }
        public string SearchProviderName { get; set; }
    }
}
