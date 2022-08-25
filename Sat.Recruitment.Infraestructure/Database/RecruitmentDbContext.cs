using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Domain.Enums;
using Sat.Recruitment.Domain.Features.Users.Entities;
using System;
using System.Linq;

namespace Sat.Recruitment.Infraestructure.Database
{
    public class RecruitmentDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public string DbPath { get; }

        public RecruitmentDbContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=recruitment_db;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity => { entity.Property(e => e.Name).IsRequired(); });
            modelBuilder.Entity<User>(entity => { entity.Property(e => e.Address).IsRequired(); });
            modelBuilder.Entity<User>(entity => { entity.Property(e => e.Email).IsRequired(); });
            modelBuilder.Entity<User>(entity => { entity.Property(e => e.Phone).IsRequired(); });

            modelBuilder.Entity<User>()
             .Property(e => e.UserTypeId)
             .HasConversion<int>();

            modelBuilder.Entity<UserType>()
                        .Property(e => e.Id)
                        .HasConversion<int>();

            modelBuilder.Entity<UserType>()
                        .HasData(Enum.GetValues(typeof(UserTypes))
                        .Cast<UserTypes>()
                        .Select(e => new UserType()
                        {
                            Id = e,
                            Name = e.ToString()
                        })
            );
       
            base.OnModelCreating(modelBuilder);
        }
    }
}
