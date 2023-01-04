using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lib.Models
{
    public class StringEntity
    {
        public StringEntity(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public int Id { get; set; }



    }
}