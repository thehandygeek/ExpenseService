using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web;

using ExpenseService.Models;
using ExpenseService.DAL;
using ExpenseService.Utilities;

namespace ExpenseService.Controllers
{
    public class SessionController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        [RequireHttps]
        public IHttpActionResult Post([FromBody] SessionRequest request)
        {
            if (request == null || request.UserName == null || request.Password == null)
            {
                return this.StatusCode(HttpStatusCode.BadRequest);
            }
            var userQuery = from user in db.Users where user.Name == request.UserName select user;
            var foundUser = userQuery.FirstOrDefault();
            if (foundUser == null || !PasswordUtility.Verify(request.Password, foundUser.PasswordHash))
            {
                 return this.StatusCode(HttpStatusCode.Unauthorized);
            }
            var newSession = new Session(foundUser.Type);
            db.Sessions.Add(newSession);
            db.SaveChanges();
            CleanupExpiredSessions();

            return Ok(new { session_id = newSession.SessionId });
        }

        private void CleanupExpiredSessions()
        {
            var sessionQuery = from session in db.Sessions where session.Expiry < DateTime.UtcNow select session;
            db.Sessions.RemoveRange(sessionQuery);
        }
    }
}
