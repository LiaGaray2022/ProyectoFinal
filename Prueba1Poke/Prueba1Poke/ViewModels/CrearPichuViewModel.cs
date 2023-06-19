using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Prueba1Poke.Models;
using Xamarin.Forms;

namespace Prueba1Poke.ViewModels
{
    public class CrearPichuViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Pokemon> Pokemones { get; set; } = new ObservableCollection<Pokemon>();

        public ObservableCollection<PokemonInfo> PokemonInfoList { get; set; } = new ObservableCollection<PokemonInfo>();

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

        public Command GuardarPokemonCommand { get; }
        public Command EvolucionarPokemonCommand { get; }
        public Command GenerarReporteCommand { get; }

        public CrearPichuViewModel()
        {
            GuardarPokemonCommand = new Command(GuardarPokemon);
            EvolucionarPokemonCommand = new Command(EvolucionarPokemon);
            GenerarReporteCommand = new Command(GenerarReporte);
        }

        private void GuardarPokemon()
        {
            if (string.IsNullOrWhiteSpace(Nombre))
            {
                Application.Current.MainPage.DisplayAlert("Error", "Debes ingresar un nombre para el Pokémon", "OK");
                return;
            }

            Pokemon nuevoPokemon = new Pichu(Nombre, DateTime.Now);

            Pokemones.Add(nuevoPokemon);

            PokemonInfoList.Add(new PokemonInfo
            {
                NombrePokemon = nuevoPokemon.Nombre,
                FechaNacimiento = nuevoPokemon.FechaNacimiento,
                NombreEvolucion = ObtenerUltimaEvolucion(nuevoPokemon)
            });

            Application.Current.MainPage.DisplayAlert("Éxito", "Pokémon guardado correctamente", "OK");
        }

        private void EvolucionarPokemon()
        {
            if (SelectedPokemon == null)
            {
                Application.Current.MainPage.DisplayAlert("Error", "Debes seleccionar un Pokémon para evolucionar", "OK");
                return;
            }

            if (SelectedPokemon is IPokemonEvolucionable evolucionable)
            {
                string siguienteEvolucion = evolucionable.ObtenerSiguienteEvolucion();
                Pokemon evolucion = evolucionable.CrearEvolucion();

                string cambioDePoder = ObtenerCambioDePoder(evolucion);

                int index = Pokemones.IndexOf(SelectedPokemon);
                if (index != -1)
                {
                    Pokemones[index] = evolucion;

                    PokemonInfoList[index].NombrePokemon = evolucion.Nombre;
                    PokemonInfoList[index].NombreEvolucion = $"{siguienteEvolucion} ({cambioDePoder})";

                    Application.Current.MainPage.DisplayAlert("Éxito", $"{SelectedPokemon.Nombre} ha evolucionado a {siguienteEvolucion} ({cambioDePoder})!", "OK");
                }
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Error", "El Pokémon seleccionado no puede evolucionar", "OK");
            }
        }

        private string ObtenerCambioDePoder(Pokemon pokemon)
        {
            if (pokemon is Pichu)
            {
                return "Cambio de poder: Amistad -> Piedra Trueno";
            }
            else if (pokemon is Pikachu)
            {
                return "Cambio de poder: Piedra Trueno -> Carga Eléctrica";
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

    public class PokemonInfo : INotifyPropertyChanged
    {
        private string nombrePokemon;
        public string NombrePokemon
        {
            get => nombrePokemon;
            set
            {
                if (nombrePokemon != value)
                {
                    nombrePokemon = value;
                    OnPropertyChanged(nameof(NombrePokemon));
                }
            }
        }

        private DateTime fechaNacimiento;
        public DateTime FechaNacimiento
        {
            get => fechaNacimiento;
            set
            {
                if (fechaNacimiento != value)
                {
                    fechaNacimiento = value;
                    OnPropertyChanged(nameof(FechaNacimiento));
                }
            }
        }

        private string nombreEvolucion;
        public string NombreEvolucion
        {
            get => nombreEvolucion;
            set
            {
                if (nombreEvolucion != value)
                {
                    nombreEvolucion = value;
                    OnPropertyChanged(nameof(NombreEvolucion));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
