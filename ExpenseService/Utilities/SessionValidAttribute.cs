using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

using ExpenseService.DAL;

namespace ExpenseService.Utilities
{
    public class SessionValidAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var failure = true;
            var db = new DatabaseContext();
            var request = actionContext.Request;
            if (request.Headers.Contains("session_id"))
            {
                var sessionId = request.Headers.GetValues("session_id").FirstOrDefault();
                if (sessionId != null)
                {
                    var sessionQuery = from session in db.Sessions where session.SessionId == sessionId select session;
                    var foundSession = sessionQuery.FirstOrDefault();
                    if (foundSession != null && DateTime.UtcNow <= foundSession.Expiry)
                    {
                        failure = false;
                    }
                }
            }

            if (failure)
            {
                actionContext.Response = request.CreateErrorResponse(HttpStatusCode.Forbidden, "Access denied");
            }
        }
    }
}