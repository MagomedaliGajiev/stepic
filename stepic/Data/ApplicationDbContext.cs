using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using stepic.Models;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Certificate> Certificates { get; set; }
    public DbSet<UserCourse> UserCourses { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<CourseReview> CourseReviews { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = config.GetConnectionString("DefaultConnection");
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
}
