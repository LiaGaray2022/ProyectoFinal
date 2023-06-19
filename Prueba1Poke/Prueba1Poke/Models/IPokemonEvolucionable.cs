using System;
using Xamarin.Forms;

public interface IPokemonEvolucionable
{
    string ObtenerSiguienteEvolucion();
    Pokemon CrearEvolucion();
}

public class Pokemon : IPokemonEvolucionable
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
        return "LLegó a su limite de evoluciones, bais";
    }

    public virtual Pokemon CrearEvolucion()
    {
        return null;
    }
}

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

public class Raichu : Pokemon
{
    public Raichu(string nombre, DateTime fechaNacimiento) : base(nombre, fechaNacimiento)
    {
    }
}
