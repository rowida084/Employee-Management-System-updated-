using ConsoleApp4.Models;
using ConsoleApp4.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{

    internal class Program
    {
        private enum enOption
        {
            enaddEmployeeOnboarding = 1, enaddDeparment = 3, enProcessNextEmployee = 2,
            enSearchEmpByID = 4, enSearchEmpByName = 5, enPrintEmps = 6, enCalcSalaryAvrage = 7,
            enEmpsReport = 8, enPrintActionHistory = 9, enPrintUniqueSkills = 10,enPromoteToManager=11,
            enPrintAllManagers=12,enAssinEmployeeToManager=13,enPrintManagerTeam=14,
            enFilterEmployee=15,enSearchByID=16,enSortEmployeesBySalary=17,enSortEmployeesByName=18,enExite = 0
        }

        private enum enFilterOption {enEmpByManagerID=1,enSalary=2,enEmpsByDepartment=3,enBack=0}

        private enum enSalaryFilterOption { enHighSalary=1,enLowSalary=2,enBack=0}

        private enum enSearchOption { enEmployee = 1, enDepartment=2}
        static private string  _readString (string message)
        {
            string s;
            do
            {
                Console.WriteLine(message);
                s = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(s))
                {
                    Console.WriteLine("Cann't Be Empty");
                }
            } while (string.IsNullOrWhiteSpace(s));

            return s.Trim();
        }

        static private int _readNewEmployeeID(Company company)
        {
            int id = _readID("Enter Employee ID: ");
            while (company.employeeIsFound(id).success)
            {
                Console.WriteLine("This ID is Already Exists!");
                id = _readID("Enter ID: ");
            }
            return id;
        }

        static private int _existingDepartmentID(Company company)
        {
            int id ;
            do
            {
                id = _readID("Enter Department ID: ");
                if (!company.depatrmentIDIsFound(id))
                {
                    Console.WriteLine("This Department Does Not Exist!");
                }
            } while (!company.depatrmentIDIsFound(id));

            return id;
        }

        static private Employee _readEmployee(Company company)
        {
            string name = _readString("Enter Name : ");

            int id = _readNewEmployeeID(company);

            int departmentID = _existingDepartmentID(company);

            string uniqueSkill = _readString("Enter Skill : ");

            double salary = _readSalary();

            Employee emp = new Employee(
                name,
                id,
                departmentID,
                salary,
                uniqueSkill
            );

            return emp;
        }
        static private int _readID(string message)
        {
            Console.Write(message);
            int id;

            while (!int.TryParse(Console.ReadLine(), out id)||id<=0)
            {
                Console.WriteLine("Invalid ID. Try Again.");
                Console.Write(message);
            }

            return id;
        }
        static void SeedData(Company company)
        {

            company.addDepartment(1, "HR");
            company.addDepartment(2, "IT");
            company.addDepartment(3, "Finance");
            company.addDepartment(4, "Marketing");
            company.addDepartment(5, "Sales");
            company.addDepartment(6, "Customer Support");


            Employee[] employees =
            {
        new Employee("Ahmed Hassan",     101, 2, 12000, "C#"),
        new Employee("Sara Ali",         102, 1,  8000, "Recruitment"),
        new Employee("Omar Mohamed",     103, 3, 11000, "Excel"),
        new Employee("Mona Adel",        104, 2, 15000, "ASP.NET"),
        new Employee("Youssef Samy",     105, 5,  9000, "Negotiation"),
        new Employee("Nour Ahmed",       106, 2, 13000, "SQL"),
        new Employee("Khaled Tarek",     107, 4, 10000, "Photoshop"),
        new Employee("Salma Mostafa",    108, 3, 14000, "Power BI"),
        new Employee("Hana Ibrahim",     109, 6,  7500, "Customer Service"),
        new Employee("Karim Magdy",      110, 2, 16000, "C#"),
        new Employee("Mariam Hany",      111, 4, 11500, "Content Writing"),
        new Employee("Amr Essam",        112, 5, 10500, "Sales"),
        new Employee("Aya Nabil",        113, 1,  8500, "Communication"),
        new Employee("Mahmoud Adel",     114, 2, 17000, "SQL"),
        new Employee("Reem Wael",        115, 6,  7800, "Problem Solving"),
        new Employee("Mostafa Ashraf",   116, 2, 14500, "ASP.NET"),
        new Employee("Laila Sherif",     117, 3, 13500, "Excel"),
        new Employee("Mohamed Gamal",    118, 2, 18000, "C#"),
        new Employee("Farah Khaled",     119, 4,  9800, "Photoshop"),
        new Employee("Ziad Hassan",      120, 5, 12500, "Negotiation")
    };


            foreach (Employee emp in employees)
            {
                company.addOnboardingEmployee(emp);
                company.addSkill(emp);
            }

            //for (int i = 0; i < 10; i++)
            //{
            //    company.addActiveEmployee();
            //}
        }

        static private void _employeeOnboardedHandler(object sender, EmployeeEventArgs e)
        {
            Console.WriteLine($"[Notification] Employee {e.Employee.name} with ID: {e.Employee.Id} has been onboarded.");
        }

        static private void _employeePromotedHandler(object sender, EmployeeEventArgs e)
        {
            Console.WriteLine($"[Notification] Employee {e.Employee.name} with ID: {e.Employee.Id} has been promoted to manager ");
        }
        static private void _mainMenuPerformance(enOption choice, Company company)
        {
            switch (choice)
            {
                case enOption.enaddEmployeeOnboarding:
                    {
                        Employee emp = _readEmployee(company);
                        Results<Employee> result =company.addOnboardingEmployee(emp);
                        if (result.success)
                        {
                            company.addSkill(result.data);                         
                        }
                         Console.WriteLine(result.message);
                        break;
                    }

                case enOption.enaddDeparment:
                    {
                        Results<Department> result=new Results<Department>();
                        do
                        {
                            Console.Write("Enter Department ID: ");
                            int id;
                            while (!int.TryParse(Console.ReadLine(), out id))
                            {
                                Console.WriteLine("Invalide ID");
                                Console.Write("Try Again: ");
                            }
                            Console.Write("Enter Department name: ");
                            string name = Console.ReadLine();
                            result = company.addDepartment(id, name);
                            Console.WriteLine(result.message);
                            Console.WriteLine("Try Again...");
                        } while (!result.success);
                        if(result.success)
                        {
                            Console.WriteLine("Department Added Successfully!");
                        }

                        Console.WriteLine();
                        break;
                    }

                case enOption.enProcessNextEmployee:
                    {
                        Results<Employee> result = company.addActiveEmployee();
                      
                        Console.WriteLine(result.message);
                        break;
                    }

                case enOption.enSearchEmpByID:
                    {
                        int id = _readID("Enter employee ID: ");
                        Results<Employee> result = company.employeeIsFound(id);
                        if (result.data != null)
                        {
                            result.data.Print();
                        }
                        else
                        {
                            Console.WriteLine(result.message);
                        }
                        Console.WriteLine();
                        break;
                    }

                case enOption.enSearchEmpByName:
                    {
                        Console.Write("Enter Employee Name : ");
                        string empName = Console.ReadLine();
                        Employee emp = company.searchEmployeeByName(empName);
                        if (emp != null)
                        {
                            emp.Print();
                        }
                        else
                        {
                            Console.WriteLine("This Employee Does Not Exist!");
                        }
                        Console.WriteLine();
                        break;
                    }

                case enOption.enPrintEmps:
                    {
                        Console.Write("Enter Department Name : ");
                        string departmentName = Console.ReadLine();
                        Results<List<Employee>> results = company.GetEmployeesByDepartment(departmentName);
                        if (results.success)
                        {
                            Console.WriteLine(results.message);
                            foreach (Employee emp in results.data)
                            {
                                emp.Print();
                            }
                        }
                        else
                        {
                            Console.WriteLine(results.message);
                        }
                     
                        break;
                    }

                case enOption.enCalcSalaryAvrage:
                    {
                        Console.WriteLine($"Salary Average : {company.calcSalaryAverage()}");
                        Console.WriteLine();
                        break;
                    }

                case enOption.enEmpsReport:
                    {
                        company.numOfEmployeesInEachDepartmentReport();
                        Console.WriteLine();
                        break;
                    }

                case enOption.enPrintActionHistory:
                    {
                        company.printActionHistory();
                        Console.WriteLine();
                        break;
                    }

                case enOption.enPrintUniqueSkills:
                    {
                        company.printUniqueSkills();
                        Console.WriteLine();
                        break;
                    }

                case enOption.enPromoteToManager:
                    {
                        int id = _readID("Enter Employee ID: ");
                        Results<Manager>result=company.promoteToManager(id);
                        Console.WriteLine(result.message);
                        if(result.success)
                        {
                            result.data.Print();
                        }
                        break;
                    }

                case enOption.enPrintAllManagers:
                    {
                        company.printAllManagers();
                        break;
                    }

                case enOption.enAssinEmployeeToManager:
                    {
                        int empID;
                        int managerID;
                        do
                        {
                            managerID = _readID("Enter Manager ID: ");
                            empID = _readID("Enter Employee ID: ");
                           
                            if(empID == managerID)
                            {
                                Console.WriteLine("Employee ID Cann't equal Manager ID");
                                Console.WriteLine("Try Again...");
                            }
                        } while (empID == managerID);
                   Results < Employee > result = company.assignEmployeeToManager(managerID, empID);
                        Console.WriteLine(result.message);
                        break;
                    }

                    case enOption.enPrintManagerTeam:
                    {
                        int managerID = _readID("Enter Manager ID: ");
                        company.printManagerTeam(managerID);
                        break;
                    }

                case enOption.enFilterEmployee:
                    {
                        _employeeFilterMenu(company);
                        break;
                    }

                case enOption.enSearchByID:
                    {
                        _searchMenu(company);
                        break;
                    }

                case enOption.enSortEmployeesBySalary:
                    {
                        company.bubbleSort((a,b)=>b.salary.CompareTo(a.salary));//decreasing order
                        company.printActiveEmployees();
                        break;
                    }

                case enOption.enSortEmployeesByName:
                    {
                        company.bubbleSort((a,b)=>a.name.CompareTo(b.name));
                        company.printActiveEmployees();
                        break;
                    }

                case enOption.enExite:
                    {

                        return;
                    }

                default:
                    Console.WriteLine("Invalid Choice");
                    break;
            }
        }

        //static private void _printEmps(List<Employee> employees)
        //{
        //    foreach(Employee emp in employees)
        //    {
        //        Console.WriteLine("Employee Info:");
        //        emp.Print();
        //        Console.WriteLine("=====================================");
        //    }
        //}

        static private double _readSalary()
        {
            double salary;
            Console.WriteLine("Enter Salary: ");
            while(!double.TryParse(Console.ReadLine(),out salary)||salary<=0)
            {
                Console.WriteLine("Invalid Salary.Try Again: ");  
            }
            return salary;
        }
        static private void _filterEmployeeMenuPerformance(enFilterOption choice,Company company)
        {
            switch(choice)
            {
               case enFilterOption.enEmpByManagerID:
                    {
                        int managerID = _readID("Enter Manager ID: ");
                        Results<Manager> result =company.isManager(managerID);

                        if (result.success)
                        {
                            List<Employee> emps = company.employeeFilter(employee => company.isEmployeeInManagerTeam(result.data, employee.Id));
                            if(emps.Count==0)
                            {
                                Console.WriteLine("This Manager Has No Team Members.");
                            }
                            else
                            {
                                company.printEmps(emps);
                            }
                               
                        }

                        else
                        {
                            Console.WriteLine(result.message);
                        }

                            break;
                    }

               case enFilterOption.enSalary:
                    {
                     _salaryFilterMenu(company); 
                            break;
                    }

               case enFilterOption.enEmpsByDepartment:
                    {
                        int departmentID = _readID("Enter Department ID: ");
                        if (company.depatrmentIDIsFound(departmentID))
                        {
                            List<Employee> emps = company.employeeFilter(employee => employee.departmentID == departmentID);
                            if (emps.Count == 0)
                            {
                                Console.WriteLine("No employees belong to this department.");
                            }
                            else
                            {
                                company.printEmps(emps);
                            }
                        }
                        else
                        {
                            Console.WriteLine("This Department Isn't Exist!");
                        }
                            break;
                    }

               case enFilterOption.enBack:
                    {
                        return;
                    }
                default:
                    {
                        Console.WriteLine("Invalid Choice");
                        break;
                    }
            }
        }

       static private void _salaryFilterPerformance(enSalaryFilterOption choice, Company company)
        {
            double salary;
            List<Employee> emps = new List<Employee>();
            switch (choice)
            { 
                case enSalaryFilterOption.enHighSalary:
                    {
                        salary = _readSalary();
                        emps = company.employeeFilter(employee => employee.salary >= salary);
                        break;
                    }

                case enSalaryFilterOption.enLowSalary:
                    {
                        salary = _readSalary();
                        emps = company.employeeFilter(employee => employee.salary < salary);
                        break;
                    }
                case enSalaryFilterOption.enBack:
                    {
                        return;
                    }
                default:
                    {
                        Console.WriteLine("Invalid Choice");
                        break;
                    }
            }
            if (emps.Count > 0)
            {
                company.printEmps(emps);
            }
            else
            {
                Console.WriteLine("No employees match this salary condition.");
            }
        }

       static private void _salaryFilterMenu(Company company)
        {
            short choice;
            do
            {
              
                Console.WriteLine("============= Salary Filter =============");
                Console.WriteLine("1. High Salary");
                Console.WriteLine("2. Low Salary");
                Console.WriteLine("0.Back");
                choice = _readChoice(2);
                _salaryFilterPerformance((enSalaryFilterOption)choice, company);
            } while (choice != 0);
        }
       static private void _employeeFilterMenu(Company company)
        {

            short choice;
            do
            {
                Console.WriteLine();
                Console.WriteLine("=============Employee Filter Menu=============");
                Console.WriteLine("1.Filter By Manager ID.");
                Console.WriteLine("2.Filter By Salary.");
                Console.WriteLine("3.Filter By Department.");
                Console.WriteLine("0.Back");
                choice = _readChoice(3);
                Console.WriteLine();
                _filterEmployeeMenuPerformance((enFilterOption)choice, company);
            } while (choice != 0);
        }
        
        static private void _searchMenuPerformance(enSearchOption choice,Company company)
        {
            switch(choice)
            {
                case enSearchOption.enEmployee:
                    {
                        int Id=_readID("Enter Employee ID: ");
                        Results<Employee> result = company.genericSearch<Employee>(Id, company.activeEmployees);
                      
                            Console.WriteLine(result.message);
                        if (result.success)
                        {
                            result.data.Print();
                        }
                            break;
                    }

                case enSearchOption.enDepartment:
                    {
                        int Id = _readID("Enter Department ID: ");
                        Results<Department> result = company.genericSearch<Department>(Id, company.departments.Values);
                        Console.WriteLine(result.message);
                        if (result.success)
                        {
                            result.data.print();
                        }
                        break;
                    }
            }
        }
        static private void _searchMenu(Company company)
        {
            short choice;
            do
            {
                Console.WriteLine();
                Console.WriteLine("=============Searching By ID Menu=============");
                Console.WriteLine("1.Search By Employee ID.");
                Console.WriteLine("2.Search By Department ID.");
                Console.WriteLine("0.Back");
                choice = _readChoice(2);
                Console.WriteLine();

                _searchMenuPerformance((enSearchOption)choice, company);
            } while (choice != 0);
        }
        static private short _readChoice(int maxOption)
        {
            Console.Write("Enter Your Choice: ");
            short choice;
         
            while (!short.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice >maxOption)
            {
                Console.WriteLine("Inavailable Option!");
                Console.Write("Try Again: ");
                
            }
            return choice;
        }

        static public void mainMenu(Company company)
        {
            short choice;
            do
            {
                Console.WriteLine("========== Company Management ==========");
                Console.WriteLine("01. Add Employee To Onboarding");
                Console.WriteLine("02. Process Next Employee");
                Console.WriteLine("03. Add Department");
                Console.WriteLine("04. Search Employee By ID");
                Console.WriteLine("05. Search Employee By Name");
                Console.WriteLine("06. Print Employees By Department");
                Console.WriteLine("07. Calculate Salary Average");
                Console.WriteLine("08. Employees Report By Department");
                Console.WriteLine("09. Print Action History");
                Console.WriteLine("10.Print Unique Skills");
                Console.WriteLine("11.Promote To Manager");
                Console.WriteLine("12.Print All Managers");
                Console.WriteLine("13.Assign Employee To Manager");
                Console.WriteLine("14.Print Manager Team");
                Console.WriteLine("15.Filter Employee");
                Console.WriteLine("16.Searching By ID");
                Console.WriteLine("17.Sort Employees By Salary (Decreasing Order)");
                Console.WriteLine("18.Sort Employees By Name (Increasing Order)");
                Console.WriteLine("0. Exit");
                Console.WriteLine();

                choice = _readChoice(18);

                Console.WriteLine();
                Console.WriteLine();
                _mainMenuPerformance((enOption)choice, company);
            } while (choice != 0);

        }
        static void Main(string[] args)
        {

            Company company = new Company();
            company.EmployeeOnboarding += _employeeOnboardedHandler;
            company.EmployeePromoted += _employeePromotedHandler;
            SeedData(company);
            mainMenu(company);
           
        }

    }
}
