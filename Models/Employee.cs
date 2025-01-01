namespace PDF.Models
{
    public class Employee : IComparable<Employee>
    {
        public string Name { get; set; }
        public double Salary { get; set; }

        public int CompareTo(Employee other)
        {
            return this.Salary.CompareTo(other.Salary);
        }
    }
}
