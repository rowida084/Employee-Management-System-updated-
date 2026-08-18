using ConsoleApp4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    internal class EmployeeEventArgs : EventArgs
    {
        public Employee Employee { get; }
      public EmployeeEventArgs(Employee employee)
        {
            this.Employee = employee;
        }
    }
}
