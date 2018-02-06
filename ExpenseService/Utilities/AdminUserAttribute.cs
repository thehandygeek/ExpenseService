using System;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

using ExpenseService.DAL;
using ExpenseService.Models;

namespace ExpenseService.Utilities
{
    public class AdminUserAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var db = new DatabaseContext();
            var sessionId = actionContext.Request.Headers.GetValues("session_id").FirstOrDefault();
            if (sessionId != null)
            {
                var sessionQuery = from session in db.Sessions where session.SessionId == sessionId && session.UserType == UserType.Admin select session;
                var foundSession = sessionQuery.FirstOrDefault();
                if (foundSession == null || DateTime.UtcNow > foundSession.Expiry)
                {
                    throw new HttpException((int)HttpStatusCode.Forbidden, "Forbidden");
                }
            }
            else
            {
                throw new HttpException((int)HttpStatusCode.Forbidden, "Forbidden");
            }
        }
    }
}