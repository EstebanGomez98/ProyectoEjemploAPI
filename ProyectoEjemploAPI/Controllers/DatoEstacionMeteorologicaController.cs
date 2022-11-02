using ProyectoEjemploAPI.Context;
using ProyectoEjemploAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyectoEjemploAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatoEstacionMeteorologicaController : ControllerBase
    {
        private readonly AppDbContext context;
        public DatoEstacionMeteorologicaController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        [Route("listarDatoEstacionM")]
        //[Authorize]
        public ActionResult Get()
        {
            try
            {
                return Ok(context.DATOESTACIONMETEOROLOGICA.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // POST api/<ValuesController>
        [HttpPost]
        [Route("saveDatoEstacionM")]
        //[Authorize]
        public ActionResult Post([FromBody] DatoEstacionMeteorologica datoEstacionM)
        {
            try
            {
                context.DATOESTACIONMETEOROLOGICA.Add(datoEstacionM);
                context.SaveChanges();
                return CreatedAtRoute("GetDatoEstacionM", new { id = datoEstacionM.Id_dato_estacion_meteorologica }, datoEstacionM);
            
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
