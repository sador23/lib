using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.API.DAL;
using Library.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Repository
{
    public class BookRepository : IBookRepository
    {

        private readonly LibContext _context;

        public BookRepository(LibContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public async Task<Book> Delete(int id)
        {
            var book = await GetBook(id);
            _context.books.Remove(book);
            return book;
        }

        public Book EditBook(Book entity)
        {
            var book = _context.books.Update(entity);
            _context.Entry(book).State = EntityState.Modified;
            return book.Entity;
        }

        public  Task<Book> GetBook(int id)
        {
            var book = _context.books.FindAsync(id);
            return book;
        }

        public async Task<IEnumerable<Book>> GetBooks()
        {
            var books = await _context.books.ToListAsync();
            return books;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
