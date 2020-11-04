using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeApi.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }

        public string EmployeeName { get; set; }

        public int Departament { get; set; }

        public string DateOfJoining { get; set; }

        public string PhotoFileName { get; set; }
    }
}