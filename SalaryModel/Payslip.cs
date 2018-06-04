using SalaryContracts;

namespace SalaryModel.Models
{
    public class Payslip : IPayslip
    {
        public double GrossIncome { get; set; }

        public double IncomeTax { get; set; }

        public double NetIncome { get; set; }

        public double SuperAmount { get; set; }

    }
}