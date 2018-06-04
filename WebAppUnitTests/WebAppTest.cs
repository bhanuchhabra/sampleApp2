using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SalaryContracts;
using SalaryWebApp.Controllers;
using SalaryWebAPI.Controllers;
using SalaryModel.Models;
using System.Web.Http.Results;

namespace WebAppUnitTests
{
    [TestClass]
    public class WebAppTest
    {

        [TestMethod]
        public void PositiveTest_GetEmployee_EmployeeIsCreated()
        {
            var employeeControllerSUT = new EmployeeController(new List<Employee>());

            var employee1 = new Employee()
            {
                FirstName = "FirstName",
                LastName = "LastName",
                AnualSalary = 1000,
                PaymentStartDate = new DateTime(2018, 03, 31),
                SuperRate = 9
            };

            var result = employeeControllerSUT.Post(employee1).Result;

            var get = employeeControllerSUT.Get();

            var fromGet = (get as OkNegotiatedContentResult<List<Employee>>).Content;

            Assert.AreEqual(1, fromGet.Count);
            Assert.IsTrue(fromGet[0].FirstName.Equals(employee1.FirstName));
        }

        [TestMethod]
        public void PositiveTest_GetEmployee_ByCorrectId_EmployeeFound()
        {
            var employeeControllerSUT = new EmployeeController(new List<Employee>());

            var employee1 = new Employee()
            {
                FirstName = "FirstName1",
                LastName = "LastName1",
                AnualSalary = 1000,
                PaymentStartDate = new DateTime(2018, 03, 31),
                SuperRate = 9
            };

            var employee2 = new Employee()
            {
                FirstName = "FirstName2",
                LastName = "LastName2",
                AnualSalary = 1000,
                PaymentStartDate = new DateTime(2018, 03, 31),
                SuperRate = 9
            };
            var response = employeeControllerSUT.Post(employee1).Result;
            response = employeeControllerSUT.Post(employee2).Result;

            var fromGet = (employeeControllerSUT.Get() as OkNegotiatedContentResult<List<Employee>>).Content;

            Assert.AreEqual(fromGet.Count, 2);
            Assert.IsTrue(fromGet[0].FirstName.Equals(employee1.FirstName));
            Assert.IsTrue(fromGet[1].FirstName.Equals(employee2.FirstName));
        }

        [TestMethod]
        public void NegativeTest_GetEmployee_ByWrongId_NoSuchEmployee()
        {
            var employeeControllerSUT = new EmployeeController(new List<Employee>());

            var employee1 = new Employee()
            {
                FirstName = "FirstName1",
                LastName = "LastName1",
                AnualSalary = 1000,
                PaymentStartDate = new DateTime(2018, 03, 31),
                SuperRate = 9
            };

            var employee2 = new Employee()
            {
                FirstName = "FirstName2",
                LastName = "LastName2",
                AnualSalary = 1000,
                PaymentStartDate = new DateTime(2018, 03, 31),
                SuperRate = 9
            };

            var id1 = employeeControllerSUT.Post(employee1 as Employee).Result;
            var id2 = employeeControllerSUT.Post(employee2 as Employee).Result;


            var fromGet = employeeControllerSUT.Get(3).Result;//(employeeControllerSUT.Get(3).Result as NotFoundResult);

            Assert.IsInstanceOfType(fromGet, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PositiveTest_GetEmployee_ByCorrectId_OfSecondEmployee_EmployeeFound()
        {
            var employeeControllerSUT = new EmployeeController(new List<Employee>());

            var employee1 = new Employee()
            {
                FirstName = "FirstName1",
                LastName = "LastName1",
                AnualSalary = 1000,
                PaymentStartDate = new DateTime(2018, 03, 31),
                SuperRate = 9
            };

            var employee2 = new Employee()
            {
                FirstName = "FirstName2",
                LastName = "LastName2",
                AnualSalary = 1000,
                PaymentStartDate = new DateTime(2018, 03, 31),
                SuperRate = 9
            };

            var id1 = employeeControllerSUT.Post(employee1 as Employee).Result;
            var id2 = (employeeControllerSUT.Post(employee2 as Employee).Result as OkNegotiatedContentResult<int>).Content;

            var fromGet = (employeeControllerSUT.Get(id2).Result as OkNegotiatedContentResult<Employee>).Content;

            Assert.AreEqual(employee2.FirstName, fromGet.FirstName);
            Assert.AreNotEqual(employee1.FirstName, fromGet.FirstName);

        }


        [TestMethod]
        public void NegativeTest_DeleteSecondEmployee_GetById_OfSecondEmployee_EmployeeNotFound()
        {
            var employeeControllerSUT = new EmployeeController(new List<Employee>());

            var employee1 = new Employee()
            {
                FirstName = "FirstName1",
                LastName = "LastName1",
                AnualSalary = 1000,
                PaymentStartDate = new DateTime(2018, 03, 31),
                SuperRate = 9
            };

            var employee2 = new Employee()
            {
                FirstName = "FirstName2",
                LastName = "LastName2",
                AnualSalary = 1000,
                PaymentStartDate = new DateTime(2018, 03, 31),
                SuperRate = 9
            };

            var id1 = employeeControllerSUT.Post(employee1 as Employee).Result;
            var id2 = (employeeControllerSUT.Post(employee2 as Employee).Result as OkNegotiatedContentResult<int>).Content;

            employeeControllerSUT.Delete(id2);
            var fromGet = employeeControllerSUT.Get(id2).Result;

            Assert.IsInstanceOfType(fromGet, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PositiveTest_GetPayslip_ByCorrectId_CorrectTaxCalculation()
        {
            var employee1 = new Employee()
            {
                FirstName = "FirstName1",
                LastName = "LastName1",
                AnualSalary = 60050,
                PaymentStartDate = new DateTime(2018, 03, 31),
                SuperRate = 9
            };

            var employeeControllerSUT = new EmployeeController();
            var id = (employeeControllerSUT.Post(employee1).Result as OkNegotiatedContentResult<int>).Content;

            var SUT = new PayslipController();

            var getResponse = SUT.Get(id);
            var paySlip = (getResponse as OkNegotiatedContentResult<IPayslip>).Content;

            Assert.IsNotNull(paySlip);
            Assert.AreEqual(5004, paySlip.GrossIncome);
            Assert.AreEqual(922, paySlip.IncomeTax);
            Assert.AreEqual(4082, paySlip.NetIncome);
            Assert.AreEqual(450, paySlip.SuperAmount);
        }

        [TestMethod]
        public void NegativeTest_GetPayslip_ByWrongId_CorrectTaxCalculation()
        {

            var employee1 = new Employee()
            {
                FirstName = "FirstName1",
                LastName = "LastName1",
                AnualSalary = 60050,
                PaymentStartDate = new DateTime(2018, 03, 31),
                SuperRate = 9
            };

            var employeeControllerSUT = new EmployeeController();
            var id = (employeeControllerSUT.Post(employee1).Result as OkNegotiatedContentResult<int>).Content;

            var SUT = new PayslipController();

            var getResponse = SUT.Get(499999);
            Assert.IsInstanceOfType(getResponse, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PositiveTest_GetPayslip_ByCorrectId_CorrectTaxCalculation_2()
        {
            var employee1 = new Employee()
            {
                FirstName = "FirstName1",
                LastName = "LastName1",
                AnualSalary = 120000,
                PaymentStartDate = new DateTime(2018, 03, 31),
                SuperRate = 10
            };
            var employeeControllerSUT = new EmployeeController();
            var id = (employeeControllerSUT.Post(employee1).Result as OkNegotiatedContentResult<int>).Content;

            var SUT = new PayslipController();

            var getResponse = SUT.Get(id);
            var paySlip = (getResponse as OkNegotiatedContentResult<IPayslip>).Content;

            Assert.IsNotNull(paySlip);
            Assert.AreEqual(10000, paySlip.GrossIncome);
            Assert.AreEqual(2669, paySlip.IncomeTax);
            Assert.AreEqual(7331, paySlip.NetIncome);
            Assert.AreEqual(1000, paySlip.SuperAmount);
        }

        [TestMethod]
        public void PositiveTest_GetPayslip_ByCorrectId_CorrectTaxCalculation_3()
        {
            var employee1 = new Employee()
            {
                FirstName = "FirstName1",
                LastName = "LastName1",
                AnualSalary = 20000,
                PaymentStartDate = new DateTime(2018, 03, 31),
                SuperRate = 12
            };

            var employeeControllerSUT = new EmployeeController();
            var id = (employeeControllerSUT.Post(employee1).Result as OkNegotiatedContentResult<int>).Content;

            var SUT = new PayslipController();

            var getResponse = SUT.Get(id);

            var paySlip = (getResponse as OkNegotiatedContentResult<IPayslip>).Content;

            Assert.IsNotNull(paySlip);
            Assert.AreEqual(1667, paySlip.GrossIncome);
            Assert.AreEqual(29, paySlip.IncomeTax);
            Assert.AreEqual(1638, paySlip.NetIncome);
            Assert.AreEqual(200, paySlip.SuperAmount);
        }

        [TestMethod]
        public void PositiveTest_GetPayslip_ByCorrectId_CorrectTaxCalculation_5()
        {
            var employee1 = new Employee()
            {
                FirstName = "FirstName1",
                LastName = "LastName1",
                AnualSalary = 9000,
                PaymentStartDate = new DateTime(2018, 03, 31),
                SuperRate = 0
            };

            var employeeControllerSUT = new EmployeeController();
            var id = (employeeControllerSUT.Post(employee1).Result as OkNegotiatedContentResult<int>).Content;

            var SUT = new PayslipController();

            var getResponse = SUT.Get(id);
            var paySlip = (getResponse as OkNegotiatedContentResult<IPayslip>).Content;

            Assert.IsNotNull(paySlip);
            Assert.AreEqual(750, paySlip.GrossIncome);
            Assert.AreEqual(0, paySlip.IncomeTax);
            Assert.AreEqual(750, paySlip.NetIncome);
            Assert.AreEqual(0, paySlip.SuperAmount);
        }


        [TestMethod]
        public void PositiveTest_GetPayslip_ByCorrectId_CorrectTaxCalculation_4()
        {
            var employee1 = new Employee()
            {
                FirstName = "FirstName1",
                LastName = "LastName1",
                AnualSalary = 200000,
                PaymentStartDate = new DateTime(2018, 03, 31),
                SuperRate = 11
            };

            var employeeControllerSUT = new EmployeeController();
            var id = (employeeControllerSUT.Post(employee1).Result as OkNegotiatedContentResult<int>).Content;

            var SUT = new PayslipController();

            var getResponse = SUT.Get(id);
            var paySlip = (getResponse as OkNegotiatedContentResult<IPayslip>).Content;

            Assert.IsNotNull(paySlip);
            Assert.AreEqual(16667, paySlip.GrossIncome);
            Assert.AreEqual(5269, paySlip.IncomeTax);
            Assert.AreEqual(11398, paySlip.NetIncome);
            Assert.AreEqual(1833, paySlip.SuperAmount);
        }

        [TestMethod]
        public void PositiveTest_GetPayslip_WithNoId_APIReferenceStringReturned()
        {
            var SUT = new PayslipController();
            var getResponse = SUT.Get();

            Assert.AreEqual("user api/Payslip/id to get payslip of Employee", (getResponse as OkNegotiatedContentResult<string>).Content);
        }

    }
}
