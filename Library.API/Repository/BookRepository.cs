using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Library.API.DAL;
using Library.API.DTO;
using Library.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Repository
{
    public class BookRepository : IBookRepository
    {

        private readonly LibContext _context;
        private readonly IMapper _mapper;

        public BookRepository(LibContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        public async Task<List<BookForEditAdmin>> GetBooks()
        {
            var books = await _context.books.ToListAsync();
            List<BookForEditAdmin> resultBooks = new List<BookForEditAdmin>();
            foreach (var item in books)
            {
                resultBooks.Add(_mapper.Map<BookForEditAdmin>(item));
            }
            return resultBooks;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
