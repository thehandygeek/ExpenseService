using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using ExpenseService.Models;
using ExpenseService.DAL;
using ExpenseService.Utilities;

namespace ExpenseService.Controllers
{
    public class UserController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        [SessionValid]
        public IHttpActionResult Post([FromBody] UserRequest request)
        {
            var newUser = new User(request);
            db.Users.Add(newUser);
            db.SaveChanges();

            return Ok();
        }
    }
}
