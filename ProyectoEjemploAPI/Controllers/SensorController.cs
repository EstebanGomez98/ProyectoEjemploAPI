using ProyectoEjemploAPI.Context;
using ProyectoEjemploAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

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
                var sensorVal = context.SENSOR.FirstOrDefault(f => f.Descripcion == sensor.Descripcion);
                if (sensorVal != null)
                {
                    return Ok("Sensor ya registrado");
                }
                else
                {
                    context.SENSOR.Add(sensor);
                    context.SaveChanges();
                    return CreatedAtRoute("GetSensor", new { id = sensor.Id_sensor }, sensor);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Sensor sensor)
        {
            try
            {
                if (sensor.Id_sensor == id)
                {
                    context.Entry(sensor).State = EntityState.Modified;
                    context.SaveChanges();
                    return CreatedAtRoute("GetSensor", new { id = sensor.Id_sensor }, sensor);
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
