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
	public partial class AddEmployeePage : ContentPage
	{
        Employee employeeDetails;
		public AddEmployeePage (Employee details)
		{
			InitializeComponent ();
            if(details!=null)
            {
                employeeDetails = details;
                PopulateDetails(employeeDetails);
            }
		}

        private void PopulateDetails(Employee details)
        {
            name.Text = details.Name;
            dui.Text = details.Dui;
            nit.Text = details.Nit;
            saveBtn.Text = "Modificar";
            this.Title = "Editar Empleado";
        }

        private void SaveEmployee(object sender, EventArgs e)
        {
            if(saveBtn.Text == "Guardar")
            {
                Employee employee = new Employee();
                employee.Name = name.Text;
                employee.Dui = dui.Text;
                employee.Nit = nit.Text;

                bool res = DependencyService.Get<ISQLite>().SaveEmployee(employee);
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
                employeeDetails.Name = name.Text;
                employeeDetails.Dui = dui.Text;
                employeeDetails.Nit = nit.Text;
                

                bool res =DependencyService.Get<ISQLite>().UpdateEmployee(employeeDetails);
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