using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Crud_Ligaut.Shared
{

    public class SQLDBContext : DbContext
    {
        private readonly string _conexionString = "Server=LAPTOP-Q1PBGT9D\\MSSQLSERVER2;Database=liga12;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False";



        public SQLDBContext()
        {


        }


        public SQLDBContext(DbContextOptions<SQLDBContext> options) : base(options)
        {

        }


        public virtual DbSet<Liga> Liga { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_conexionString);
            }
            base.OnConfiguring(optionsBuilder);
        }
    }
}
