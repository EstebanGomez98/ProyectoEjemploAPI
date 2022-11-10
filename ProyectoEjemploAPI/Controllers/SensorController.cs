using ProyectoEjemploAPI.Context;
using ProyectoEjemploAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using ProyectoEjemploAPI.ResponseModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyectoEjemploAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorController : ControllerBase
    {
        private readonly AppDbContext context;
        public SensorController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        [Route("listarsensores")]
        public ActionResult Get()
        {
            try
            {
                return Ok(context.SENSOR.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}", Name = "GetSensor")]
        public ActionResult Get(int id)
        {
            try
            {
                var sensor = context.SENSOR.FirstOrDefault(f => f.Id_sensor == id);
                return Ok(sensor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<ValuesController>
        [HttpPost]
        [Route("saveSensor")]
        public ActionResult Post([FromBody] Sensor sensor)
        {
            try
            {
                var sensorVal = context.SENSOR.FirstOrDefault(f => f.Descripcion != sensor.Descripcion);
                if (sensorVal != null)
                {
                    context.SENSOR.Add(sensor);
                    context.SaveChanges();
                    LoginResponseModel actEstado = new LoginResponseModel()
                    {
                        Token = "ok",
                        Mensaje = "Guardado Exitosamente",
                        Respuesta = 1
                    };
                    return Ok(actEstado);
                }
                else
                {
                    LoginResponseModel actEstado = new LoginResponseModel()
                    {
                        Respuesta = 0,
                        Mensaje = "Sensor ya registrado",
                    };
                    return Ok(actEstado);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public ActionResult Put([FromBody] Sensor sensor)
        {
            try
            {
                LoginResponseModel actEstado = new LoginResponseModel();
                var obj = context.SENSOR.Find(sensor.Id_sensor);
                if (obj == null)
                {
                    actEstado.Mensaje = "No se encontro el sensor";
                    actEstado.Respuesta = 0;
                    return BadRequest(actEstado);
                }
                //context.Entry(sensor).State = EntityState.Modified; sensor.Id_estado where sensor.Id_sensor
                if (obj.Id_estado != sensor.Id_estado)
                {
                    obj.Id_estado = sensor.Id_estado;
                    obj.Descripcion = obj.Descripcion;
                    context.Entry(obj).State = EntityState.Modified;
                    context.SaveChanges();
                    actEstado.Mensaje = "Actualizacion exitosa";
                    actEstado.Respuesta = 1;
                }else if (obj.Descripcion != sensor.Descripcion)
                {
                    obj.Id_estado = obj.Id_estado;
                    obj.Descripcion = sensor.Descripcion;
                    context.Entry(obj).State = EntityState.Modified;
                    context.SaveChanges();
                    actEstado.Mensaje = "Actualizacion exitosa";
                    actEstado.Respuesta = 1;
                }
                return Ok(actEstado);

            }
            catch (Exception ex)
            {
                LoginResponseModel actEstado = new LoginResponseModel()
                {
                    Respuesta = 0,
                    Mensaje = "actualizacion con error",
                };
                return BadRequest(actEstado);
            }
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                try
                {
                    var sensor = context.SENSOR.FirstOrDefault(f => f.Id_sensor == id);
                    if (sensor != null)
                    {
                        context.SENSOR.Remove(sensor);
                        context.SaveChanges();
                        return Ok(id);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
