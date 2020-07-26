using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayerAbstraction.UserAbs
{
    public interface IUserRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<IEnumerable<ModelLayer.User>> GetUsers();
        Task<ModelLayer.User> GetUser(int id);
    }
}
