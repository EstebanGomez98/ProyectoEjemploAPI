using ProyectoEjemploAPI.Context;
using ProyectoEjemploAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;
using ProyectoEjemploAPI.ResponseModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyectoEjemploAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class EstadoController : ControllerBase
    {
        private readonly AppDbContext context;
        public EstadoController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        [Route("listarEstados")]
        public ActionResult Get()
        {
            try
            {
                return Ok(context.ESTADO.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<ValuesController>
        [HttpPost]
        [Route("saveEstado")]
        public ActionResult Post([FromBody] Estado estado)
        {
            try
            {
                var estadoVal = context.ESTADO.FirstOrDefault(f => f.Descripcion == estado.Descripcion);
                if (estadoVal != null)
                {
                    return Ok("Estado ya registrado");
                }
                else
                {
                    context.ESTADO.Add(estado);
                    context.SaveChanges();
                    return CreatedAtRoute("GetEstado", new { id = estado.Id_estado }, estado);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Estado estado)
        {
            try
            {
                if (estado.Id_estado == id)
                {
                    context.Entry(estado).State = EntityState.Modified;
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
