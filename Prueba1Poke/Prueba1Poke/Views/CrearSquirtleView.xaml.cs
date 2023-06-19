using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prueba1Poke.ViewModels;

namespace Prueba1Poke.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CrearSquirtleView : ContentPage
    {
        public CrearSquirtleView()
        {
            InitializeComponent();
            BindingContext = new CrearSquirtleViewModel();
        }
    }
}
