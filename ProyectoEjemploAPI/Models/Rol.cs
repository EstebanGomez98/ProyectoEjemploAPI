using System.ComponentModel.DataAnnotations;

namespace ProyectoEjemploAPI.Models
{
    public class Rol
    {
        [Key]
        public int Id_rol { get; set; }
        public string Descripcion { get; set; }
    }
}
