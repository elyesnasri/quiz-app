using System;
using Microsoft.EntityFrameworkCore;
using QuizApp.Web.Data.Models;

namespace QuizApp.Web.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext (DbContextOptions options)
        : base (options)
    {
    }

    public DbSet<Question>? Questions { get; set; }

    public DbSet<Response>? Responses { get; set; }

    protected override void OnModelCreating (ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Question> ()
            .HasMany (x => x.Responses);

        modelBuilder.Entity<Response> ()
            .HasOne (x => x.Question);
    }
}

