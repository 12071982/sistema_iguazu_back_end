using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public class _dbContext:DbContext
    {
        #region configuration
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationBuilder configurationBuild = new ConfigurationBuilder();
            //    configurationBuild = configurationBuild.SetBasePath(Directory.GetCurrentDirectory());
            configurationBuild = configurationBuild.AddJsonFile("appsettings.json");
            IConfiguration configurationFile = configurationBuild.Build();

            // Leemos el archivo de configuración.
            string conneccion = configurationFile.GetConnectionString("db");
            optionsBuilder.UseSqlServer(connectionString: conneccion);
        }
        #endregion


        public DbSet<DestinoModel> Destino { get; set; }
        public DbSet<PaqueteModel> Paquete { get; set; }
        public DbSet<TemporadaModel> Temporada { get; set; }
        public DbSet<PaqueteTemporadaModel> Paquete_Temporada { get; set; }
        public DbSet<ClienteModel> Cliente { get; set; }
        public DbSet<UsuarioModel> Usuario { get; set; }
        public DbSet<PagoModel> Pago { get; set; }
        public DbSet<ReservaModel> Reserva { get; set; }
        public DbSet<ErrorModel> Error { get; set; }
        public DbSet<DetallePagoModel>Detalle_Pago { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de la relación Cliente -> Reserva (uno a muchos)
            modelBuilder.Entity<ReservaModel>()
                .HasOne(r => r.Cliente)
                .WithMany(c => c.Reservas)
                .HasForeignKey(r => r.ID_Cliente)
                .OnDelete(DeleteBehavior.Cascade); // Eliminar en cascada

            // Configuración de la relación Reserva -> Pago (uno a uno)
            modelBuilder.Entity<PagoModel>()
                .HasOne(p => p.Reserva)
                .WithOne(r => r.Pago)
                .HasForeignKey<PagoModel>(p => p.ID_Reserva)
                .OnDelete(DeleteBehavior.Cascade); // Eliminar en cascada

            // Configuración de la relación Pago -> DetallePago (uno a muchos)
            modelBuilder.Entity<DetallePagoModel>()
                .HasOne(d => d.Pago)
                .WithMany(p => p.DetallePago)
                .HasForeignKey(d => d.ID_Pago)
                .OnDelete(DeleteBehavior.Cascade); // Eliminar en cascada

            base.OnModelCreating(modelBuilder);
        }
    }
}
