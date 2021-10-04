using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Sqlite
{
    public class SQLiteContext : DbContext
    {
        public virtual DbSet<short_url> short_url { get; set; }
        public virtual DbSet<job_schedule> job_schedule { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=../Model/Sqlite/SQLiteDB.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<job_schedule>()
                .Property(f => f.id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<job_schedule>().HasData(
                new job_schedule { id = 1, name = "測試Schedule", cron_expression = "0/30 * * * * ?" });
        }
    }
}
