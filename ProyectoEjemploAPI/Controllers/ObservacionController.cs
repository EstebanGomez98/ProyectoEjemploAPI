using ProyectoEjemploAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoEjemploAPI.Models;
using System;
using System.Linq;
using ProyectoEjemploAPI.Context;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyectoEjemploAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObservacionController : ControllerBase
    {
        private readonly AppDbContext context;
        public ObservacionController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        [Route("listarObservaciones")]
        public ActionResult Get()
        {
            try
            {
                return Ok(context.OBSERVACION.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}", Name = "GetObservacion")]
        public ActionResult Get(int id)
        {
            try
            {
                var observacion = context.OBSERVACION.FirstOrDefault(f => f.Id_observacion == id);
                return Ok(observacion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<ValuesController>
        [HttpPost]
        [Route("saveObservacion")]
        public ActionResult Post([FromBody] Observacion observacion)
        {
            try
            {
                context.OBSERVACION.Add(observacion);
                context.SaveChanges();
                return CreatedAtRoute("GetObservacion", new { id = observacion.Id_observacion }, observacion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Observacion observacion)
        {
            try
            {
                if (observacion.Id_observacion == id)
                {
                    context.Entry(observacion).State = EntityState.Modified;
                    context.SaveChanges();
                    return CreatedAtRoute("GetUsuario", new { id = observacion.Id_observacion }, observacion);
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
                    var observacion = context.OBSERVACION.FirstOrDefault(f => f.Id_observacion == id);
                    if (observacion != null)
                    {
                        context.OBSERVACION.Remove(observacion);
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
