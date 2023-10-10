using BankSystem_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BankSystem_API
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)//use for appps.json
           : base(options)
        { }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //its now used in appstings.json in case you want to chang server

            //options.UseSqlServer("Data Source=(local);Initial Catalog=BS_Using_EF_Core; Integrated Security=true; TrustServerCertificate=True");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
