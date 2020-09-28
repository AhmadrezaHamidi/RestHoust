using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VestaAbner.Dbcontext;
using VestaAbner.Dto;
using VestaAbner.Model;

namespace VestaAbner.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookAndShelveController : ControllerBase
    {
        private readonly DbContextt contaxt1;
   
    public BookAndShelveController(DbContextt contaxt)
    {
        contaxt1 = contaxt;
    }
    public async Task<IActionResult> Add([FromBody] BookAndShelve item)
    {
            var MyBAndSh = new BookAndShelve
            {
                BookID = item.BookID,
                ShelveId = item.ShelveId
            };
            var res = await contaxt1.bookAndShelves.AnyAsync(c => c.ShelveId==item.ShelveId);
            if (res)
            {
                return NotFound();
            }
            else
            {
                contaxt1.bookAndShelves.Add(MyBAndSh);
                contaxt1.SaveChanges();
                return Ok();
            }
          
    }
    public  string  Delete([FromBody] BookAndShelve item)
        {
            var ExistShelve = contaxt1.Shelves.Where(z => z.Id == item.ShelveId).FirstOrDefault();
            if (ExistShelve==null)
            {
                return "This shelve is not exis ";
            }
            
            else
            {
                var existBook = contaxt1.bookAndShelves.Where(z => z.BookID == item.BookID && z.ShelveId == item.ShelveId);
              
                if (existBook==null)
                {
                    return "This Book is Not Exist In This Shelve ";
                }
                else
                {
                    contaxt1.Remove(existBook);
                    contaxt1.SaveChanges();
                    return "Remov The Book ";
                }
            }
        }
            //var existShelve = contaxt1.bookAndShelves.Where(z => z.ShelveId.Equals(id));
            //if (existShelve == null)
            //{
            //    return "This shlve is not exist ";
            //}
            //else
            //{
            //    var BoOksInshelve = contaxt1.bookAndShelves.Select(x => x.book).ToList();
            //    var shelve = contaxt1.bookAndShelves.Select(x => x.shelve);
                

            //    var reultBook = contaxt1.bookAndShelves.RemoveRange(BoOksInshelve);
            //}
            //var BooksInShelve=contaxt1.bookAndShelves.Where(z=>z.BookID )
            //var shelveBook =  contaxt1.bookAndShelves.Where(z=>z.ShelveId == id);
           
        }

    }

