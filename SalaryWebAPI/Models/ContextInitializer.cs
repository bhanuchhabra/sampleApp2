using System.Data.Entity;

namespace SalaryWebAPI.Models
{
    public class ContextInitializer : DropCreateDatabaseAlways<EmployeeContext>
    {
        public ContextInitializer()
        {
        }

        protected override void Seed(EmployeeContext context)
        {
            base.Seed(context);
        }
    }
}