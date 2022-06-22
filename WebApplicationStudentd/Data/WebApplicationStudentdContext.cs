using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplicationStudentd.Models;

namespace WebApplicationStudentd.Data
{
    public class WebApplicationStudentdContext : DbContext
    {
        public WebApplicationStudentdContext (DbContextOptions<WebApplicationStudentdContext> options)
            : base(options)
        {
        }

        public DbSet<WebApplicationStudentd.Models.Students>? Students { get; set; }

        public DbSet<WebApplicationStudentd.Models.Courses>? Courses { get; set; }

        public DbSet<WebApplicationStudentd.Models.StudentCourses>? StudentCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Students>()
                .HasIndex(s => s.StudentId)
                .IsUnique();

            modelBuilder.Entity<Courses>()
                .HasIndex(c => c.Name)
                .IsUnique();
        }
    }
}
