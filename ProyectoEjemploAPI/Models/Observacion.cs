using System.ComponentModel.DataAnnotations;

namespace ProyectoEjemploAPI.Models
{
    public class Observacion
    {
        [Key]
        public int Id_observacion { get; set; }
        public int Id_usuario { get; set; }
        public string Fecha { get; set; }
        public string Descripcion { get; set; }
    }
}
