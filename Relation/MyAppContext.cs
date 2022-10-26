using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relation
{
    internal class MyAppContext:DbContext
    {

        public DbSet<Worker> Workers { get; set; } = null!;
        public DbSet<Company> Companies { get; set; } = null!;
        public IQueryable<Worker> GetUsersByAge(int age) => FromExpression(() => GetUsersByAge(age));
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server = (localdb)\mssqllocaldb; Database = relation; Trusted_Connection = True;");
            optionsBuilder.UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Worker>().HasOne(u=>u.Company).WithMany(x=>x.Workers).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.HasDbFunction(() => GetUsersByAge(default));
        }
        public override void Dispose()
        {
            base.Dispose();
        }
        public override ValueTask DisposeAsync()
        {
            return base.DisposeAsync();
        }
    }
}
