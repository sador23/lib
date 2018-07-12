using Library.API.DTO;
using Library.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Repository
{
    public interface IUserRepository
    {
        void Add<T>(T entity) where T : class;

        Task<User> Delete(int id);

        Task<bool> SaveAll();

        List<UserForAdmin> GetUsers();

        User EditUser(User entity);

        Task<User> GetUser(int id);
    }
}
