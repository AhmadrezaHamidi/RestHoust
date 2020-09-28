using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VestaAbner.Model
{
    public class Book
    {
        public string  publisher  { get; set; }
        public string  Name { get; set; }
        public long seriyal { get; set; }
        public string  Title { get; set; }
        public int Id { get; set; }
        public List<BookAndShelve> BookAndShelves { get; set; }
    }
}
