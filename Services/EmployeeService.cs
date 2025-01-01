using PDF.Models;

namespace PDF.Services
{
    public class EmployeeService
    {
        List<Employee> employees = new List<Employee>
        {
            new Employee { Name = "Alice", Salary = 50000 },
            new Employee { Name = "aob", Salary = 5000 },
            new Employee { Name = "dob", Salary = 601000 },
            new Employee { Name = "bob", Salary = 600010 },
            new Employee { Name = "Bob", Salary = 600002 },
            new Employee { Name = "cob", Salary = 600003 },
        };

        public List<Employee> getEmployee()
        {
            SortEmplyees();
            return employees;
        }

        public List<Employee> SortEmplyeesByName()
        {
            SortByName();
            return employees;
        }

        public void SortEmplyees()
        {
            employees.Sort();
        }

        public void SortByName()
        {
            EmployeeNameComparer employeeNameComparer = new EmployeeNameComparer();
            employees.Sort(employeeNameComparer);
        }
    }
}
