using BlogAPI.Configurations;
using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace BlogAPI
{
    public class BlogDbContext(DbContextOptions<BlogDbContext> options)
        : DbContext(options)
    {
        public DbSet<PostEntity> Posts { get; set; }
        public DbSet<TagEntity> Tags { get; set; }
        public DbSet<PostTagEntity> PostTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PostConfigurations());
            modelBuilder.ApplyConfiguration(new TagConfigurations());
            modelBuilder.ApplyConfiguration(new PostTagConfigurations());

            base.OnModelCreating(modelBuilder);
        }
    }
}
