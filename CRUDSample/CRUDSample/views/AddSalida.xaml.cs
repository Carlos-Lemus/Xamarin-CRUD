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
        int id;
        public AddSalida(Salida details)
        {
            InitializeComponent();
            if (details != null)
            {
                salidaDetails = details;
                id = details.Ids;
                PopulateDetails(salidaDetails);
            }

            pickerEmployee.ItemsSource = DependencyService.Get<ISQLite>().getIdEmployee();
        }

        private void PopulateDetails(Salida details)
        {
            des.Text = details.Des;
            fecha.Date = DateTime.Parse(details.Fecha); 
            //DateTime.ParseExact(details.Fecha, "M/d/yyyy", null)
            monto.Text = details.Monto.ToString();
            Btn.Text = "Modificar";
            this.Title = "Editar Salida";
        }

        private void SaveSalida(object sender, EventArgs e)
        {
            if (Btn.Text == "Guardar")
            {
                Salida salida = new Salida();
                salida.Ids = id;
                salida.Des = des.Text;
                salida.Fecha = fecha.Date.ToShortDateString().ToString();
                salida.Monto = Convert.ToDouble(String.Format("{0:0.##}", monto.Text));
                salida.Id = Convert.ToInt32(pickerEmployee.SelectedItem.ToString());

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
                salidaDetails.Fecha = fecha.Date.ToShortDateString().ToString();
                salidaDetails.Monto = Convert.ToDouble(monto.Text);


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