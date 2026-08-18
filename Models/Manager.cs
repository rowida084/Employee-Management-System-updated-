using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4.Models
{
    internal class Manager :Employee
    {
        public  List<Employee> teamMembers {  get; set; }
       public Manager(string name,int ID,int deparementID,double salary,string uniqueSkill)
            :base(name,ID, deparementID, salary,uniqueSkill)
        {
         teamMembers = new List<Employee>(); 
        }
    }
}
