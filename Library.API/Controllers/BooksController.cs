﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library.API.DAL;
using Library.API.Models;
using Microsoft.AspNetCore.Authorization;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "User,Administrator")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LibContext _context;

        public BooksController(LibContext context)
        {
            _context = context;
        }

        // GET: api/Books
        [HttpGet]
        public IEnumerable<Book> Getbooks()
        {
            return _context.books;
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var book = await _context.books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook([FromRoute] int id, [FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != book.Id)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
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

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var book = await _context.books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.books.Remove(book);
            await _context.SaveChangesAsync();

            return Ok(book);
        }

        private bool BookExists(int id)
        {
            return _context.books.Any(e => e.Id == id);
        }
    }
}