using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionePraticheApiDonini.Data;
using Microsoft.EntityFrameworkCore;

namespace GestionePraticheApiDonini.Infrastructure
{
    public class PraticheDB : DbContext
    {
        public string DbPath { get; }

        public PraticheDB()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "praticheDB.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        public DbSet<Pratica> Pratiche { get; set; }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        // }
    }
}