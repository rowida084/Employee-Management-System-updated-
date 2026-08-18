using ConsoleApp4.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4.Services
{
    internal class Company
    {
        private Queue<Employee> onboarding=new Queue<Employee>();
        private Stack<string> actionHistory=new Stack<string>();
        public List<Employee> activeEmployees=new List<Employee>();
        public Dictionary<int, Department> departments=new Dictionary<int, Department>();
        private HashSet<string> uniqueSkills=new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        public event EventHandler<EmployeeEventArgs> EmployeeOnboarding;
        public event EventHandler<EmployeeEventArgs> EmployeePromoted;
        public Results<Employee> employeeIsFound(int id )
        {
            foreach (Employee emp in activeEmployees)
            {
                if (emp.Id == id)
                {
                    return new Results<Employee>
                    {
                        success = true,
                        message = "Employee Is Found!",
                        data = emp
                    };
                }
            }
             foreach (Employee emp in onboarding)
    {
        if (emp.Id == id)

                    return new Results<Employee>
                    {
                        success = true,
                        message = "Employee Is Found!",
                        data = emp
                    };
            }


            return new Results<Employee>
            {
                success = false,
                message = "Employee Is Not Found!",
                data = null
            };
        }

        private bool _departmentNameIsFound(string departmentName)
        {

            foreach (KeyValuePair<int, Department> department in departments)
            {
                if (department.Value.name.Equals(departmentName,StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        public bool depatrmentIDIsFound(int departmentID)
        {
            return departments.ContainsKey(departmentID);
        }
        public Results <Employee> addOnboardingEmployee(Employee newEmp)
        {
            if(newEmp == null)
            {
                return new Results<Employee>
                {
                    success = false,
                    message = "This Employee Is Empty!",
                    data = null
                };
            }
            Results<Employee> result = employeeIsFound(newEmp.Id);
            if (result.success)
                return new Results<Employee>
                {
                    success = false,
                    message = "This employee is already exist!",
                    data =null
                };

            onboarding.Enqueue(newEmp);
            actionHistory.Push($"Employee {newEmp.name} with ID {newEmp.Id} added to onboarding.");

            return new Results<Employee>
            {
                success = true,
                message = "Employee added successfully!",
                data = newEmp
            };
        }

        public Results<Employee> addActiveEmployee()
        {
            if (onboarding.Count > 0)
            {
                Employee employee = onboarding.Dequeue();
                activeEmployees.Add(employee);
                actionHistory.Push($"Employee {employee.name} with ID {employee.Id} added to active employees.");
                EmployeeOnboarding?.Invoke(this, new EmployeeEventArgs(employee));
                return new Results<Employee>
                {
                    success = true,
                    message = "Employee added successfully!",
                    data = employee
                };
            }

            return new Results<Employee>
            {
                success = false,
                message= "No employees in onboarding queue!",
                data = null
            };

        }

        public Results<Department> addDepartment(int ID, string name)
        {
            if(ID<=0)
            {
                return new Results<Department>
                {
                    success = false,
                    message = "Department ID can't Be Less Than Or Equal 0!",
                    data = null
                };
            }

            if (depatrmentIDIsFound(ID))
            {
                return new Results<Department>
                {
                    success = false,
                    message = "This Department ID Is Already Exist!",
                    data = null
                };
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                return new Results<Department>
                {
                    success = false,
                    message = "This Is Invalide Name!",
                    data = null
                };
            }

            if(_departmentNameIsFound(name))
            {
                return new Results<Department>
                {
                    success = false,
                    message = "This Department Name Is Already Exist!",
                    data = null
                };
            }

                Department department = new Department(name, ID);
                departments[ID]=department;
            actionHistory.Push($"Department {department.name} with ID: {department.Id} was added");
                return new Results<Department>
                {
                    success = true,
                    message = "The New Department Added Successfully!",
                    data =department
                };
         
        }

        public Results<Employee> addSkill(Employee emp)
        {
            if(emp==null)
            {
                return new Results<Employee>
                {
                    success = false,
                    message = "This Is An Empty Employee",
                    data = emp
                };
            }

            if(string.IsNullOrWhiteSpace(emp.uniqueSkill))
            {
                return new Results<Employee>
                {
                    success = false,
                    message = "Unique Skill For This Employee Is Empty!",
                    data = null
                };
            }
            uniqueSkills.Add(emp.uniqueSkill);
            return new Results<Employee>
            {
                success = true,
                message = "Unique Skill For This Employee Added Successfully!",
                data = emp
            };
        }


        public Employee searchEmployeeByName(string name)
        {
            foreach(Employee employee in activeEmployees)
            {
                if(employee.name.Equals(name,StringComparison.OrdinalIgnoreCase))
                {
                    return (employee);
                }
            }
            return null;
        }

        private int _getDepartmentIDByName(string departmentName)
        {
            
            foreach(KeyValuePair<int,Department>item in departments)
            {
                if(item.Value.name.Equals(departmentName,StringComparison.OrdinalIgnoreCase))
                {
                    return  item.Key;
                }
            }
            return -1;
        }

        public Results<List<Employee>> GetEmployeesByDepartment(string department)
        {
            int departmentID = _getDepartmentIDByName(department);
            if (departmentID == -1)
                return new Results<List<Employee>>()
                {
                    success = false,
                    message = "This Department Is Not Found!",
                    data = null
                };

            List<Employee> employees = new List<Employee>();
            if (_hasEmployee(department))
            {
                foreach (Employee employee in activeEmployees)
                {
                    if (employee.departmentID == departmentID)
                    {
                        employees.Add(employee);
                    }
                }
                return new Results<List<Employee>>()
                {
                    success = true,
                    message = "Emplotees Info : ",
                    data = employees
                };

            }
            return new Results<List<Employee>>()
            {
                success = false,
                message = "This Department Doesn't Have Employees! ",
                data = null
            };
            

        }

        public double calcSalaryAverage()
        {
            double sum = 0;
            foreach(Employee employee in activeEmployees)
            {
                sum += employee.salary;
            }
            if(activeEmployees.Count ==0)
            {
                return 0;
            }
            return sum / activeEmployees.Count;
        }

        private  Dictionary<int,int> _getNumberOfEmployeeInEachDepartment()
        {
            Dictionary<int, int> numOfEmployees=new Dictionary<int, int>();
            foreach(Employee employee in activeEmployees)
            {
                if (numOfEmployees.ContainsKey(employee.departmentID))
                {
                    numOfEmployees[employee.departmentID]++;
                }
                else
                {
                    numOfEmployees[employee.departmentID] = 1;
                }
            }
            return numOfEmployees;
        }

        private bool _hasEmployee(string departmentName)
        {
            int departmentID=_getDepartmentIDByName(departmentName);
            foreach (Employee employee in activeEmployees)
            {
               if(employee.departmentID==departmentID)
                {
                    return true;
                }
            }
            return false;
        }

        public void numOfEmployeesInEachDepartmentReport()
        {
            Dictionary<int, int> numOfEmployees=_getNumberOfEmployeeInEachDepartment();
            foreach(KeyValuePair<int,Department> item in departments)
            {
                if (numOfEmployees.ContainsKey(item.Key))
                {
                    Console.WriteLine($"{item.Value.name}: {numOfEmployees[item.Key]}");
                }
                else
                {
                    Console.WriteLine($"{item.Value.name}: 0");
                }
            }
        }

        public void printActionHistory()
        {
            if(actionHistory.Count == 0)
            {
                Console.WriteLine("No Actions In History");
                return;
            }

            foreach(string s in actionHistory)
            {
                Console.WriteLine(s);
            }
        }
        public void printUniqueSkills()
        {
            foreach(string item in  uniqueSkills)
            {
                Console.WriteLine(item);
            }
        }

        private int _getIndexOfEmployee(Employee emp)
        {
            return activeEmployees.IndexOf(emp);
        }

        private Results< Employee> _searchInActiveEmployee(int id)
        {
            foreach (Employee emp in activeEmployees)
            {
                if(emp is Manager&&emp.Id ==id)
                {
                    return new Results<Employee>
                    {
                        success = false,
                        message = "This Employee Is Already Manager!",
                        data = null
                    };
                }

                if (emp.Id == id)
                {
                    return new Results<Employee>
                    {
                        success = true,
                        message = "Employee Exists In Active Employees List! ",
                        data = emp
                    };
                }
            }
            return new Results<Employee>
            {
                success = false,
                message = "Employee Is Not Found In Active Employees List!",
                data = null
            };
        }
        public Results<Manager> promoteToManager(int id)
        {
             Results<Employee>result = _searchInActiveEmployee(id);
          
            if(result.success)
            {
                actionHistory.Push(($"Employee {result.data.name} with ID {result.data.Id} promoted to manager."));

              Manager newManager = new Manager(result.data.name, result.data.Id, result.data.departmentID
                , result.data.salary, result.data.uniqueSkill);

                activeEmployees[_getIndexOfEmployee(result.data)] = newManager;

                EmployeePromoted?.Invoke(this, new EmployeeEventArgs(newManager));

                return new Results<Manager>
                {
                    success = true,
                    message = "Manager Added Successfully!",
                    data =newManager
                };
            }
            return new Results<Manager>
            {
                success = false,
                message = "This Employee Is Not Exist To Be Promoted!",
                data = null
            };
        }

        public void printAllManagers()
        {
            bool found = false;
        
                foreach (Employee employee in activeEmployees)
                {
                    if (employee is Manager)
                    {
                    found = true;
                    Console.WriteLine("Manager Information: ");
                        employee.Print();
                    }
                }

            if (!found)
            {
                Console.WriteLine("This Company Doesn't have Managers!");
                Console.WriteLine();
            }
            
        }

        public Results<Manager> isManager(int ID)
         {
            if(ID<=0)
            {
                return new Results<Manager>
                {
                    success = false,
                    message = "This is invalide ID!",
                    data = null
                };
            }
            foreach(Employee employee in activeEmployees)
            {
                if (employee is Manager && employee.Id == ID)
                {
                    return new Results<Manager>
                    {
                        success = true,
                        message = "Manager Is Found.",
                        data = (Manager)employee
                    };
                }
            }
            return new Results<Manager>
            {
                success = false,
                message = "This Manager Isn't Exist!",
                data = null
            };
         }

        public bool isEmployeeInManagerTeam(Manager manager,int employeeID)
        {
            foreach(Employee employee in manager.teamMembers)
            {
                if(employee.Id ==employeeID)
                    { 
                    return true;
                }
            }
            return false;
        }
        public Results<Employee> assignEmployeeToManager(int managerID,int employeeID)
        {
            if(managerID<=0)
            {
                return new Results<Employee>
                {
                    success = false,
                    message = "Manager ID Can't Be Less Than Or Equal 0!",
                    data = null
                };
            }

            if(employeeID<=0)
            {
                return new Results<Employee>
                {
                    success = false,
                    message = "Employee ID Can't Be Less Than Or Equal 0!",
                    data = null
                };
            }

           Results<Employee> empResult=_searchInActiveEmployee(employeeID);
            if(!empResult.success)
            {
                return new Results<Employee>
                {
                    success = false,
                    message = empResult.message,
                    data = empResult.data
                };
            }

            else if(empResult.success&&empResult.data is Manager)
            {
                return new Results<Employee>
                {
                    success = false,
                    message = "A Manager cannot be assigned to another Manager's team.",
                    data = null
                };
            }
                Results<Manager> managerResult = isManager(managerID);
            if(!managerResult.success)
            {
                return new Results<Employee>
                {
                    success = false,
                    message = managerResult.message,
                    data = null
                };
            }

            if(isEmployeeInManagerTeam(managerResult.data,employeeID))
            {
                return new Results<Employee>
                {
                    success = false,
                    message = $"This Employee Is Already Exist In The Team Of Manager With Id {managerResult.data.Id}",
                    data = null
                };
            }

            managerResult.data.teamMembers.Add(empResult.data);
            return new Results<Employee>
            {
                success = true,
                message = $"Employee With ID: {employeeID} Assigned To Manager With ID {managerID} ",
                data = empResult.data
            };
        }

        public void printManagerTeam(int managerID)
        {
            Results<Manager> result = isManager(managerID);
            if (result.success)
            {
                if (result.data.teamMembers.Count > 0)
                {
                    foreach (Employee emp in result.data.teamMembers)
                    {
                        Console.WriteLine("Employee Info: ");
                        emp.Print();
                    }
                }

            }
            else
            {
                Console.WriteLine(result.message);
            }
           

        }

        public List<Employee> employeeFilter(EmployeeFilter filter)
        {
            List<Employee> result = new List<Employee>();
            foreach(Employee emp in activeEmployees)
            {
                if(filter(emp))
                {
                    result.Add(emp);
                }
            }
            return result;
        }

        public Results<t> genericSearch<t>(int id,IEnumerable <t> collection) where t:IHasId 
        {
            foreach(var result in collection)
            {
                if(result.Id == id)
                {
                    return new Results<t>
                    {
                        success = true,
                        message = $"{result.GetType()} Is Found !",
                        data = result
                    };
                }
            }

            return new Results<t>
            {
                success = false,
                message = "This ID Is Not Found!",
                data = default(t)
            };

        }  
         
    }
}
