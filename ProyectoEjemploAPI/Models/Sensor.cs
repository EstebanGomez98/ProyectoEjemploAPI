using System.ComponentModel.DataAnnotations;

namespace ProyectoEjemploAPI.Models
{
    public class Sensor
    {
        [Key]
        public int Id_sensor { get; set; }
        public string Descripcion { get; set; }
        public int Id_estado { get; set; }
    }
}
