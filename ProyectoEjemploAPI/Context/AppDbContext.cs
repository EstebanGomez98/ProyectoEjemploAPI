using Microsoft.EntityFrameworkCore;
using ProyectoEjemploAPI.Models;

namespace ProyectoEjemploAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
        {

        }

        public DbSet<Rol> ROL { get; set; }
        public DbSet<Estado> ESTADO { get; set; }
        public DbSet<Usuario> USUARIO { get; set; }
        public DbSet<Sensor> SENSOR { get; set; }
        public DbSet<Observacion> OBSERVACION { get; set; }
        public DbSet<NEstacionMeteorolica> NESTACIONMETEOROLOGICA { get; set; }
        public DbSet<DatoEstacionMeteorologica> DATOESTACIONMETEOROLOGICA { get; set; }
    }
}
