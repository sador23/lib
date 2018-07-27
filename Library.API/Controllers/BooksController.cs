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
using Microsoft.Extensions.Logging;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "User")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LibContext _context;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<BooksController> _logger;

        public BooksController(LibContext context, IBookRepository bookRepository, IMapper mapper, ILogger<BooksController> logger)
        {
            _context = context;
            _bookRepository = bookRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<List<BookForEditAdmin>> Getbooks()
        {
            _logger.LogWarning("enter here");
            var books = await _bookRepository.GetBooks();
            return books;
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