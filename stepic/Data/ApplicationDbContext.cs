﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using stepic.Data.Configurations;
using stepic.Models;
using System.Reflection;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<SocialProvider> SocialProviders { get; set; }
    public DbSet<UserSocialProvider> UserSocialProviders { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<UserCourse> UserCourses { get; set; }
    public DbSet<CourseAuthor> CourseAuthors { get; set; }
    public DbSet<Certificate> Certificates { get; set; }
    public DbSet<CertificateSetting> CertificateSettings { get; set; }
    public DbSet<Unit> Units { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<UnitLesson> UnitLessons { get; set; }
    public DbSet<Step> Steps { get; set; }
    public DbSet<Progress> Progresses { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<CourseReview> CourseReviews { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = config.GetConnectionString("DefaultConnection");
        optionsBuilder.UseNpgsql(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.FillInitialData();
    }
}