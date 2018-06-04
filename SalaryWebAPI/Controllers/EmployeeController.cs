using SalaryModel.Models;
using SalaryWebAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace SalaryWebAPI.Controllers
{
    public class EmployeeController : ApiController
    {

        public EmployeeController()
        {

        }

        public EmployeeController(List<Employee> employees) : this()
        {
            using (var employeeContext = new EmployeeContext())
            {
                employeeContext.Employees.RemoveRange(employeeContext.Employees);
                employeeContext.SaveChanges();

                employeeContext.Employees.AddRange(employees);
                employeeContext.SaveChanges();
            }
        }

        // GET api/<controller>
        public IHttpActionResult Get()
        {
            using (var employeeContext = new EmployeeContext())
            {
                return Ok(employeeContext.Employees.ToList());
            }
        }

        // GET api/<controller>/5
        public async Task<IHttpActionResult> Get(int id)
        {
            using (var employeeContext = new EmployeeContext())
            {
                var emp = await employeeContext.Employees.FindAsync(id);
                if (emp != null)
                    return Ok(emp);
                else
                    return NotFound();
            }
        }

        public void Delete(int id)
        {
            using (var employeeContext = new EmployeeContext())
            {
                var emp = employeeContext.Employees.Find(id);
                if (emp != null)
                {
                    employeeContext.Employees.Remove(emp);
                    employeeContext.SaveChanges();
                }
            }
        }

        // POST api/<controller>
        public async Task<IHttpActionResult> Post([FromBody]Employee employee)
        {
            using (var employeeContext = new EmployeeContext())
            {
                employeeContext.Employees.Add(employee);
                try
                {
                    await employeeContext.SaveChangesAsync();
                    return Ok(employee.Id);
                }
                catch (System.Exception e)
                {
                    System.Console.WriteLine(e.ToString());
                    return Ok(-1);
                }
            }
        }
    }
}