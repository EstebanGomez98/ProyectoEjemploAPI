using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ProyectoEjemploAPI.Models
{
    public class DatoEstacionMeteorologica
    {
        [Key]
        public int Id_dato_estacion_meteorologica { get; set; }
        public int Id_estacion_meteorologica { get; set; }
        public string Fecha_hora { get; set; }
        public int Id_sensor { get; set; }
        public double Valor { get; set; }
    }
}
