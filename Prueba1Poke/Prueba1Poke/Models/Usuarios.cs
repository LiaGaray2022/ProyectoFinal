using System;
using System.Collections.Generic;
using System.Text;

namespace Prueba1Poke.Models
{

    [Serializable]
    public class Usuarios
    {

        public string correo;
        public string contrasenia;

        public bool autenticacion(string email, string pass)
        {

            if (correo == email && contrasenia == pass)
            {

                return true;

            }

            return false;
        }

        public override string ToString()
        {

            return $" {correo} - {contrasenia} ";

        }

    }
}
