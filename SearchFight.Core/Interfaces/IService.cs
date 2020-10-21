using SearchFight.Core.Entities;
using System.Threading.Tasks;

namespace SearchFight.Core.Interfaces
{
    public interface IService
    {
        Task<SearchEngineQuery> Search(string searchCriteria);
    }
}
