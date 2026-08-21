using ConsoleApp4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    public delegate bool EmployeeFilter(Employee employee);
    public delegate int EmployeeComparer(Employee emp1, Employee emp2);
}
