using Prueba1Poke.Models;
using Prueba1Poke.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace Prueba1Poke.ViewModels
{
    public class viewModelCategorias : INotifyPropertyChanged
    {

        public viewModelCategorias()
        {


            lista.Add(new Categorias()
            {

                nombre = "Creación de Pichu",
                urlCategoria = "https://th.bing.com/th/id/OIP.jDpQuKnTsWDWKCeUbCVuswHaJf?pid=ImgDet&rs=1"

            });

            lista.Add(new Categorias()
            {

                nombre = "Creación de Charmander",
                urlCategoria = "https://th.bing.com/th/id/OIP.kXxQ5EozqR2SBoZ7fakNDAHaHl?pid=ImgDet&rs=1"

            });


            lista.Add(new Categorias()
            {

                nombre = "Creación de Squirtle",
                urlCategoria = "https://mestrepokemon.com/wp-content/uploads/2019/11/Squirtle-Pok%C3%A9dex.jpg"

            });

           

            redirigirCategoria = new Command(async () =>
            {
                switch (ObjetoSeleccionado.nombre)
                {
                    case "Creación de Pichu":
                        await Application.Current.MainPage.Navigation.PushAsync(new CrearPichuView());
                        break;

                    case "Creación de Charmander":
                        await Application.Current.MainPage.Navigation.PushAsync(new CrearCharmanderView());
                        break;

                    case "Creación de Squirtle":
                        await Application.Current.MainPage.Navigation.PushAsync(new CrearSquirtleView());
                        break;
                    default:

                      await Application.Current.MainPage.DisplayAlert("Error", "Objeto no Encontrado", "OK");

                        break;



                }


            });


        }

        Categorias objetoSeleccionado;
        public Categorias ObjetoSeleccionado
        {

            get => objetoSeleccionado;
            set
            {
                objetoSeleccionado = value;
                OnPropertyChanged(nameof(ObjetoSeleccionado));
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<Categorias> lista { get; set; } = new ObservableCollection<Categorias>();

        public Command redirigirCategoria { get; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

