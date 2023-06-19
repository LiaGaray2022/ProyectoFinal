using System;
using System.Collections.Generic;
using System.Text;

namespace Prueba1Poke.Models
{
    public class Pikachu : Pokemon
    {
        public Pikachu(string nombre, DateTime fechaNacimiento) : base(nombre, fechaNacimiento)
        {
        }

        public override string ObtenerSiguienteEvolucion()
        {
            return "Raichu";
        }

        public override Pokemon CrearEvolucion()
        {
            return new Raichu(Nombre, FechaNacimiento);
        }
    }
}