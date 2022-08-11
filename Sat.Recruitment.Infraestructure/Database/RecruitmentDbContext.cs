using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Domain.Features.Users.Entities;

namespace Sat.Recruitment.Infraestructure.Database
{
    public class RecruitmentDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }     
        public string DbPath { get; }

        public RecruitmentDbContext() {}
      
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=recruitment_db;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
    }
}
