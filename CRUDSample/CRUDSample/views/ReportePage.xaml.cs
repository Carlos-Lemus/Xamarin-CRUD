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
    public partial class ReportePage : ContentPage
    {
        public ReportePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            PopulateGastoList();
        }

        public void PopulateGastoList()
        {
            lblTotalGastos.Text = "Total de gastos: $ " + DependencyService.Get<ISQLite>().GetTotalGastos().ToString();
            GastosList.ItemsSource = null;
            GastosList.ItemsSource = DependencyService.Get<ISQLite>().GetGastosEmployee();
        }

        private void FilterGastosEmployee(object sender, EventArgs e)
        {
            GastosList.ItemsSource = null;

            string fechaInicio = dpInicio.Date.ToShortDateString().ToString();
            string fechaFinal = dpFinal.Date.ToShortDateString().ToString();

            GastosList.ItemsSource = DependencyService.Get<ISQLite>().FilterGastosEmployee(fechaInicio, fechaFinal);
        }
    }
}