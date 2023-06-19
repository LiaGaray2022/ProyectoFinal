using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Prueba1Poke.Models;
using Xamarin.Forms;

namespace Prueba1Poke.ViewModels
{
    public class CrearSquirtleViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Pokemon> Pokemones { get; set; } = new ObservableCollection<Pokemon>();

        private Pokemon selectedPokemon;
        public Pokemon SelectedPokemon
        {
            get => selectedPokemon;
            set
            {
                if (selectedPokemon != value)
                {
                    selectedPokemon = value;
                    OnPropertyChanged(nameof(SelectedPokemon));
                }
            }
        }

        private string nombre;
        public string Nombre
        {
            get => nombre;
            set
            {
                if (nombre != value)
                {
                    nombre = value;
                    OnPropertyChanged(nameof(Nombre));
                }
            }
        }

        public ICommand GuardarPokemonCommand { get; }
        public ICommand EvolucionarPokemonCommand { get; }
        public DateTime FechaNacimiento { get; private set; }
        public Command GenerarReporteCommand { get; }

        public static CrearSquirtleViewModel Instance { get; } = new CrearSquirtleViewModel();

        public CrearSquirtleViewModel()
        {
            GuardarPokemonCommand = new Command(GuardarPokemon);
            EvolucionarPokemonCommand = new Command(EvolucionarPokemon);
            GenerarReporteCommand = new Command(GenerarReporte);
        }

        private void GuardarPokemon()
        {
            if (string.IsNullOrWhiteSpace(Nombre)) // Validar si el nombre está en blanco o es nulo
            {
                Application.Current.MainPage.DisplayAlert("Error", "Debes ingresar un nombre para el Pokémon", "OK");
                return;
            }

            // Crear una nueva instancia de Pokemon con el nombre proporcionado
            Pokemon nuevoPokemon = new Squirtle(Nombre, FechaNacimiento);

            // Agregar el nuevo Pokémon a la colección
            Pokemones.Add(nuevoPokemon);

            // Mostrar mensaje de confirmación
            Application.Current.MainPage.DisplayAlert("Éxito", "Pokémon guardado correctamente", "OK");
        }


        private void EvolucionarPokemon()
        {
            if (SelectedPokemon == null)
            {
                Application.Current.MainPage.DisplayAlert("Error", "Debes seleccionar un Pokémon para evolucionar", "OK");
                return;
            }

            // Verificar si el Pokémon seleccionado puede evolucionar
            if (SelectedPokemon is IPokemonEvolucionable evolucionable)
            {
                // Obtener los datos de la evolución
                string siguienteEvolucion = evolucionable.ObtenerSiguienteEvolucion();
                Pokemon evolucion = evolucionable.CrearEvolucion();
                string cambioDePoder = ObtenerCambioDePoder(evolucion);

                // Reemplazar el Pokémon seleccionado con la evolución
                int index = Pokemones.IndexOf(SelectedPokemon);
                if (index != -1)
                {
                    Pokemones[index] = evolucion;

                    // Mostrar mensaje de confirmación
                    Application.Current.MainPage.DisplayAlert("Éxito", $"{SelectedPokemon.Nombre} ha evolucionado a {siguienteEvolucion} ({cambioDePoder})!", "OK");
                }
            }
            else
            {
                // El Pokémon seleccionado no puede evolucionar
                Application.Current.MainPage.DisplayAlert("Error", "El Pokémon seleccionado no puede evolucionar", "OK");
            }
        }


        private string ObtenerCambioDePoder(Pokemon pokemon)
        {
           if (pokemon is Squirtle)
            {
                return "Cambio de poder: Agua -> Nadador";
            }
            else if (pokemon is Wartortle)
            {
                return "Cambio de poder: Nadador -> Lanza Agua";
            }
            else
            {
                return "Cambio de poder desconocido";
            }
        }

        public string ObtenerUltimaEvolucion(Pokemon pokemon)
        {
            string ultimaEvolucion = pokemon.Nombre;
            while (pokemon is IPokemonEvolucionable evolucionable)
            {
                Pokemon evolucion = evolucionable.CrearEvolucion();
                if (evolucion != null)
                {
                    ultimaEvolucion = evolucion.Nombre;
                    pokemon = evolucion;
                }
                else
                {
                    break;
                }
            }
            return ultimaEvolucion;
        }
        private void GenerarReporte()
        {
            StringBuilder reporte = new StringBuilder();
            reporte.AppendLine("Reporte de Pokémones Creados:");
            reporte.AppendLine();

            foreach (Pokemon pokemon in Pokemones)
            {
                reporte.AppendLine($"Nombre: {pokemon.Nombre}");
                reporte.AppendLine($"Fecha de Nacimiento: {pokemon.FechaNacimiento:d}");
                reporte.AppendLine($"Evolución: {ObtenerUltimaEvolucion(pokemon)}");
                reporte.AppendLine();
            }

            Application.Current.MainPage.DisplayAlert("Reporte de Pokémones", reporte.ToString(), "OK");
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
