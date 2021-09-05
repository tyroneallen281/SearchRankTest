namespace SearchRankTest.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using SearchRankDomain;
    using SearchRankService.Interfaces;
    using SearchRankService.Models;

    [Route("api/[controller]")]
    public class SearchRankController : Controller
    {
        private IEnumerable<ISearchRankService> _searchProviders { get; set; }

        public SearchRankController(IEnumerable<ISearchRankService> searchProviders)
        {
            _searchProviders = searchProviders;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<RankResultModel>>> GetRanks(string searchTerms)
        {
            if (string.IsNullOrEmpty(searchTerms))
            {
                BadRequest();
            }

            var result = new List<RankResultModel>();
            foreach (var searchProvider in _searchProviders)
            {
                result.Add(await searchProvider.GetRanksAsync(searchTerms));
            }

            return Ok(result);
        }

    }
}