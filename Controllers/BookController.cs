using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using VestaAbner.Dbcontext;
using VestaAbner.Dto;
using VestaAbner.Model;

namespace VestaAbner.Controllers
{
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly DbContextt _db;
        public BookController(DbContextt db)
        {
            _db = db;
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Book item )
        {
            Book book = new Book
            {
                publisher = item.publisher,
                Name = item.Name,
                seriyal = item.seriyal,
                Title = item.Title,
               

            };
            var res = await _db.Books.AnyAsync(c => c.seriyal == item.seriyal);
            if (res)
            {
                return NotFound();
            }
            else
            {
                await _db.Books.AddAsync(book);
                await _db.SaveChangesAsync();
                return Ok();
            }
        }
        [HttpPost]
        public async Task<string> Update([FromBody] Book itm)
        {
            /// var OurBook =a_db.Books.Where(z => z.Id == itm.Id).FirstOrDefault();
            var OurBook =  _db.Books.Where(z => z.Id == itm.Id).FirstOrDefault();
            if (OurBook == null)
            {
                return "this book is not exist";
            }
            else
            {
                OurBook.Name = itm.Name;
                OurBook.publisher = itm.publisher;
                OurBook.Title = itm.Title;
                _db.Books.Update(OurBook);
                _db.SaveChanges();
                return "Upload is Okey ";
            }
        }
        [HttpDelete]
        public string Delete([FromQuery] long seriyal )
        {
            var OurBook = _db.Books.Where(z => z.seriyal == seriyal).FirstOrDefault();
            if (OurBook == null)
            {
                return "this book is not exist";
            }
            else
            {
                _db.Books.Remove(OurBook);
                _db.SaveChanges();
                return "the Book is deleted ! ";
            }

        }
        [HttpPost]
        public BookDto ShowOne([FromQuery] int id )
        {
            var OurBook = _db.Books.Where(z => z.Id == id).FirstOrDefault();
            if (OurBook == null)
            {
                return null;
            }
            else
            {


                BookDto dto = new BookDto
                {
                    Id = OurBook.Id,
                    Name = OurBook.Name,
                    Title = OurBook.Title,
                    publisher = OurBook.publisher
                };
                return dto;
            }

        }
        [HttpPost]
        public List<BookDto> ShowAll()
        {
            var OurBook = _db.Books.Select(x => new BookDto
            {
                Id = x.Id,
                Name = x.Name,
                publisher = x.publisher,
                Title = x.Title

            }).ToList();
            return OurBook;
          


        }

    }
    }

