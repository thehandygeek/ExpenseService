using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.IO;

using ExpenseService.DAL;
using ExpenseService.Models;
using ExpenseService.Utilities;
using System.Net.Http.Headers;
using System.Data.Entity;

namespace ExpenseService.Controllers
{
    public class RecieptController : ApiController
    {
        protected DatabaseContext db = new DatabaseContext();

        [SessionValid]
        [RequireHttps]
        public IHttpActionResult Get()
        {
            var expenseId = this.FetchExpenseId();
            if (expenseId == null)
            {
                return this.StatusCode(HttpStatusCode.BadRequest);
            }
            var referenceId = Expense.ConvertReferenceIdString(expenseId);
            var foundExpense = db.Expenses
                               .Where(e => e.ReferenceId == referenceId)
                               .Include(e => e.Reciept)
                               .FirstOrDefault();
             if (foundExpense == null || foundExpense.Reciept == null)
            {
                return this.StatusCode((HttpStatusCode)422);
            }
 
            return new BinaryResult(foundExpense.Reciept.ImageData, "image/jpeg");
        }

        [SessionValid]
        [RequireHttps]
        public IHttpActionResult Post([FromBody] Byte[] imageData)
         {
            var expenseId = this.FetchExpenseId();
            if (expenseId == null || imageData == null)
            {
                return this.StatusCode(HttpStatusCode.BadRequest);
            }
            var referenceId = Expense.ConvertReferenceIdString(expenseId);
            var foundExpense = db.Expenses
                .Where(e => e.ReferenceId == referenceId)
                .FirstOrDefault();
            if (foundExpense == null)
            {
                return this.StatusCode((HttpStatusCode) 422);
            }

            var newReciept = new RecieptImage();
            newReciept.ImageData = imageData;
            db.Reciepts.Add(newReciept);
            foundExpense.Reciept = newReciept;
            db.SaveChanges();
            return Ok();
        }
    }
}
