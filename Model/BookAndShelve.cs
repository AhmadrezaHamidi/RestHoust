using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VestaAbner.Model
{
    public class BookAndShelve
    {
        public Book book { get; set; }
        public Shelvecs shelve { get; set; }
        public int BookID { get; set; }
        public int ShelveId { get; set; }
    }
}
