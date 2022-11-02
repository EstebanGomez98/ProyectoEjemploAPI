using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoEjemploAPI.RequestModels;
using ProyectoEjemploAPI.ResponseModels;
using ProyectoEjemploAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoEjemploAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        public AuthenticationController(IAuthenticationService authService)
        {
            this._authService = authService;
        }
        [AllowAnonymous]
        [HttpPost, Route("requestToken")]
        public ActionResult RequestToken([FromBody] LoginRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Request");
            }

            string token;
            if (_authService.IsAuthenticated(request, out token))
            {
                LoginResponseModel loginResponseModel = new LoginResponseModel()
                {
                    Token = token,
                    Mensaje = "Login Exitoso",
                    Respuesta = 1
                };
                return Ok(loginResponseModel);
            }
            else
            {
                LoginResponseModel loginResponseModel = new LoginResponseModel()
                {
                    Respuesta = 0,
                    Mensaje = "Login con error",
                };
                return Ok(loginResponseModel);
            }

            return BadRequest("Invalid Request");

        }
    }
}
