using System;
using System.Collections.Generic;
using System.Text;

namespace Prueba1Poke.Models
{
    public class Pichu : Pokemon
    {
        public Pichu(string nombre, DateTime fechaNacimiento) : base(nombre, fechaNacimiento)
        {
        }

        public override string ObtenerSiguienteEvolucion()
        {
            return "Pikachu";
        }

        public override Pokemon CrearEvolucion()
        {
            return new Pikachu(Nombre, FechaNacimiento);
        }
    }

    

    public class Raichu : Pokemon
    {
        public Raichu(string nombre, DateTime fechaNacimiento) : base(nombre, fechaNacimiento)
        {
        }

        public override void ImprimirDatos()
        {
            base.ImprimirDatos();
            Console.WriteLine("Tipo: Eléctrico");
        }
    }
}
