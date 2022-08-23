using atmAPI.Data;
using atmAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Data;
using System.Net.NetworkInformation;

namespace atmAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class accountController : ControllerBase
    {
        ApplicationDbContext _dbContext;

        public accountController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //get method
        [HttpGet]

        public List<account> GetAccounts() {

            var accounts = _dbContext.accounts.ToList();


            return accounts;
        }

        //get account by Id
        [HttpGet("{id}")]
        public async Task<ActionResult<account>> Show(int id)
        {


            if (_dbContext.accounts == null)
            {

                return NotFound();
            }

            var accounts = await _dbContext.accounts.FindAsync(id);

            if (accounts == null) return NotFound();


            return accounts;
        }


        [HttpGet]
        [Route("GetTransFromAcc")]
        public async Task<ActionResult<client>> GetTransFromAcc(int id)
        {

            var dbUser = _dbContext.accounts.Where(x => x.account_id == id).SelectMany(y => y.transactions);


            if (dbUser == null) { return NotFound(); }


            return Ok(dbUser);
        }

        //post method
        //[HttpPost]


        //public async Task<ActionResult<account>> PostAccounts(account account)
        //{
        //    _dbContext.accounts.Add(account);

        //    await _dbContext.SaveChangesAsync();


        //    return CreatedAtAction(nameof(GetAccounts), new { id = account.account_id }, account);
        //}

        //put method


        //[HttpPut("{id}")]


        //public async Task<IActionResult> PutAccoount(int id, client client)
        //{

        //    var dbUser = _dbContext.accounts.Where(x => x.account_id == id).FirstOrDefault();

        //    if (id != client.client_id)
        //    {

        //        return BadRequest();

        //    }




        //    _dbContext.Entry(client).State = EntityState.Modified;


        //    try
        //    {


        //        await _dbContext.SaveChangesAsync();


        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {


        //        if (!accountsExist(id))
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


        //withdraw







   
        [HttpPut()]
        [Route("withdraw")]

        public async Task<IActionResult> Withdraw(int id, int amount)
        {
           
            var dbUser = _dbContext.accounts.Where(x => x.account_id == id).FirstOrDefault();
    
           
            if (dbUser == null)
            {

                return BadRequest();

            }

            if (dbUser.balance == null || amount >dbUser.balance)
            {

                return BadRequest();

            }

            
            transaction transaction = new transaction();
            transaction.amount = amount;
            transaction.transaction_type = "Withdraw";
            transaction.account = dbUser;


            _dbContext.transactions.Add(transaction);

            dbUser.balance = dbUser.balance - amount;


            await _dbContext.SaveChangesAsync();

            return Ok("Withdraw Successful!");

        }












        [HttpPut()]
        [Route("deposit")]

        public async Task<IActionResult> Deposit(int id, int amount)
        {

            var dbUser = _dbContext.accounts.Where(x => x.account_id == id).FirstOrDefault();

            if (dbUser == null)
            {

                return BadRequest();

            }

            transaction transaction = new transaction();
            transaction.amount = amount;
            transaction.transaction_type = "Deposit";
            transaction.account = dbUser;


            _dbContext.transactions.Add(transaction);

            dbUser.balance = dbUser.balance + amount;

            await _dbContext.SaveChangesAsync();

            return Ok("Deposit Successful!");

        }

        //private bool accountsExist(long id)
        //{

        //    return (_dbContext.accounts?.Any(e => e.account_id == id)).GetValueOrDefault();
        //}

        //[HttpPatch]
        //[Route("withdraw")]

        //public IActionResult withdraw(int id, [FromBody] JsonPatchDocument<account> patchEntity) {


        //    var entity = account.FirstorDefault();


        //    return Ok();
        //}




        //Delete  method

        //[HttpDelete("{id}")]

        //public string Delete(int id)
        //{

        //    // get account me ket id
        //    var foundAccount = _dbContext.accounts.FirstOrDefault(x => x.account_id == id);

        //    if (foundAccount is null) return "No account found with this id!";

        //    _dbContext.accounts.Remove(foundAccount);

        //    bool isDeleted = _dbContext.SaveChanges() > 0;
        //    if (isDeleted)
        //    {

        //        return "Account deleted successfully";
        //    }

        //    return "Error";
        //}















    }






    }




    

