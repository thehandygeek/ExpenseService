using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

using ExpenseService.Utilities;

namespace ExpenseService.Models
{
    public enum UserType
    {
        Regular = 1,
        Admin
    }

    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public String PasswordHash { get; set; }
        [Required]
        public UserType Type { get; set; }

        public User()
        {
            this.Id = Guid.NewGuid();
        }

        public User(UserRequest request)
        {
            this.Id = Guid.NewGuid();
            this.Name = request.UserName;
            this.PasswordHash = PasswordUtility.Hash(request.Password);
            this.Type = request.UserType == "admin" ? UserType.Admin : UserType.Regular;
        }
    }
}