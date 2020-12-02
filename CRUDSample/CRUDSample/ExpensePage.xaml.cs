using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRUDSample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExpensePage : ContentPage
    {
        public ExpensePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            PopulateSalidaList();
        }

        public void PopulateSalidaList()
        {
            SaliList.ItemsSource = null;
            SaliList.ItemsSource = DependencyService.Get<ISQLite>().GetSalidas();
        }

        private void AddSalida(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddSalida(null));
        }

        private void EditSalida(object sender, ItemTappedEventArgs e)
        {
            Salida details = e.Item as Salida;
            if (details != null)
            {
                Navigation.PushAsync(new AddSalida(details));
            }
        }

        private async void DeleteSalida(object sender, EventArgs e)
        {
            bool res = await DisplayAlert("Message", "¿Quieres eliminar este empleado?", "Ok", "Cancelar");
            if (res)
            {
                var menu = sender as MenuItem;
                Salida details = menu.CommandParameter as Salida;
                DependencyService.Get<ISQLite>().DeleteSalida(details.Ids);
                PopulateSalidaList();
            }
        }
    }
}