namespace SearchRankService.Interfaces
{
    using SearchRankDomain;
    using System.Threading.Tasks;

    public interface ISearchRankService
    {
        Task<RankResultModel> GetRanksAsync(string searchTerms);
    }
}
