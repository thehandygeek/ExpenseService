using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Globalization;

using ExpenseService.DAL;
using ExpenseService.Models;
using ExpenseService.Utilities;
using System.Data.Entity;

namespace ExpenseService.Controllers
{
    public class ExpenseController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        [SessionValid]
        [RequireHttps]
        public IHttpActionResult Get()
        {
            IQueryable<Expense> expenseQuery;
            var expenseId = this.FetchExpenseId();
            if (expenseId == null)
            {
                expenseQuery = from expense in db.Expenses select expense;
            }
            else
            {
                var referenceId = Expense.ConvertReferenceIdString(expenseId);
                expenseQuery = from expense in db.Expenses where expense.ReferenceId == referenceId select expense;
            }
            List<ExpenseResponse> expenses = new List<ExpenseResponse>();
            foreach (var expense in expenseQuery)
            {
                expenses.Add(new ExpenseResponse(expense));
            }

            return Ok(expenses.ToArray());
        }

        [SessionValid]
        [RequireHttps]
        public IHttpActionResult Post([FromBody] ExpenseRequest request)
        {
            if (request == null || request.Amount == null || request.Date == null || request.Type == null)
            {
                return this.StatusCode(HttpStatusCode.BadRequest);
            }
            Decimal amount;
            if (!Decimal.TryParse(request.Amount, out amount))
            {
                return this.StatusCode((HttpStatusCode)422);
            }
            var newExpense = new Expense();
            newExpense.Type = request.Type;
            newExpense.Amount = amount;
            try
            {
                newExpense.Date = DateTime.ParseExact(request.Date, "d", CultureInfo.InvariantCulture);
                db.Expenses.Add(newExpense);
                db.SaveChanges();
            }
            catch
            {
                return this.StatusCode((HttpStatusCode)422);
            }

            return Ok(new { expense_id = newExpense.ReferenceIdString });
        }

        [SessionValid]
        [RequireHttps]
        public IHttpActionResult Delete()
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
            if (foundExpense == null)
            {
                return this.StatusCode((HttpStatusCode)422);
            }
            if (foundExpense.Reciept != null)
            {
                db.Reciepts.Remove(foundExpense.Reciept);
            }
            db.Expenses.Remove(foundExpense);
            db.SaveChanges();

            return Ok();
        }
    }
}
