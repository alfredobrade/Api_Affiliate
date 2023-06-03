using API_Affiliates.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Affiliates.Repository
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext()
        {
            
        }

        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options)
        {
            
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Affiliate> Affiliates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            


            //OnModelCreatingPartial(modelBuilder);
        }

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
