using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VestaAbner.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace VestaAbner.Dbcontext
{
    public class DbContextt : IdentityDbContext<User>
    {
        public DbContextt()
        {

        }
        public DbContextt(DbContextOptions<DbContext> options):base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server = .; Database=VestaAbnerDataBAse;Integrated Security = true;");
            }
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Book>(e =>
           {
               e.HasMany(p => p.BookAndShelves).WithOne(p => p.book).HasForeignKey(X => X.BookID);
               e.HasKey(p => p.Id);
               e.ToTable("Book");
           });
            builder.Entity<BookAndShelve>(e =>
           {
             
               e.HasKey(p => p.ShelveId);
               e.ToTable("BookAndShelve");
           });
            builder.Entity<TbUserRole>(e =>
           {
               e.HasOne(x => x.User).WithMany(x => x.userRoles).HasForeignKey(x => x.UserId);
               e.HasOne(x => x.Role).WithMany(X => X.userRoles).HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.NoAction);

           });
            builder.Entity<User>(e =>
           {
             ///  e.Property(x => x.Birthday).HasDefaultValue("getdate()");
              
               e.HasKey(X => X.Id);
               e.ToTable("User");
           });
            builder.Entity<Shelvecs>(e =>
           {
               e.HasOne(x => x.user).WithMany(x => x.shelveces).HasForeignKey(p => p.UserId);
               e.HasMany(x => x.BookAndShelve).WithOne(x => x.shelve).HasForeignKey(x => x.ShelveId);
               e.HasKey(X => X.Id);
               e.ToTable("shelves");
           });

        }














        //public DbContextt( DbContextOptions options) : base(options)
        //{

        //}
       // public DbSet<User> users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Shelvecs> Shelves  { get; set; }
        public DbSet<BookAndShelve> bookAndShelves { get; set; }
    }
}
