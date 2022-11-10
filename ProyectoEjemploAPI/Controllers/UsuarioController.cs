using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoEjemploAPI.Context;
using ProyectoEjemploAPI.Models;
using ProyectoEjemploAPI.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProyectoEjemploAPI.Utilities;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyectoEjemploAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext context;
        public UsuarioController(AppDbContext context)
        {
            this.context = context;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        [Route("listarUsuarios")]
        [Authorize]
        public ActionResult Get()
        {
            try
            {
                return Ok(context.USUARIO.ToList());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}", Name = "GetUser")]
        //[Authorize]
        public ActionResult Get(int id)
        {
            try
            {
                var user = context.USUARIO.FirstOrDefault(f => f.Id_usuario == id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<ValuesController>
        [HttpPost]
        [Route("saveUsuarios")]
        //[Authorize]
        public ActionResult Post([FromBody] Usuario user)
        {
            try
            {
                var userVal = context.USUARIO.FirstOrDefault(f => f.Email == user.Email);
                if (userVal != null)
                {
                    LoginResponseModel actEstado = new LoginResponseModel()
                    {
                        Respuesta = 0,
                        Mensaje = "Correo ya registrado ya registrado",
                    };
                    return BadRequest(actEstado);
                }
                else
                {
                    string result = string.Empty;
                    byte[] encryted = System.Text.Encoding.Unicode.GetBytes(user.Pass);  
                    result = Convert.ToBase64String(encryted);
                    //Console.WriteLine("clave encriptada:" + result+ "Aqui");
                    user.Pass=result;
                    context.USUARIO.Add(user);
                    context.SaveChanges();
                    LoginResponseModel actEstado = new LoginResponseModel()
                    {
                        Mensaje = "Creacion de usuario Exitoso",
                        Respuesta = 1
                    };
                    return Ok(actEstado);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //PUT
        [HttpPut("{id}")]
        [Authorize]
        public ActionResult Put(int id, [FromBody] Usuario user)
        {
            try
            {
                if (user.Id_usuario == id)
                {
                    context.Entry(user).State = EntityState.Modified;
                    context.SaveChanges();
                    LoginResponseModel actEstado = new LoginResponseModel()
                    {
                        Mensaje = "Actualizacion de usuario Exitosa",
                        Respuesta = 1
                    };
                    return Ok(actEstado);
                }
                else
                {
                    LoginResponseModel actEstado = new LoginResponseModel()
                    {
                        Respuesta = 0,
                        Mensaje = "Actualizacion de usuario Erronea",
                    };
                    return Ok(actEstado);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //--------
    }
}