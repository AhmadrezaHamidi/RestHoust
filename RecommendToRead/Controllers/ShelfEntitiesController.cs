using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using LibraryWithIdentity.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ShelfEntitiesController : ControllerBase
    {
        private readonly LibraryContext _context;

        public ShelfEntitiesController(LibraryContext context)
        {
            _context = context;
        }

        // GET: api/ShelfEntities
    //     [HttpGet]
    //     [Authorize]
    //     public async Task<ActionResult<IEnumerable<ShelfEntity>>> GetShelfEntities()
    //     {
            
    //         var UserIdtt = User.Claims.Where(x => x.Type.Equals("Id"))
    //         .Select(x => x.Value)
    //         .FirstOrDefault();

    //          var shelves = await _context.ShelfEntities
    //         .Where(u => u.UserId.Equals(UserIdtt))
    //         .ToListAsync();
    //         return shelves;
    //     }

    //     // GET: api/ShelfEntities/5
    //     [HttpGet("{id}")]
    //     public List<bookdto> GetShelfEntity(string id)
    //     {
    //         var books = _context.ShelfEntities
    //             .Where(x => x.Id == id)
    //             .Include(x => x.BookShelves)
    //             .ThenInclude(x => x.bookEntity)
    //             .SelectMany(x => x.BookShelves)
    //             .Select (x => new bookdto
    //             {
    //                 bookname = x.bookEntity.Title
    //             })
    //             // .Select(x => x.BookShelves.Select(x=> new bookdto{bookname= x.bookEntity.Title}))
    //             .ToList();

    //       return books;
    //     }

       
    //     // PUT: api/ShelfEntities/5
    //     // To protect from overposting attacks, enable the specific properties you want to bind to, for
    //     // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    //     [HttpPut("{id}")]
    //     public async Task<IActionResult> PutShelfEntity(string id, ShelfEntity shelfEntity)
    //     {
    //         if (id != shelfEntity.Id)
    //         {
    //             return BadRequest();
    //         }

    //         _context.Entry(shelfEntity).State = EntityState.Modified;

    //         try
    //         {
    //             await _context.SaveChangesAsync();
    //         }
    //         catch (DbUpdateConcurrencyException)
    //         {
    //             if (!ShelfEntityExists(id))
    //             {
    //                 return NotFound();
    //             }
    //             else
    //             {
    //                 throw;
    //             }
    //         }

    //         return NoContent();
    //     }

    //     // POST: api/ShelfEntities
    //     // To protect from overposting attacks, enable the specific properties you want to bind to, for
    //     // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        
    //     [HttpPost]
    //     [Authorize]
    //     public async Task<ActionResult<ShelfEntity>> PostShelfEntity(ShelfEntity shelfEntity)
    //     {
    //         var UserIdt = User.Claims.Where(x => x.Type.Equals("Id"))
    //         .Select(x => x.Value)
    //         .FirstOrDefault();
    //         shelfEntity.UserId=UserIdt;
    //         _context.ShelfEntities.Add(shelfEntity);
    //         await _context.SaveChangesAsync();

    //         return CreatedAtAction("GetShelfEntity", new { id = shelfEntity.Id }, shelfEntity);
    //     }

    //     // DELETE: api/ShelfEntities/5
    //     [HttpDelete("{id}")]
    //     public async Task<ActionResult<ShelfEntity>> DeleteShelfEntity(int id)
    //     {
    //         var shelfEntity = await _context.ShelfEntities.FindAsync(id);
    //         if (shelfEntity == null)
    //         {
    //             return NotFound();
    //         }

    //         _context.ShelfEntities.Remove(shelfEntity);
    //         await _context.SaveChangesAsync();

    //         return shelfEntity;
    //     }

    //     private bool ShelfEntityExists(string id)
    //     {
    //         return _context.ShelfEntities.Any(e => e.Id == id);
    //     }
     }
}
