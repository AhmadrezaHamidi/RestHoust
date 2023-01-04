using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TodoApi.Models;

namespace LibraryWithIdentity.Models
{
    public class LibraryContext : IdentityDbContext<UserEntity>
    {
        public DbSet<UserEntity> UserEntities { get; set; }
        public DbSet<PostEntity> PostEntities { get; set; }
        public DbSet<CommentEntity> CommentEntities { get; set; }

        public DbSet<CommiunitiesEntity> CommiunitiesEntities { get; set; }


        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<PostEntity>(e =>
           {
               e.HasKey(p => p.Id);
               e.ToTable("PostEntities");
           });
            builder.Entity<CommentEntity>(e =>
           {
               e.HasKey(p => p.Id);
               e.ToTable("CommentEntities");
           });
            builder.Entity<UserEntity>(e =>
           {
               e.HasKey(X => X.Id);
               e.ToTable("UserEntities");
           });
             builder.Entity<CommiunitiesEntity>(e =>
           {
               e.HasKey(X => X.Id);
               e.ToTable("CommiunitiesEntity");
           });
        }
    }
}
