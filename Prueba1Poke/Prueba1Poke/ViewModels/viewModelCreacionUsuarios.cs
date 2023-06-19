using Prueba1Poke.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Xamarin.Forms;

namespace Prueba1Poke.ViewModels
{
    public class viewModelCreacionUsuarios : INotifyPropertyChanged
    {


        public viewModelCreacionUsuarios()
        {

            crearUsuario = new Command(() => {


                Usuarios u = new Usuarios()
                {

                    contrasenia = this.Contrasenia,
                    correo = this.CorreoElectronico

                };


                /* Rutina de Seralizacion */

                BinaryFormatter formatter = new BinaryFormatter();
                MemoryStream memory = new MemoryStream();
                formatter.Serialize(memory, u);
                byte[] serializedData = memory.ToArray();
                memory.Close();

                File.WriteAllBytes(ruta, serializedData);

                // Pop Async es para Regresar
                //Application.Current.MainPage.Navigation.PopAsync();

                var pagina = new MainPage();

                Application.Current.MainPage.Navigation.PushAsync(pagina);


            });


        }

        string ruta = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "usuario.bin");

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

        public Command crearUsuario { get; }


        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

