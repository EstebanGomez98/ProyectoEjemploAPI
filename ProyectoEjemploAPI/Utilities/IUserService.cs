using ProyectoEjemploAPI.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoEjemploAPI.Utilities
{
    public interface IUserService
    {
        bool IsValid(LoginRequestModel req);
    }
}
