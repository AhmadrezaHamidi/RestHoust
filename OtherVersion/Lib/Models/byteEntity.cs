using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Lib.Models
{
    public class byteEntity 
    {

        public byteEntity(string cipherText, string key, string iv)
        {
            CipherText = cipherText;
            Key = key;
            Iv = iv;
        }

        [Key]
        public int Id { get; set; }
        public string CipherText { get; set; }
        public string Key { get; set; }
        public string Iv { get; set; }
        
    }
}