using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using MKT_POLOSYS_API;

#nullable disable

namespace MKT_POLOSYS_API.DataAccess
{
    public partial class WISE_STAGINGContext : DbContext
    {
        public WISE_STAGINGContext()
        {
        }

        public WISE_STAGINGContext(DbContextOptions<WISE_STAGINGContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Server=10.0.11.145;Database=WISE_STAGING;user id=IT_SOLWOM;password=Passwd789;");
                IConfigurationRoot configuration = new ConfigurationBuilder()
                 .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                 .AddJsonFile("appsettings.json")
                 .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }
        }
         
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
