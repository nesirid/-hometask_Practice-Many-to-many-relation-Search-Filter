using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Reflection;


namespace Repository.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Teacher> Teachers { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Teacher>()
            .Property(t => t.Salary)
            .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<GroupsStudents>()
                .HasKey(t => new { t.GroupId, t.StudentId });
            modelBuilder.Entity<GroupsStudents>()
                .HasOne(t => t.Group)
                .WithMany(g => g.GroupsStudents)
                .HasForeignKey(t => t.GroupId);
            modelBuilder.Entity<GroupsStudents>()
                .HasOne(t => t.Student)
                .WithMany(s => s.GroupsStudents)
                .HasForeignKey(t => t.StudentId);
            //Group<>Room
            modelBuilder.Entity<Group>()
                .HasOne(g => g.Room)
                .WithMany(r => r.Groups)
                .HasForeignKey(g => g.RoomId)
                .OnDelete(DeleteBehavior.Cascade);
            //Group Education
            modelBuilder.Entity<Group>()
                .HasOne(g => g.Education)
                .WithMany(e => e.Groups)
                .OnDelete(DeleteBehavior.Restrict);
            // Group<>Teacher
            modelBuilder.Entity<Group>()
                .HasOne(g => g.Teacher)
                .WithMany()
                .HasForeignKey(g => g.TeacherId)
                .OnDelete(DeleteBehavior.Cascade);
            //Student
            modelBuilder.Entity<Student>()
                .HasMany(s => s.Educations)
                .WithMany(e => e.Students)
                .UsingEntity(j => j.ToTable("StudentEducations"));
        }
    }
}