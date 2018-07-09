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
        public async Task<IEnumerable<Book>> Getbooks()
        {
            return await _bookRepository.GetBooks();
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

            return Ok(book);
        }

        // POST: api/Books
        [HttpPost]
        public async Task<IActionResult> PostBook([FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = book.Id }, book);
        }

        private bool BookExists(int id)
        {
            return _context.books.Any(e => e.Id == id);
        }
    }
}