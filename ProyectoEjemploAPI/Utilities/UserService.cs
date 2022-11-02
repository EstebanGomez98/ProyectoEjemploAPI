using ProyectoEjemploAPI.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoEjemploAPI.Utilities
{
    public class UserService : IUserService
    {
        // Prueba de simulación, el valor predeterminado es verificación artificial efectiva
        public bool IsValid(LoginRequestModel req)
        {
            return true;
        }
    }
}
