using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Prueba1Poke.Models;
using Xamarin.Forms;

namespace Prueba1Poke.ViewModels
{
    public class CrearCharmanderViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Pokemon> Pokemones { get; set; } = new ObservableCollection<Pokemon>();

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

        private string fechaNacimiento;
        public string FechaNacimiento
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

        public ICommand GuardarPokemonCommand { get; }

        public CrearCharmanderViewModel()
        {
            GuardarPokemonCommand = new Command(GuardarPokemon);
        }

        private void GuardarPokemon()
        {
            // Validar que se hayan ingresado el nombre y la fecha de nacimiento
            if (string.IsNullOrWhiteSpace(Nombre) || string.IsNullOrWhiteSpace(FechaNacimiento))
            {
                Application.Current.MainPage.DisplayAlert("Error", "Debes ingresar el nombre y la fecha de nacimiento", "OK");
                return;
            }

            // Crear el objeto Charmander con los datos ingresados
            Pokemon charmander = new Pokemon(Nombre, DateTime.Parse(FechaNacimiento));
            {
               
            };

            // Agregar el Charmander a la colección de Pokemones
            Pokemones.Add(charmander);

            // Mostrar mensaje de confirmación
            Application.Current.MainPage.DisplayAlert("Éxito", "¡Tu Pokémon ha sido creado!", "OK");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
