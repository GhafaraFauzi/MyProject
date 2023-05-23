using Microsoft.EntityFrameworkCore;
using MyProject.Models;

namespace MyProject.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> option) : base(option)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Departemen> Departemens { get; set; }
        public DbSet<Account> Accounts { get; set; }

    }
}
