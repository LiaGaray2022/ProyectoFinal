using Prueba1Poke.Models;
using Prueba1Poke.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Xamarin.Forms;

namespace Prueba1Poke.ViewModels
{
    public class viewModelMainPage : INotifyPropertyChanged
    {

        public viewModelMainPage()
        {

            try
            {
                // Abrir y leer el archivo
                byte[] data = File.ReadAllBytes(ruta);
                MemoryStream memory = new MemoryStream(data);
                BinaryFormatter formatter = new BinaryFormatter();
                u = (Usuarios)formatter.Deserialize(memory);
                memory.Close();
            }
            catch (FileNotFoundException)
            {
                // El archivo no existe, se crea una nueva lista vacía
                u = new Usuarios();
            }


            navegarCrearUsuario = new Command(async () => {


                var pagina = new viewCreacionUsuarios();

                // Push Async es para redirigir a Otra Pagina
                await Application.Current.MainPage.Navigation.PushAsync(pagina);


            });


            autenticar = new Command(() => {

                if (u.autenticacion(this.CorreoElectronico, this.Contrasenia) == true)
                {

                    var pagina = new viewCategorias();

                    Application.Current.MainPage.Navigation.PushAsync(pagina);

                }
                else
                {

                    ResultadoAuth = "Autenticacion Erronea";

                }

            });

        }

        string ruta = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "usuario.bin");

        Usuarios u;

        public Usuarios U
        {
            get => u;
            set
            {
                u = value;
                OnPropertyChanged(nameof(U));
            }
        }


        string correoElectronico;

        public string CorreoElectronico
        {
            get => correoElectronico;
            set
            {
                correoElectronico = value;
                OnPropertyChanged(nameof(CorreoElectronico));
            }
        }

        string resultadoAuth;

        public string ResultadoAuth
        {
            get => resultadoAuth;
            set
            {
                resultadoAuth = value;
                OnPropertyChanged(nameof(ResultadoAuth));
            }
        }

        string contrasenia;

        public string Contrasenia
        {

            get => contrasenia;
            set
            {
                contrasenia = value;
                OnPropertyChanged(nameof(Contrasenia));
            }

        }

        public Command navegarCrearUsuario { get; }
        public Command autenticar { get; }


        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
