using Blog.API.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Blog.API.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("DB_CONNECTION_STRING"));
        }

        public DbSet<PostData> Posts { get; set; }
        public DbSet<ReplyData> Replies { get; set; }
    }
}
