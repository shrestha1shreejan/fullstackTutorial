using DatingApp.Helpers;
using DatingApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatingApp.Data
{
    public interface IDatingRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        void Add<T>(T entity) where T : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        void Delete<T>(T entity) where T : class;

        /// <summary>
        /// Save the data
        /// </summary>
        /// <returns></returns>
        Task<bool> SaveAll();

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        Task<PagedList<User>> GetUsersAsync(UserParams userParams);

        /// <summary>
        /// Get single user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<User> GetUserAsync(int id);

        /// <summary>
        /// Get the user photo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Photo> GetPhotoAsync(int id);

        /// <summary>
        /// Gets teh main photo for the user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Photo> GetMainPhotoForUser(int id);

        /// <summary>
        /// Gets the Like information on basis of the two user id's
        /// to figure out if the user has already liked another user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="recipientId"></param>
        /// <returns></returns>
        Task<Like> GetLike(int userId, int recipientId);
    }
}
