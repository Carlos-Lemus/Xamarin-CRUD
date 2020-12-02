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
    public partial class AddSalida : ContentPage
    {
        Salida salidaDetails;
        public AddSalida(Salida details)
        {
            InitializeComponent();
            if (details != null)
            {
                salidaDetails = details;
                PopulateDetails(salidaDetails);
            }
        }

        private void PopulateDetails(Salida details)
        {
            des.Text = details.Des;
            fecha.Date = DateTime.ParseExact(details.Fecha, "M/d/yyyy", null);
            monto.Text = details.Monto;
            Btn.Text = "Modificar";
            this.Title = "Editar Salida";
        }

        private void SaveSalida(object sender, EventArgs e)
        {
            if (Btn.Text == "Guardar")
            {
                Salida salida = new Salida();
                salida.Des = des.Text;
                salida.Fecha = fecha.ToString();
                salida.Monto = monto.Text;

                bool res = DependencyService.Get<ISQLite>().SaveSalida(salida);
                if (res)
                {
                    Navigation.PopAsync();
                }
                else
                {
                    DisplayAlert("Message", "No se pudieron guardar los datos", "Ok");
                }
            }
            else
            {
                // update employee
                salidaDetails.Des = des.Text;
                salidaDetails.Fecha = fecha.ToString();
                salidaDetails.Monto = monto.Text;


                bool res = DependencyService.Get<ISQLite>().UpdateSalida(salidaDetails);
                if (res)
                {
                    Navigation.PopAsync();
                }
                else
                {
                    DisplayAlert("Message", "No se pudieron actualizar los datos", "Ok");
                }
            }
        }
    }
}