using BankSystem_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BankSystem_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        public static ApplicationDBContext _context;

        public AccountController(ApplicationDBContext DB)
        {
            _context = DB;
        }

        private int accountHolderID;
        private string AccountHolderName;





        [HttpPost("login")]
        public IActionResult post(string email, string password)
        {
            User user = _context.Users.Where(n => n.Email == email && n.Password == password).FirstOrDefault();
            if (user != null)
            {

                accountHolderID = user.Id; //save it in global variable
                AccountHolderName = user.Name;

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authentication"));

                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


                var data = new List<Claim>();
                data.Add(new Claim("Name", user.Name));


                var token = new JwtSecurityToken(
                issuer: "ahmed",
                audience: "TRA",
                claims: data,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials

                );

                return Ok(new JwtSecurityTokenHandler().WriteToken(token));

            }
            else
            {
                return Unauthorized("the user doesn't exist");
            }

        }
        [HttpPost("CreatAccount")]
        public IActionResult CreateAccount(decimal initialBalance)
        {
            try
            {
                // Create a new Account object
                var newAccount = new Account { Balance = initialBalance, Id = accountHolderID, AccountHolderName = AccountHolderName };// Glopal user id created when ever the user login from function info

                // Add the new account to the context and save changes to the database
                _context.Accounts.Add(newAccount);
                int rowsAffected = _context.SaveChanges();

                if (rowsAffected > 0)
                {
                    return Ok("Bank account created successfully.");
                }
                else
                {
                    return BadRequest("Error creating bank account.");

                }

            }
            catch (Exception e)
            {
                return BadRequest("unable to create account " + e.Message);
            }
        }


        [HttpDelete("DeleteAccount")]
        public IActionResult DeleteAccount(int accountNumber)
        {

            try
            {

                var accountToDelete = _context.Accounts.Where(x => x.Id == accountHolderID).SingleOrDefault(a => a.AccountNumber == accountNumber);

                if (accountToDelete != null)
                {
                    _context.Accounts.Remove(accountToDelete);
                    int rowsAffected = _context.SaveChanges();

                    if (rowsAffected > 0)
                    {
                        return Ok($"Account {accountNumber} deleted successfully.");
                    }
                    else
                    {
                        return BadRequest($"Error deleting account {accountNumber}.");
                    }
                }
                else
                {
                    return BadRequest("Account not found.");
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet("GetUserAcconts")]
        public IActionResult UserAccounts()
        {

            try
            {

                var userAccounts = _context.Accounts.Where(a => a.Id == accountHolderID).ToList();
                if (userAccounts != null)
                {
                    return Ok(userAccounts);
                }
                else
                {
                    return BadRequest("there is no account");
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}
