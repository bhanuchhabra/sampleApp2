using SalaryWebAPI.Models;
using System.Data.Entity;
using System.Web;
using System.Web.Http;

namespace SalaryWebAPI
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new ContextInitializer());
            using (var employeeContext = new EmployeeContext())
            {
                employeeContext.Database.Delete();
            }
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
