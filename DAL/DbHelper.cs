using Microsoft.EntityFrameworkCore;
using university.DAL.Models;
using university.Models;

namespace university.DAL;

public class DbHelper : DbContext
{
    public DbHelper(DbContextOptions<DbHelper> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<ClassesVenue> ClassesVenue { get; set; } = null!;
    public DbSet<Courses> Courses { get; set; } = null!;
    public DbSet<DifficultyLevel> DifficultyLevels { get; set; } = null!;
    public DbSet<Grades> Grades { get; set; } = null!;
    public DbSet<RegistrationCourse> RegistrationCourse { get; set; } = null!;
    public DbSet<Reviews> Reviews { get; set; } = null!;
    public DbSet<Students> Students { get; set; } = null!;
    public DbSet<Teachers> Teachers { get; set; } = null!;
    public DbSet<TeachersCourses> TeachersCourses { get; set; } = null!;
    public DbSet<Venue> Venue { get; set; } = null!;
    public DbSet<Classess> Classess { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
}