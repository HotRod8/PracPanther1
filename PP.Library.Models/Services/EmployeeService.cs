using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PP_Library.Models;

namespace PP_Library.Services
{
    //Workers of each project
    public class EmployeeService
    {
        private static EmployeeService? instance;
        private static object daLock = new object();
        public static EmployeeService Current
        {
            get
            {
                //For thread safety
                lock (daLock)
                {
                    //Critical Part - OS issue if used w/o lock
                    if (instance == null)
                    {
                        instance = new EmployeeService();
                    }
                }

                return instance;
            }
        }

        private List<Employee> employees;
        private EmployeeService()
        {
            employees = new List<Employee>
            {
                new Employee {Id = 300, Name = "Rodana Smith", Rate = 30},
                new Employee {Id = 400, Name = "Britannia Faith", Rate = 20},
                new Employee {Id = 500, Name = "Ganondorf Dragonsword", Rate = 100}
            };
        }
        public List<Employee> Employees { get { return employees; } }
        public Employee? Get(int emplid)
        {
            return employees.FirstOrDefault(e => e.Id == emplid);
        }

        public List<Employee> Search(string query)
        {
            return employees.Where(s => s.Name.ToUpper().Contains(query?.ToUpper() ?? string.Empty)).ToList();
        }

        public void Add(Employee? Employee)
        {
            if (Employee != null) 
            {
                Employee.Id = LastId + 1;
                employees.Add(Employee);
            }
        }

        private int LastId
        {
            get
            {
                return Employees.Any() ? Employees.Select(c => c.Id).Max() : 0;
            }
        }

        public void Read()
        {
            foreach (var money in employees)
            { Console.WriteLine(money); }
            //employees.ForEach(Console.WriteLine);
        }
        public void Edit(Employee toUpdate)
        {
            var UpdateEmployee = Current.Get(toUpdate.Id);
            if (UpdateEmployee != null)
            {
                UpdateEmployee.Rate = toUpdate.Rate;
                UpdateEmployee.Name = toUpdate.Name;
            }
        }
        public void Delete(int id)
        {
            var employeeToRemove = Get(id);
            if (employeeToRemove != null) { employees.Remove(employeeToRemove); }
        }
        public void Delete(Employee c)
        {
            Delete(c.Id);
        }
    }
}
