using System;
using System.Collections.Generic;
using System.Text;

namespace Prueba1Poke.Models
{
    public class Pokemon
    {
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public Pokemon(string nombre, DateTime fechaNacimiento)
        {
            Nombre = nombre;
            FechaNacimiento = fechaNacimiento;
        }

        public virtual string ObtenerSiguienteEvolucion()
        {
            return "";
        }

        public virtual Pokemon CrearEvolucion()
        {
            return null;
        }

        public virtual void ImprimirDatos()
        {
            Console.WriteLine("Nombre: " + Nombre);
            Console.WriteLine("Fecha de nacimiento: " + FechaNacimiento.ToString("yyyy-MM-dd"));
        }
    }
}
