using System.ComponentModel.DataAnnotations;

namespace ProyectoEjemploAPI.Models
{
    public class Estado
    {
        [Key]
        public int Id_estado { get; set; }
        public string Descripcion { get; set; }
    }
}
