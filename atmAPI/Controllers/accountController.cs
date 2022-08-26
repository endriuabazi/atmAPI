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

        public List<Account> GetAccounts() {

            var accounts = _dbContext.accounts.ToList();


            return accounts;
        }

        //get account by Id
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> Show(int id)
        {


            var account = await _dbContext.accounts.FindAsync(id);

            if (account == null) return NotFound();


            return account;
        }


        [HttpGet]
        [Route("GetTransFromAcc")]
        public async Task<ActionResult<Client>> GetTransFromAcc(int id)
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
           
            var account = _dbContext.accounts.Where(x => x.account_id == id).FirstOrDefault();
    
           
            if (account == null)
            {

                return BadRequest();

            }

            if (account.balance == null || amount > account.balance)
            {

                return BadRequest();

            }

            
            Transaction transaction = new Transaction();
            transaction.amount = amount;
            transaction.transaction_type = "Withdraw";
            transaction.account = account;


            _dbContext.transactions.Add(transaction);

            account.balance = account.balance - amount;


            await _dbContext.SaveChangesAsync();

            return Ok("Withdraw Successful!");

        }



        [HttpPut()]
        [Route("deposit")]

        public async Task<IActionResult> Deposit(int id, int amount)
        {

            var account = _dbContext.accounts.Where(x => x.account_id == id).FirstOrDefault();

            if (account == null)
            {

                return BadRequest();

            }

            Transaction transaction = new Transaction();
            transaction.amount = amount;
            transaction.transaction_type = "Deposit";
            transaction.account = account;


            _dbContext.transactions.Add(transaction);

            account.balance = account.balance + amount;

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





        [HttpPut()]
        [Route("send")]

        public async Task<IActionResult> Send(int id, int amount, string account_name)
        {
            var existSendAccount = _dbContext.accounts.Where(x => x.account_name == account_name).FirstOrDefault();
            //var sendaccount = _dbContext.accounts.Where(x => x.account_name == account_name).Select(x => x.balance);
            var account = _dbContext.accounts.Where(x => x.account_id == id).FirstOrDefault();

            if (account == null)
            {

                return BadRequest();

            }

            Transaction transaction = new Transaction();
            transaction.amount = amount;
            transaction.transaction_type = "Send";
            transaction.account = account;


            _dbContext.transactions.Add(transaction);

            account.balance = account.balance - amount;

            if (existSendAccount.currency != account.currency) {

                if (existSendAccount.currency == '$')

                    amount = amount * 2;
                else if (existSendAccount.currency == '€') {

                    amount = amount /2;
                    
                }

            }
        existSendAccount.balance = existSendAccount.balance + amount;

            if (existSendAccount == null)
            {
                return NotFound();
            }

            await _dbContext.SaveChangesAsync();

            return Ok("Deposit Successful!");

        }










    }






    }




    

