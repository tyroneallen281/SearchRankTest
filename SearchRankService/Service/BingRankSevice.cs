using SearchRankDomain;
using SearchRankService.Interfaces;
using SearchRankService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchRankService.Service
{
    public class BingRankSevice : ISearchRankService
    {

        public Task<RankResultModel> GetRanksAsync(string searchTerms)
        {
            throw new NotImplementedException();
        }
    }
}
