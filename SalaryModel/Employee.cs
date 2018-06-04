using Newtonsoft.Json;
using SalaryContracts;
using SalaryModel.Validators;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalaryModel.Models
{
    public class Employee: ITaxableEmployee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [JsonProperty(PropertyName = "FirstName")]
        public virtual string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [JsonProperty(PropertyName = "LastName")]
        public virtual string LastName { get; set; }

        [CustomPositiveValidator(ErrorMessage = "Salary should be positive")]
        [Required(ErrorMessage = "Anual Salary is required")]
        [JsonProperty(PropertyName = "AnualSalary")]
        public virtual int AnualSalary { get; set; }


        [Required(ErrorMessage = "Super rate is required")]
        [Range(0, 12, ErrorMessage = "Super rate shall be between 0 to 12")]
        [JsonProperty(PropertyName = "SuperRate")]
        public virtual int SuperRate { get; set; }

        [JsonProperty(PropertyName = "PaymentStartDate")]
        public virtual DateTime? PaymentStartDate { get; set; }
    }
}
