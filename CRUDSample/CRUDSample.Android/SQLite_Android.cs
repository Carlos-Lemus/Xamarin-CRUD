using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CRUDSample.Droid;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_Android))]
namespace CRUDSample.Droid
{
    class SQLite_Android : ISQLite
    {
        SQLiteConnection con;
        public SQLiteConnection GetConnectionWithCreateDatabase()
        {
            string fileName = "sampleDatabase.db3";
            string documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string path = Path.Combine(documentPath, fileName);
            con = new SQLiteConnection(path);
            con.CreateTable<Salida>();
            con = new SQLiteConnection(path);
            con.CreateTable<Employee>();
            return con;
        }
        public bool SaveEmployee(Employee employee)
        {
            bool res = false;
            try
            {
                con.Insert(employee);
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
            }
            return res;
        }



        public List<Employee> GetEmployees()
        {
            string sql = "SELECT * FROM Employee";
            List<Employee> employees = con.Query<Employee>(sql);
            return employees;
        }
        public bool UpdateEmployee(Employee employee)
        {
            bool res = false;
            try
            {
                string sql = $"UPDATE Employee SET Name='{employee.Name}',Dui='{employee.Dui}',Nit='{employee.Nit}' WHERE Id={employee.Id}";
                con.Execute(sql);
                res = true;
            }
            catch (Exception ex)
            {

            }
            return res;
        }
        public void DeleteEmployee(int Id)
        {
            string sql = $"DELETE FROM Employee WHERE Id={Id}";
            con.Execute(sql);
        }


        public bool SaveSalida(Salida salida)
        {
            bool res = false;
            try
            {
                con.Insert(salida);
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
            }
            return res;
        }

        public List<Salida> GetSalidas()
        {
            string sql = "SELECT * FROM Salida";
            List<Salida> salidas = con.Query<Salida>(sql);
            return salidas;
        }
        public bool UpdateSalida(Salida salida)
        {
            bool res = false;
            try
            {
                string sql = $"UPDATE Salida SET Des='{salida.Des}',Fecha='{salida.Fecha}',Monto='{salida.Monto}' WHERE Ids={salida.Ids}";
                con.Execute(sql);
                res = true;
            }
            catch (Exception ex)
            {
                Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Mensaje", ex.Message, "Ok");
            }
            return res;
        }
        public void DeleteSalida(int Id)
        {
            string sql = $"DELETE FROM Salida WHERE Ids={Id}";
            con.Execute(sql);
        }

        public List<GastosEmployee> GetGastosEmployee()
        {

            List<GastosEmployee> gastos = new List<GastosEmployee>();

            foreach (Employee employee in GetEmployees())
            {
                GastosEmployee gasto = new GastosEmployee();
                gasto.Name = employee.Name;
                gasto.Fecha = "";
                double total = 0;

                foreach (Salida salida in GetSalidas())
                {
                    if (employee.Id == salida.Id)
                    {
                        if (total > 0)
                        {
                            gasto.Fecha += ", " + salida.Fecha;
                        }
                        else
                        {
                            gasto.Fecha += salida.Fecha;
                        }

                        total += salida.Monto;
                    }
                }
                gasto.Monto = total;

                gastos.Add(gasto);

            }

            return gastos;

        }

        public List<GastosEmployee> FilterGastosEmployee(string fechaInicio, string fechaFinal)
        {
            string[] fechaInicioItems = fechaInicio.Split("/");
            string[] fechaFinalItems = fechaFinal.Split("/");

            int dayInicio = Convert.ToInt32(fechaInicioItems[1]);
            int monthInicio = Convert.ToInt32(fechaInicioItems[0]);
            int yearInicio = Convert.ToInt32(fechaInicioItems[2]);

            int dayFinal = Convert.ToInt32(fechaFinalItems[1]);
            int monthFinal = Convert.ToInt32(fechaFinalItems[0]);
            int yearFinal = Convert.ToInt32(fechaFinalItems[2]);

            List<GastosEmployee> gastos = new List<GastosEmployee>();

            foreach (Employee employee in GetEmployees())
            {
                GastosEmployee gasto = null;

                foreach (Salida salida in GetSalidas())
                {
                    string[] fechaCurrent = salida.Fecha.Split("/");
                    int dayCurrent = Convert.ToInt32(fechaCurrent[1]);
                    int monthCurrent = Convert.ToInt32(fechaCurrent[0]);
                    int yearCurrent = Convert.ToInt32(fechaCurrent[2]);

                    if (employee.Id == salida.Id)
                    {
                        if (yearCurrent > yearInicio || yearCurrent < yearFinal)
                        {
                            if (yearCurrent > yearInicio && yearCurrent < yearFinal)
                            {
                                gasto = new GastosEmployee();

                                gasto.Name = employee.Name;
                                gasto.Monto = salida.Monto;
                                gasto.Fecha = salida.Fecha;

                                gastos.Add(gasto);

                                continue;
                            }
                            else if (yearCurrent >= yearInicio && yearCurrent < yearFinal)
                            {
                                if (monthCurrent >= monthInicio && dayCurrent >= dayInicio)
                                {
                                    gasto = new GastosEmployee();

                                    gasto.Name = employee.Name;
                                    gasto.Monto = salida.Monto;
                                    gasto.Fecha = salida.Fecha;

                                    gastos.Add(gasto);
                                }

                                continue;
                            }

                            else if (yearCurrent > yearInicio && yearCurrent <= yearFinal)
                            {
                                if (monthCurrent <= monthFinal && dayCurrent <= dayFinal)
                                {
                                    gasto = new GastosEmployee();

                                    gasto.Name = employee.Name;
                                    gasto.Monto = salida.Monto;
                                    gasto.Fecha = salida.Fecha;

                                    gastos.Add(gasto);
                                }

                            }


                        }
                        if ((monthCurrent >= monthInicio && monthCurrent <= monthFinal) && (yearCurrent == yearInicio && yearCurrent == yearFinal))
                        {
                            if (monthCurrent > monthInicio && monthCurrent < monthFinal)
                            {
                                gasto = new GastosEmployee();

                                gasto.Name = employee.Name;
                                gasto.Monto = salida.Monto;
                                gasto.Fecha = salida.Fecha;

                                gastos.Add(gasto);

                                continue;
                            }
                            else if (monthCurrent >= monthInicio && monthCurrent < monthFinal)
                            {
                                if (dayCurrent >= dayInicio)
                                {
                                    gasto = new GastosEmployee();

                                    gasto.Name = employee.Name;
                                    gasto.Monto = salida.Monto;
                                    gasto.Fecha = salida.Fecha;

                                    gastos.Add(gasto);
                                }

                                continue;
                            }
                            else if (monthCurrent > monthInicio && monthCurrent <= monthFinal)
                            {
                                if (dayCurrent <= dayFinal)
                                {
                                    gasto = new GastosEmployee();

                                    gasto.Name = employee.Name;
                                    gasto.Monto = salida.Monto;
                                    gasto.Fecha = salida.Fecha;

                                    gastos.Add(gasto);
                                }

                            }

                        }
                        if ((dayCurrent >= dayInicio && dayCurrent <= dayFinal) && (monthCurrent == monthInicio && monthCurrent == monthFinal) && (yearCurrent == yearInicio && yearCurrent == yearFinal))
                        {
                            gasto = new GastosEmployee();

                            gasto.Name = employee.Name;
                            gasto.Monto = salida.Monto;
                            gasto.Fecha = salida.Fecha;

                            gastos.Add(gasto);

                            continue;
                        }
                    }

                }

            }

            return gastos;

        }


        public double GetTotalGastos()
        {
            double total = 0;

            string sql = "SELECT * FROM Salida";
            List<Salida> salidas = con.Query<Salida>(sql);

            foreach (Salida salida in salidas)
            {
                total += Convert.ToDouble(salida.Monto);
            }

            return total;
        }

        public List<string> getIdEmployee()
        {
            List<string> idLista = new List<string>();

            foreach (Employee employee in GetEmployees())
            {
                idLista.Add(employee.Id.ToString());
            }

            return idLista;
        }

    }
}