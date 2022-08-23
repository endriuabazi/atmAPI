using atmAPI.Data;
using atmAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Net.NetworkInformation;

namespace atmAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class clientController : Controller
    {
        ApplicationDbContext _dbContext;
        public clientController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //get method
        [HttpGet]

        public async Task<ActionResult<IEnumerable<client>>> GetClients()
        {
            if (_dbContext.clients == null)
            {

                return NotFound();

            }
            return await _dbContext.clients.ToListAsync();

        }

        //get method by Id
        //[HttpGet("{id}")]
        //public async Task<ActionResult<client>> ShowClient(int id)
        //{


        //    if (_dbContext.clients == null)
        //    {

        //        return NotFound();
        //    }

        //    var clients = await _dbContext.clients.FindAsync(id);

        //    if (clients == null) return NotFound();


        //    return clients;
        //}



        // get accounts from clients
        [HttpGet]
        [Route("GetAccountsFromClients")]
        public async Task<ActionResult<client>> GiveAccounts(string username)
        {

            var dbUser = _dbContext.clients.Where(x=>x.username == username).SelectMany(y=>y.accounts);
          


            if ( dbUser == null) { return NotFound(); }


            return Ok(dbUser);
           
        }

        //[HttpGet]
        //[Route("GetClientId")]
        //public async Task<ActionResult<client>> GetClientId(int pin, string username)
        //{

        //    var dbUser = _dbContext.clients.Where(x => x.pin == pin && x.username == username).FirstOrDefault();

        //    if (dbUser == null) { return BadRequest(); }

        //    return Ok("");
        //}











        //post method
        //[HttpPost]


        //public async Task<ActionResult<client>> PostClients(client client)
        //{
        //    _dbContext.clients.Add(client);

        //    await _dbContext.SaveChangesAsync();


        //    return CreatedAtAction(nameof(GetClients), new { id = client.client_id }, client);
        //}


        //login method


        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<client>> LoginClients(int pin,string username)
        {   
            var dbUser = _dbContext.clients.Where(x => x.pin == pin && x.username == username).FirstOrDefault();

            if (dbUser == null) { return BadRequest(); }

            return Ok("Lezzgooo!");
           

        }

        [HttpPut()]
        [Route("changePin")]

        public async Task<IActionResult> changePin(string username,int pin1, int pin2)
        {

            var dbUser = _dbContext.clients.Where(x => x.username == username).FirstOrDefault();

            if (dbUser != null && pin1 == pin2 )
            {
                dbUser.pin = pin1;

                await _dbContext.SaveChangesAsync();

                return Ok("Pin Changed successfully!");

               

            }

         

            return BadRequest();

        }




        [HttpPut()]
        [Route("editProfile")]

        public async Task<IActionResult> editProfile(string username, string newusername , string address, string phone, string email)
        {

            var dbUser = _dbContext.clients.Where(x => x.username == username).FirstOrDefault();

            if (dbUser != null)
            {
                dbUser.username = newusername;
                dbUser.address = address;
                dbUser.client_phone = phone;
                dbUser.email = email;


                await _dbContext.SaveChangesAsync();

                return Ok("Profile edited success!");



            }



            return BadRequest();

        }


        ////Delete  method

        //[HttpDelete("{id}")]

        //public string Delete(int id)
        //{

        //    // get account me ket id
        //    var foundClient = _dbContext.clients.FirstOrDefault(x => x.client_id == id);

        //    if (foundClient is null) return "No client found with this id!";

        //    _dbContext.clients.Remove(foundClient);

        //    bool isDeleted = _dbContext.SaveChanges() > 0;
        //    if (isDeleted)
        //    {

        //        return "Client deleted successfully";
        //    }

        //    return "Error";
        //}


    }
}
