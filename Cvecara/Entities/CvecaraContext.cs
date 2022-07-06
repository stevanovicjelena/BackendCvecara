using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Entities
{
    public class CvecaraContext : DbContext
    {
        public CvecaraContext(DbContextOptions<CvecaraContext> options) : base(options)
        {

        }

        public DbSet<Cvet> Cvet { get; set; }
        public DbSet<CvetniAranzman> CvetniAranzman { get; set; }
        public DbSet<CvetniAranzman_Cvet> CvetniAranzman_Cvet { get; set; }
        public DbSet<Dodatak> Dodatak { get; set; }
        public DbSet<User> tblUser { get; set; }
        public DbSet<Lokacije> Lokacije { get; set; }
        public DbSet<Pakovanje> Pakovanje { get; set; }
        public DbSet<Porudzbina> Porudzbina { get; set; }
        public DbSet<Porudzbina_Dodatak> Porudzbina_Dodatak { get; set; }
        public DbSet<TipDodatka> TipDodatka { get; set; }
        public DbSet<VrstaCveta> VrstaCveta { get; set; }

    }
}
