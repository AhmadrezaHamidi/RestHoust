using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VestaAbner.Model
{
    public class Shelvecs
    {
        public string  Name { get; set; }
        public string  Titele { get; set; }
        public string  UserId { get; set; }
        public List<BookAndShelve> BookAndShelve { get; set; }
        public int Id { get; set; }
        public User user { get; set; }
    }
}
