using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ExpenseService.Models
{
    public class Session
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public DateTime Expiry { get; set; }
        [Required]
        public string SessionId { get; set; }
        [Required]
        public UserType UserType { get; set; }

        public Session()
        {
            this.Id = Guid.NewGuid();
        }

        public Session(UserType userType)
        {
            this.Id = Guid.NewGuid();
            this.Expiry = DateTime.UtcNow.AddHours(1);
            this.SessionId = Guid.NewGuid().ToString("N");
            this.UserType = userType;
        }
    }
}