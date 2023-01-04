using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using JsonWebToken.Internal;
using LibraryWithIdentity.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserEntitiesController : ControllerBase
    {
        private readonly LibraryContext _context;

        public UserEntitiesController(LibraryContext context)
        {
            _context = context;
        }

        // GET: api/UserEntities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserEntity>>> GetUserEntities()
        {
            return await _context.UserEntities.ToListAsync();
        }
        [HttpGet]
        [Route("GetUserData")]
        [Authorize(Policy = Policies.User)]
        public IActionResult GetUserData()
        { 
            var UserId = User.Claims.FirstOrDefault(x => x.Type.Equals("jti"));
            User.IsInRole("Admin");
            return Ok("This is a response from user method");
        }
        [HttpGet]
        [Route("GetAdminData")]
        [Authorize(Policy = Policies.Admin)]
        public IActionResult GetAdminData()
        {
            
            return Ok("This is a response from Admin method");
        }

        // GET: api/UserEntities/5

        [HttpGet("byid/{id}")]
        public async Task<ActionResult<UserEntity>> GetUserEntity(int id)
        {
            var userEntity = await _context.UserEntities.FindAsync(id);

            if (userEntity == null)
            {
                return NotFound();
            }

            return userEntity;
        }
        // GET: api/UserEntities/5
        [HttpGet("byname/{name}")]
        public async Task<ActionResult<UserEntity>> GetUserEntity(string name)
        {
            var user = await _context.UserEntities.Where(u => u.Name == name).FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound();
            }
            return user;


        }

        // PUT: api/UserEntities/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserEntity(string id, UserEntity userEntity)
        {
            if (id != userEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(userEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserEntityExists(id))
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

        // POST: api/UserEntities
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<UserEntity>> PostUserEntity(UserEntity userEntity)
        {
            _context.UserEntities.Add(userEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserEntity", new { id = userEntity.Id }, userEntity);
        }

        // DELETE: api/UserEntities/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserEntity>> DeleteUserEntity(int id)
        {
            var userEntity = await _context.UserEntities.FindAsync(id);
            if (userEntity == null)
            {
                return NotFound();
            }

            _context.UserEntities.Remove(userEntity);
            await _context.SaveChangesAsync();

            return userEntity;
        }

        private bool UserEntityExists(string id)
        {
            return _context.UserEntities.Any(e => e.Id == id);
        }
    }
}
