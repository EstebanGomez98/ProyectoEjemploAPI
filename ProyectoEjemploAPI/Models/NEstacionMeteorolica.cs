using System.ComponentModel.DataAnnotations;

namespace ProyectoEjemploAPI.Models
{
    public class NEstacionMeteorolica
    {
        [Key]
        public int Id_estacion_meteorologica { get; set; }
        public string Nombre { get; set; }
    }
}
