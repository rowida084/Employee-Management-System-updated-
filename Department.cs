using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4.Models
{
    internal class Department : IHasId
    {
        public string name {  get; set; }
        public int Id { get; set; }

      public  Department(string name, int id)
        {
            this.name = name;
            this.Id = id;
        }

        public void print()
        { 
            Console.WriteLine($"Department ID: {Id}");
            Console.WriteLine($"Department Name: {name}");
        }
    }
}
