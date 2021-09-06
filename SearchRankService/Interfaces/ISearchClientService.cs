using SearchRankDomain;
using SearchRankService.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SearchRankService.Interfaces
{
    public interface ISearchClientService
    {
        Task<string> GetPageDataAsync(string searchUrl);
    }
}
