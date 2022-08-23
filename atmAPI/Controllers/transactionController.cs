using atmAPI.Data;
using atmAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Data;
using System.Net.NetworkInformation;

namespace atmAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class transactionController : ControllerBase
    {
        ApplicationDbContext _dbContext;

        public transactionController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //get method
        [HttpGet]

        public async Task<ActionResult<IEnumerable<transaction>>> GetTransactions()
        {
            if (_dbContext.transactions == null)
            {

                return NotFound();

            }
            return await _dbContext.transactions.ToListAsync();

        }

        //get method by Id
        [HttpGet("{id}")]
        
        public async Task<ActionResult<transaction>> Show(int id)
        {


            if (_dbContext.transactions == null)
            {

                return NotFound();
            }

            var transactions = await _dbContext.transactions.FindAsync(id);

            if (transactions == null) return NotFound();


            return transactions;
        }








        //post method
        [HttpPost]
       

        public async Task<ActionResult<transaction>> PostTransaction(transaction transaction)
        {
            

            _dbContext.transactions.Add(transaction);

            await _dbContext.SaveChangesAsync();


            return CreatedAtAction(nameof(GetTransactions), new { id = transaction.transaction_id }, transaction);
        }




        //[HttpPost]


        //public async Task<ActionResult<transaction>> createTrans(string type , int amount, account account)
        //{

        //    transaction transaction = new transaction();
        //    transaction.amount = amount;
        //    transaction.transaction_type = type;
        //    transaction.transaction_date= DateTime.Now;
        //    transaction.account = account;




        //    _dbContext.transactions.Add(transaction);

        //    await _dbContext.SaveChangesAsync();


        //    return CreatedAtAction(nameof(GetTransactions), new { id = transaction.transaction_id }, transaction);
        //}






        //put method
        //[HttpPut("{id}")]


        //public async Task<IActionResult> PutTransaction(int id, transaction transaction)
        //{

        //    if (id != transaction.transaction_id)
        //    {

        //        return BadRequest();

        //    }



        //    _dbContext.Entry(transaction).State = EntityState.Modified;


        //    try
        //    {

        //        await _dbContext.SaveChangesAsync();


        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {


        //        if (!transactionExist(id))
        //        {


        //            return NotFound();

        //        }

        //        else
        //        {
        //            throw;

        //        }

        //    }



        //    return NoContent();

        //}
        //private bool transactionExist(long id)
        //{

        //    return (_dbContext.transactions?.Any(e => e.transaction_id == id)).GetValueOrDefault();
        //}




        //Delete  method

        [HttpDelete("{id}")]

        public string Delete(int id)
        {

            // get account me ket id
            var foundTransaction = _dbContext.transactions.FirstOrDefault(x => x.transaction_id == id);

            if (foundTransaction is null) return "No transaction found with this id!";

            _dbContext.transactions.Remove(foundTransaction);

            bool isDeleted = _dbContext.SaveChanges() > 0;
            if (isDeleted)
            {

                return "Transaction deleted successfully";
            }

            return "Error";
        }




    }
}
