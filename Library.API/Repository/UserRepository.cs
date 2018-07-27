using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Library.API.DAL;
using Library.API.DTO;
using Library.API.Models;

namespace Library.API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly LibContext _context;
        private readonly IMapper _mapper;

        public UserRepository(LibContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public async Task<User> Delete(int id)
        {
            var user = await _context.User.FindAsync(id);
            _context.User.Remove(user);
            return user;
        }

        public User EditUser(User entity)
        {
            _context.User.Update(entity);
            return entity;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            return user;
        }

        public List<UserForAdmin> GetUsers()
        {
            var users = _context.User;
            List<UserForAdmin> results = new List<UserForAdmin>();
            foreach(var item in users)
            {
                results.Add(_mapper.Map<UserForAdmin>(item));
            }
            return results;
        }

        public Task<bool> SaveAll()
        {
            throw new NotImplementedException();
        }
    }
}
