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
    public class NEstacionMeteorologicaController : ControllerBase
    {
        private readonly AppDbContext context;
        public NEstacionMeteorologicaController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        [Route("listarEstacionM")]
        public ActionResult Get()
        {
            try
            {
                return Ok(context.NESTACIONMETEOROLOGICA.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}", Name = "GetEstacionM")]
        public ActionResult Get(int id)
        {
            try
            {
                var EstacionM = context.NESTACIONMETEOROLOGICA.FirstOrDefault(f => f.Id_estacion_meteorologica == id);
                return Ok(EstacionM);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<ValuesController>
        [HttpPost]
        [Route("saveEstacionM")]
        public ActionResult Post([FromBody] NEstacionMeteorolica EstacionM)
        {
            try
            {
                var EstacionMVal = context.NESTACIONMETEOROLOGICA.FirstOrDefault(f => f.Nombre == EstacionM.Nombre);
                if (EstacionMVal != null)
                {
                    return Ok("Estacion Meteorologica ya registrado");
                }
                else
                {
                    context.NESTACIONMETEOROLOGICA.Add(EstacionM);
                    context.SaveChanges();
                    return CreatedAtRoute("GetEstacionM", new { id = EstacionM.Id_estacion_meteorologica }, EstacionM);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] NEstacionMeteorolica EstacionM)
        {
            try
            {
                if (EstacionM.Id_estacion_meteorologica == id)
                {
                    context.Entry(EstacionM).State = EntityState.Modified;
                    context.SaveChanges();
                    LoginResponseModel actEstado = new LoginResponseModel()
                    {
                        Token = "ok",
                        Mensaje = "actualizacion Exitosa",
                        Respuesta = 1
                    };
                    return Ok(actEstado);
                }
                else
                {
                    LoginResponseModel actEstado = new LoginResponseModel()
                    {
                        Respuesta = 0,
                        Mensaje = "actualizacion con error",
                    };
                    return Ok(actEstado);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
