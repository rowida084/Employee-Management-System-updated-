using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4.Models
{
    internal class Employee :IHasId
    {
        public string name {  get; set; }
        public int Id {  get; set; }
        public DateTime hireDate { get; set; }
        public int departmentID {  get; set; }
        public double salary {  get; set; }
        public string uniqueSkill { get; set; }

        public Employee(string name, int iD, int departmentID, double salary,string uniqueSkill)
        {
            this.name = name;
            this. Id = iD;
            this.hireDate = DateTime.Now;
            this.departmentID = departmentID;
            this.salary = salary;
            this.uniqueSkill = uniqueSkill;
        }
        public void Print()
        {
            //Console.WriteLine("Employee Information");
            Console.WriteLine($"Name          : {name}");
            Console.WriteLine($"ID            : {Id}");
            Console.WriteLine($"Hire Date     : {hireDate:dd/MM/yyyy}");
            Console.WriteLine($"Department ID : {departmentID}");
            Console.WriteLine($"Salary        : {salary}");
        }

    }
}
