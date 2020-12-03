using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUDSample
{
    public interface ISQLite
    {
        SQLiteConnection GetConnectionWithCreateDatabase();

        bool SaveEmployee(Employee employee);

        List<Employee> GetEmployees();

        bool UpdateEmployee(Employee employee);
        void DeleteEmployee(int Id);

        bool SaveSalida(Salida salida);

        List<Salida> GetSalidas();

        bool UpdateSalida(Salida salida);
        void DeleteSalida(int Ids);

        List<string> getIdEmployee();

        List<GastosEmployee> GetGastosEmployee();
        List<GastosEmployee> FilterGastosEmployee(string fechaInicio, string fechaFinal);

        double GetTotalGastos();

    }
}
