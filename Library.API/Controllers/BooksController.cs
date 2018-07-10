using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library.API.DAL;
using Library.API.Models;
using Microsoft.AspNetCore.Authorization;
using Library.API.Repository;
using Library.API.DTO;
using AutoMapper;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(Roles = "User,Administrator")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LibContext _context;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BooksController(LibContext context, IBookRepository bookRepository, IMapper mapper)
        {
            _context = context;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<List<BookForEditAdmin>> Getbooks()
        {
            var books = await _bookRepository.GetBooks();
            List<BookForEditAdmin> resultBooks = new List<BookForEditAdmin>();
            foreach(var item in books)
            {
                resultBooks.Add(_mapper.Map<BookForEditAdmin>(item));
            }
            return resultBooks;
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var book = await _bookRepository.GetBook(id);
            if (book == null)
            {
                return NotFound();
            }
            var bookResult = _mapper.Map<BookForEditAdmin>(book);

            return Ok(bookResult);
        }

        // POST: api/Books
        [HttpPost]
        public async Task<IActionResult> PostBook([FromBody] BookForEditAdmin book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bookResult = _mapper.Map<BookForEditAdmin, Book>(book);

            _context.books.Add(bookResult);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = bookResult.Id }, bookResult);
        }

        private bool BookExists(int id)
        {
            return _context.books.Any(e => e.Id == id);
        }
    }
}