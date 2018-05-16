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
    class ExpenseFilterInfo
    {
        public int Month;
        public int Year;
    }

    public class ExpenseController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        [SessionValid]
        [RequireHttps]
        public IHttpActionResult Get()
        {
            IQueryable<Expense> expenseQuery;
            var expenseId = this.FetchExpenseId();
            var monthFilterInfo = this.FetchExpenseFilter();
            if (expenseId != null)
            {
                var referenceId = Expense.ConvertReferenceIdString(expenseId);
                expenseQuery = db.Expenses
                    .Where(e => e.ReferenceId == referenceId);
            }
            else if (monthFilterInfo != null)
            {
                expenseQuery = db.Expenses
                    .Where(e => e.Date.Month == monthFilterInfo.Month && e.Date.Year == monthFilterInfo.Year)
                    .OrderBy(e => e.Date);
            }
            else
            {
                expenseQuery = db.Expenses
                   .OrderBy(e => e.Date);
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
            newExpense.CompanyId = request.CompanyId;
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

        private ExpenseFilterInfo FetchExpenseFilter()
        {
            ExpenseFilterInfo result = null;

            if (this.Request.Headers.Contains("month_filter"))
            {
                var stringFilter = this.Request.Headers.GetValues("month_filter").FirstOrDefault();
                var filterElements = stringFilter.Split('/');
                if (filterElements.Length == 2 && int.TryParse(filterElements[0], out int month) && int.TryParse(filterElements[1], out int year))
                {
                    result = new ExpenseFilterInfo() { Month = month, Year = year };
                }
            }

            return result;
        }
    }
}
