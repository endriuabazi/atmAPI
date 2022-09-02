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

        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            if (_dbContext.clients == null)
            {

                return NotFound();

            }
            return await _dbContext.clients.ToListAsync();

        }



        [HttpGet]
        [Route("GetClientByUsername")]
        public async Task<ActionResult<IEnumerable<Client>>> GetClientbyUsername(string username)
        {

            var clientExist = _dbContext.clients.Where(x => x.username == username);

            if (clientExist == null) {
                return NotFound("Client not found!");
            
            }
            return Ok(clientExist);

        }




        //[HttpGet]
        //[Route("getClientsbyUsername")]
        //public async Task<ActionResult<Client>> getClientsbyUsername(string username)

        //{

        //    var clientWiththisID = _dbContext.clients.Where(x => x.username == username);

        //    if (clientWiththisID == null)
        //    {

        //        return NotFound();
        //    }

        //    return Ok(clientWiththisID);

        //}



        // get accounts from clients
        [HttpGet]
        [Route("GetAccountsFromClients")]
        public async Task<ActionResult<Client>> GiveAccounts(string username)
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
        public async Task<ActionResult<Client>> LoginClients(int pin,string username)
        {   
            var dbUser = _dbContext.clients.Where(x => x.pin == pin && x.username == username).FirstOrDefault();

            if (dbUser == null) { return BadRequest(); }
            
            if (dbUser.pin != pin) {

                return NotFound("Incorrect Pin");
            
            }
            else if (dbUser.username != username)
            {

                return NotFound("Incorrect Username");

            }

            return Ok("Login succesfull!");
           

        }

        [HttpPut]
        [Route("changePin")]

        public async Task<IActionResult> changePin(int id,int pin1, int pin2)
        {

            var clientExist = _dbContext.clients.Where(x => x.client_id == id).FirstOrDefault();

            if (clientExist != null && pin1 == pin2 )
            {
                clientExist.pin = pin1;

                await _dbContext.SaveChangesAsync();

                return Ok("Pin Changed successfully!");

               

            }

            if (clientExist.pin == pin1 && pin1 == pin2) {


                return BadRequest("This pin exist!");
            
            }

         

            return BadRequest();

        }




        [HttpPut()]
        [Route("editProfile")]

        public async Task<IActionResult> editProfile(int id, string username , string address, string phone, string email)
            {

            var clientExist = _dbContext.clients.Where(x => x.client_id == id).FirstOrDefault();


            if (clientExist != null)
            {
                


                clientExist.username = username;
                clientExist.address = address;
                clientExist.client_phone = phone;
                clientExist.email = email;



                await _dbContext.SaveChangesAsync();

                return Ok("Profile edited success!");



            }



            return BadRequest();

        }


        [HttpPut()]
        [Route("editProfilewithID")]

        public async Task<IActionResult> editProfilewithID(int id, string username, string address, string phone, string email)
        {

            var clientExist = _dbContext.clients.Where(x => x.client_id == id).FirstOrDefault();

            if (clientExist != null)
            {

                
                clientExist.username = username;

               
                clientExist.address = address;
             
               

                clientExist.client_phone = phone;
               
              
               
                clientExist.email = email;



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
