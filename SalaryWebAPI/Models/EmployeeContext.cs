using SalaryModel.Models;
using System.Data.Entity;

namespace SalaryWebAPI
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext() : base("SalaryApp")
        {
            
        }

        public virtual DbSet<Employee> Employees { get; set; }
    }
}
