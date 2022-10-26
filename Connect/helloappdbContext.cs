using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Connect
{
    public partial class helloappdbContext : DbContext
    {
        readonly StreamWriter _writer = new StreamWriter("log.txt", true);
        public helloappdbContext()
        {
           
            bool isExist = Database.EnsureCreated();
            if (isExist) Console.WriteLine("База данных уже создана");
            else Console.WriteLine("База данных уже существует");

            if(Database.CanConnect())
                Console.WriteLine("Бд доступна");
            else
                Console.WriteLine("Бд недоступна");
        }

        public helloappdbContext(DbContextOptions<helloappdbContext> options)
            : base(options)
        {
            
        }

        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
           // {
                var builder = new ConfigurationBuilder();
                object value = builder.SetBasePath(Directory.GetCurrentDirectory());

                builder.AddJsonFile("jsconfig1.json");
                var config = builder.Build();

                string connectionString = config.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
           // }
            optionsBuilder.LogTo(_writer.WriteLine, new[] {DbLoggerCategory.Database.Command.Name} );
        }
        public override void Dispose()
        {
            base.Dispose();
            _writer.Dispose();
        }
        public override async ValueTask DisposeAsync()
        {
            await base.DisposeAsync();
            await _writer.DisposeAsync();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().Ignore(x=>x.Id);//создание сущности 
            modelBuilder.Ignore<Company>();//не создавать
            modelBuilder.Entity<Company>().ToTable("LTD");
            modelBuilder.Entity<Company>().Property("name").HasColumnName("company_name");
            modelBuilder.Entity<Company>().Property("Name").IsRequired();
            modelBuilder.Entity<Company>().HasKey(x => x.Id).HasName("NAMEID");
            modelBuilder.Entity<Company>().HasAlternateKey(x => new { x.Id, x.Name });

        }
    }
}
