using Newtonsoft.Json;
using SalaryContracts;
using SalaryModel.Models;
using SalaryWebApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

namespace SalaryWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly WebApiCallerService _webApiCallerService;

        public HomeController()
        {
            if (_webApiCallerService == null)
                _webApiCallerService = new WebApiCallerService();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ViewResult CreateEmployee()
        {
            return View();
        }

        public async Task<ViewResult> EmployeeList()
        {
            List<Employee> listOfEmployees = new List<Employee>();

            var result = await _webApiCallerService.WebApiCallerGet("api/Employee");
            listOfEmployees = JsonConvert.DeserializeObject<List<Employee>>(result);

            return View(listOfEmployees);
        }

        public async Task<ActionResult> GetPayslip(int id)
        {

            IPayslip payslip = null;

            var result = await _webApiCallerService.WebApiCallerGet(string.Format("api/Payslip/{0}", id));

            payslip = JsonConvert.DeserializeObject<Payslip>(result);
            if (payslip == null)
                return RedirectToAction("EmployeeList", "Home");

            return View(payslip);
        }

        public async Task<bool> SaveEmployee([FromBody]Employee emp)
        {
            return await _webApiCallerService.WebApiCallerPost<Employee>("api/Employee", emp);

        }
    }
}