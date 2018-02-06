using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ExpenseService.Controllers
{
    public static class ApiControllerExtension
    {
        public static String FetchExpenseId(this ApiController controller)
        {
            String result = null;

            if (controller.Request.Headers.Contains("expense_id"))
            {
                result = controller.Request.Headers.GetValues("expense_id").FirstOrDefault();
            }
 
            return result;
        }
    }
}