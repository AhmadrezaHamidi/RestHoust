using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient.DataClassification;
using VestaAbner.Dbcontext;
using VestaAbner.Dto;
using VestaAbner.Model;

namespace VestaAbner.Controllers
{
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class ShelveController : ControllerBase
    {
        private readonly DbContextt contaxt1;
        public ShelveController(DbContextt contaxt)
        {
            contaxt1 = contaxt;
        }
        [HttpPost]
        ///for adding shele 
        public string Add([FromBody] Shelvecs shelve)
        {
            Shelvecs shelve1 = new Shelvecs
            {
                Name = shelve.Name,
                Titele= shelve.Titele,
                UserId= shelve.UserId,
                Id=shelve.Id,

            };
            var res =  contaxt1.Shelves.Any(c => c.Id == shelve1.Id);
            if (res)
            {
                return "this shelve is exist later ";
            }
            else
            {
                contaxt1.Shelves.Add(shelve1);
                contaxt1.SaveChanges();
                return "its Okey !!";
            }
           

        }
        [HttpDelete]
        //for deleting 
        public string Delete([FromQuery] int id )
        {
            var exist = contaxt1.Shelves.Find(id);
            if (exist == null)
            {
                return "This is not Okey !!";
            }
            else
            {
                contaxt1.Shelves.Remove(exist);
                contaxt1.SaveChanges();
                return "Okey shod ";
            }
           
        }
         [HttpPut("{id}")]
         //for udateing 
        public string Update([FromBody] Shelvecs shelve)
        {
            var OurBook = contaxt1.Shelves.Where(z => z.Id == shelve.Id).FirstOrDefault();
            ///  var xxx = contaxt1.Shelves.Find(id);
            if (OurBook == null)
            {
                return "This is not exist ";
            }
            else
            {
                OurBook.Name = shelve.Name;
                OurBook.Titele = shelve.Titele;
                OurBook.UserId = shelve.UserId;
                OurBook.Id = shelve.Id;
                contaxt1.SaveChanges();
                return "Update is Okey ";
            }
            

        }
        [HttpGet]
        public shelveDto showOne([FromQuery] int id )
        {
            var Ourshelve = contaxt1.Shelves.Where(z => z.Id == id).FirstOrDefault();
            if (Ourshelve == null)
            {
                return null;
            }
            else
            {
                shelveDto dto = new shelveDto
                {
                    Name = Ourshelve.Name,
                    Titele = Ourshelve.Titele,
                    UserId = Ourshelve.UserId,
                    Id = Ourshelve.Id
                };
                return dto;
            }
        }
        [HttpPost]
        public List<shelveDto> ShowAll()
        {
            var Ourshelve = contaxt1.Shelves.Select(x => new shelveDto
            {
                Id = x.Id,
                Name = x.Name,
                UserId=x.UserId,
                Titele=x.Titele

            }).ToList();
            return Ourshelve;



        }
    }
}
