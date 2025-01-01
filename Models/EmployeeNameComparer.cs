namespace PDF.Models
{
    public class EmployeeNameComparer : IComparer<Employee>
    {
        public int Compare(Employee? x, Employee? y)
        {
            return string.Compare(x.Name, y.Name);
        }
    }
}
