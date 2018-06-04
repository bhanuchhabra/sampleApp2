using SalaryBuinessLayer;
using System.Web.Http;

namespace SalaryWebAPI.Controllers
{
    public class PayslipController : ApiController
    {
        private readonly int FINANCIAL_YEAR = 2017;
        private readonly TaxedPayslipGenerator taxedPayslipGenerator;

        public PayslipController()
        {
            taxedPayslipGenerator = new TaxedPayslipGenerator();
        }

        // GET api/<controller>
        public IHttpActionResult Get()
        {
            return Ok("user api/Payslip/id to get payslip of Employee");
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            using (var empContext = new EmployeeContext())
            {
                var employee = empContext.Employees.Find(id);
                if (employee != null)
                {
                    taxedPayslipGenerator.SetTaxStrategy(new TaxStrategy2017(FINANCIAL_YEAR));
                    return Ok(taxedPayslipGenerator.GeneratePayslip(employee));

                }
                else
                {
                    return NotFound();
                }
            }
        }
    }
}