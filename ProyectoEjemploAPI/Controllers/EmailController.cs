using Microsoft.AspNetCore.Mvc;
using ProyectoEjemploAPI.Context;
using ProyectoEjemploAPI.Models;
using ProyectoEjemploAPI.RequestModels;
using ProyectoEjemploAPI.ResponseModels;
using ProyectoEjemploAPI.Utilities;
using System;
using System.Linq;

namespace ProyectoEjemploAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly AppDbContext context;
        public EmailController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        [Route("sendEmail")]
        public ActionResult RequestToken([FromBody] EmailRequestModel request)
        {
            EmailService emailServises = new EmailService();
            EmailResponseModel emailResponseModel = new EmailResponseModel();
    
            var userv = context.USUARIO.Where(f => f.Email == request.Destinatario).FirstOrDefault();
            if (userv != null)
            {
                string result = string.Empty;
                byte[] decryted = Convert.FromBase64String(userv.Pass);
                result = System.Text.Encoding.Unicode.GetString(decryted);
                userv.Pass = result;
                request.Mensaje += "    Contraseña:  " + userv.Pass;
            }
            else
            {
                emailResponseModel.Respuesta = 0;
                emailResponseModel.Mensaje = "Error Correo no registrado  ";
                return BadRequest(emailResponseModel);
            }
            
            try
            {
                emailServises.EnviarCorreo(request.Destinatario, request.Aasunto, request.Mensaje, request.EsHtml);
                emailResponseModel.Respuesta = 1;
                emailResponseModel.Mensaje = "Correo enviado correctamente";
                return Ok(emailResponseModel);
            }
            catch (Exception e)
            {
                emailResponseModel.Respuesta = 0;
                emailResponseModel.Mensaje = "Error de Correo:  " + e;
                return BadRequest(emailResponseModel);
            }
        }
    }
}
