using ModelLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayerAbstraction.CDBAbs
{
    public interface ICosmosDbService
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<IEnumerable<Test>> GetUsers();
        Task<Test> GetUser(int id);
    }
}
