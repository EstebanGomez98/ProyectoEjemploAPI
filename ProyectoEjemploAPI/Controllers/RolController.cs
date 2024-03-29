﻿using ProyectoEjemploAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoEjemploAPI.Models;
using System;
using System.Linq;
using ProyectoEjemploAPI.Context;
using ProyectoEjemploAPI.ResponseModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyectoEjemploAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly AppDbContext context;
        public RolController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        [Route("listarRoles")]
        public ActionResult Get()
        {
            try
            {
                return Ok(context.ROL.ToList());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}", Name = "GetRol")]
        public ActionResult Get(int id)
        {
            try
            {
                var rol = context.ROL.FirstOrDefault(f => f.Id_rol == id);
                return Ok(rol);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<ValuesController>
        [HttpPost]
        [Route("saveRol")]
        public ActionResult Post([FromBody] Rol rol)
        {
            try
            {
                var rolVal = context.ROL.FirstOrDefault(f => f.Descripcion == rol.Descripcion);
                if (rolVal != null)
                {
                    return Ok("Rol ya registrado");
                }
                else
                {
                    context.ROL.Add(rol);
                    context.SaveChanges();
                    return CreatedAtRoute("GetUser", new { id = rol.Id_rol }, rol);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Rol rol)
        {
            try
            {
                if (rol.Id_rol == id)
                {
                    context.Entry(rol).State = EntityState.Modified;
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
