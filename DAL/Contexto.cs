using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using RPrestamosDetalle.Entidades;

namespace RPrestamosDetalle.DAL
{
    class Contexto : DbContext
    {
        public DbSet<Moras> Moras { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source= Data\Mora.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Moras>().HasData(new Moras { MoraId = 1, Fecha = DateTime.Now, Total = 0 });
        }
    }
}
