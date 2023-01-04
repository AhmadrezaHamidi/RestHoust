using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lib.Models
{
    public class LibContext : DbContext
    {
        
        public LibContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<byteEntity> ByteEntity { get; set; }
        public DbSet<StringEntity> StringEntity { get; set; }








        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    
    }
}
