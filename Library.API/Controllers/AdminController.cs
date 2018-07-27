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
using AutoMapper;
using Library.API.DTO;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly LibContext _context;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<AdminController> _logger;
        private readonly UserManager<User> _userManager;

        public AdminController(LibContext context, IBookRepository bookRepository, IMapper mapper, IUserRepository userRepository, ILogger<AdminController> logger, UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _bookRepository = bookRepository;
            _userRepository = userRepository;
            _logger = logger;
            _userManager = userManager;
        }

        // GET: api/Users
        [HttpGet]
        [Route("users")]
        public List<UserForAdmin> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        [HttpPost]
        [Route("user/save")]
        public async Task<ActionResult> SaveUser([FromBody] UserForAdmin admin)
        {
            User user = new User();
            user = _mapper.Map<User>(admin);
            string psw = "rK%bHU5";
            user.Status = "New";
            user.EmailConfirmed = false;
           var success = await _userManager.CreateAsync(user, psw);
            if (success.Succeeded)
            {
                return Ok("Successful");
            }
            return StatusCode(500);
            
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        [Route("user/get/{id}")]
        public async Task<IActionResult> GetUser([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser([FromRoute] Guid id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        [Route("user/delete/{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = await _context.User.FindAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                _context.User.Remove(user);
                await _context.SaveChangesAsync();
                return Ok(user);

            }
            catch(Exception ex)
            {
                _logger.LogWarning("Wierd error");
                _logger.LogWarning(ex.Message + "\n" + ex.StackTrace);
            }
            return Ok("how ?");
                
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        [Route("book/delete")]
        public async Task<IActionResult> DeleteBook([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var book = await _bookRepository.Delete(id); ;
            if (book == null)
            {
                return NotFound();
            }

            _context.books.Remove(book);
            await _context.SaveChangesAsync();

            return Ok(book);
        }

        [HttpGet]
        [Route("book/list")]
        public async Task<IActionResult> ListBooks()
        {
            try
            {
                var books = await _bookRepository.GetBooks();
                return Ok(books);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("book/get")]
        public async Task<IActionResult> GetBook(int id)
        {
            try
            {
                var book = await _bookRepository.GetBook(id);
                return Ok(book);
            }catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        [Route("book/put")]
        public async Task<IActionResult> PutBook([FromRoute] int id, [FromBody] BookForEditAdmin book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var originalBook = _mapper.Map<Book>(book);

            if (id != originalBook.Id)
            {
                return BadRequest();
            }
            _bookRepository.EditBook(originalBook);

            try
            {
                await _bookRepository.SaveAll();
            }
            catch (DbUpdateConcurrencyException)
            {

            }
            return Ok(originalBook);
        }

        private bool UserExists(Guid id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}