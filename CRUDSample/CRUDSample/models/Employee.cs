using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUDSample
{
    public class Employee
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        [MaxLength(256)]
        public string Name { get; set; }
        [MaxLength(256)]
        public string Dui{ get; set; }
        [MaxLength(256)]
        public string Nit { get; set; }
    }
    public class Salida
    {
        [PrimaryKey, AutoIncrement]
        public int Ids { get; set; }
        [MaxLength(256)]
        public string Des { get; set; }
        [MaxLength(256)]
        public string Fecha { get; set; }
        [MaxLength(256)]
        public double Monto { get; set; }
        [Indexed]
        public int Id { get; set; }
    }
    public class GastosEmployee
    {
        public string Name { get; set; }
        public double Monto { get; set; }
        public string Fecha { get; set; }
    }

}
