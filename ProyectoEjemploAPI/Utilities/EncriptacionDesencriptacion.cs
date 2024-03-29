﻿using System;

namespace ProyectoEjemploAPI.Utilities
{
    public class EncriptacionDesencriptacion
    {
        public string Encriptar(string clave)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(clave);
            result = Convert.ToBase64String(encryted);
            Console.WriteLine("clave encriptada" + result);
            return (result);
        }
        public string DesEncriptar(string claveE)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(claveE);
            //result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            Console.WriteLine("clave desencriptada" + result);
            return result;
        }
    }
}
