using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryWithIdentity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookEntitiesController : ControllerBase
    {
        private readonly LibraryContext _context;

        public BookEntitiesController(LibraryContext context)
        {
            _context = context;
        }

        // GET: api/BookEntities
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<CommentEntity>>> GetCommentEntities()
        {
            return await _context.CommentEntities.ToListAsync();
        }

        // GET: api/BookEntities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CommentEntity>> GetCommentEntity(int id)
        {
            var bookEntity = await _context.CommentEntities.FindAsync(id);

            if (bookEntity == null)
            {
                return NotFound();
            }

            return bookEntity;
        }


        // // POST: api/BookEntities
        // // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<UserEntity>> PostUserEntity(UserEntity userEntity)
        {
            _context.UserEntities.Add(userEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserEntity", new { id = userEntity.Id }, userEntity);
        }
        // // DELETE: api/BookEntities/5
        // [HttpDelete("{id}")]
        // public async Task<ActionResult<BookEntity>> DeleteBookEntity(int id)
        // {
        //     var bookEntity = await _context.BookEntities.FindAsync(id);
        //     if (bookEntity == null)
        //     {
        //         return NotFound();
        //     }

        //     _context.BookEntities.Remove(bookEntity);
        //     await _context.SaveChangesAsync();

        //     return bookEntity;
        // }

        // private bool BookEntityExists(string id)
        // {
        //     return _context.BookEntities.Any(e => e.Id == id);
        // }
    }
}
