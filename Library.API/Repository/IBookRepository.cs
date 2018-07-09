using Library.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Repository
{
    public interface IBookRepository
    {
        void Add<T>(T entity) where T : class;

        Task<Book> Delete(int id);

        Task<bool> SaveAll();

        Task<IEnumerable<Book>> GetBooks();

        Book EditBook(Book entity);

        Task<Book> GetBook(int id);
    }
}
