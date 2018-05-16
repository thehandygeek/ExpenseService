using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseService.Models
{
    public class Expense
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public Decimal Amount { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public Guid ReferenceId { get; set; }
        [Required]
        public RecieptImage Reciept { get; set; }
        [Required]
        public string CompanyId { get; set; }
        [NotMapped]
        public String ReferenceIdString
        {
            get
            {
                return ReferenceId.ToString("N");
            }
        }

        public Expense()
        {
            this.Id = Guid.NewGuid();
            this.ReferenceId = Guid.NewGuid();
        }

        public static Guid ConvertReferenceIdString(String referenceIdString)
        {
            return new Guid(referenceIdString);
        }
    }
}