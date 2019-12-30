using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ExpenseService.Models
{
    public class ReceiptImage
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public byte[] ImageData { get; set; }

        public ReceiptImage()
        {
            this.Id = Guid.NewGuid();
        }
    }
}