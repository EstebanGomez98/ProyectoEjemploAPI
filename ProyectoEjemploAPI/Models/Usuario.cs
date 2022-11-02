using System.ComponentModel.DataAnnotations;

namespace ProyectoEjemploAPI.Models
{
    public class Usuario
    {
        [Key]
        public int Id_usuario { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public int Id_rol { get; set; }
        public int Id_estado { get; set; }
    }
}
