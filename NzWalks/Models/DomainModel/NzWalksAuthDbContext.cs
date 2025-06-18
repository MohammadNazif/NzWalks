using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NzWalks.Models.DomainModel
{
    public class NzWalksAuthDbContext : IdentityDbContext
    {
        public NzWalksAuthDbContext(DbContextOptions<NzWalksAuthDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var readerRoleId = "e1da7ff7-6a75-4547-8302-f4fd1a4944d0";
            var writerRoleId = "bb3a9f4c-872e-43a6-b5de-2e2fd190500d";
            var roles = new List<IdentityRole>
            {
                 new IdentityRole
                 {
                     Id = readerRoleId,
                     ConcurrencyStamp = readerRoleId,
                     Name = "Reader",
                     NormalizedName = "Reader".ToUpper()
                 },
                 new IdentityRole
                 {
                     Id = writerRoleId,
                      ConcurrencyStamp= writerRoleId,
                      Name = "Writer",
                      NormalizedName = "Writer".ToUpper()
                 }
            };
            builder.Entity<IdentityRole>().HasData(roles);

        }

    }
}
