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
    public partial class EmployeePage : ContentPage
    {
        public EmployeePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            PopulateEmployeeList();
        }
        public void PopulateEmployeeList()
        {
            EmployeeList.ItemsSource = null;
            EmployeeList.ItemsSource = DependencyService.Get<ISQLite>().GetEmployees();
        }

        private void AddEmployee(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddEmployeePage(null));
        }

        private void EditEmployee(object sender, ItemTappedEventArgs e)
        {
            Employee details = e.Item as Employee;
            if (details != null)
            {
                Navigation.PushAsync(new AddEmployeePage(details));
            }
        }

        private async void DeleteEmployee(object sender, EventArgs e)
        {
            bool res = await DisplayAlert("Message", "¿Quieres eliminar este empleado?", "Ok", "Cancelar");
            if (res)
            {
                var menu = sender as MenuItem;
                Employee details = menu.CommandParameter as Employee;
                DependencyService.Get<ISQLite>().DeleteEmployee(details.Id);
                PopulateEmployeeList();
            }
        }
    }
}