using BankSystem_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace BankSystem_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        [HttpGet("view exchange rate")]
        public async Task ViewExchangeRates()
        {

            ExchangeRateService exchangeRateService = new ExchangeRateService();
            ExchangeRateData exchangeRates = await exchangeRateService.GetExchangeRatesAsync();

            if (exchangeRates != null)
            {
                Console.WriteLine($"Base Currency: {exchangeRates.base_code}");
                Console.WriteLine("Exchange Rates:");
                foreach (var conversion_rates in exchangeRates.conversion_rates)
                {
                    Console.WriteLine($"{conversion_rates.Key}: {conversion_rates.Value}");
                }
            }
            return;
        }

        [HttpGet("curencyconverter")]
        public async Task<IActionResult> CurrencyConverter(string fromCurrency, string toCurrency, int amount)
        {

            CurrencyConverter currencyConverter = new CurrencyConverter();

            decimal convertedAmount = await currencyConverter.ConvertCurrencyAsync(fromCurrency, toCurrency, amount);
            if (convertedAmount >= 0)
            {
                return Ok($"Converted amount: {convertedAmount} {toCurrency}");
            }
            else
            {
                return BadRequest("bad requerst");
            }

        }


        public static ApplicationDBContext _context;

        public UserController(ApplicationDBContext DB)
        {
            _context = DB;
        }

        [HttpPost("RegisterUser")]
        public IActionResult RegisterUser(string name, string email, string password)
        {
            if (checkPassword.IsStrongPassword(password))
            {

                try
                {
                    var userinfo = new User { Name = name, Email = email, Password = password };


                    _context.Users.Add(userinfo);
                    _context.SaveChanges();

                    return Ok("The User Registereed successfully");

                }
                catch (Exception e)
                {
                    return BadRequest("error when registering user " + e.Message);
                }
            }
            else
            {
                return BadRequest("the password is weak");
            }

        }

        [HttpDelete("DeleteUser")]
        public ActionResult Remove(int ID)
        {
            try
            {
                User user = _context.Users.SingleOrDefault(x => x.Id == ID);

                if (user != null)
                {
                    _context.Users.Remove(user);
                    _context.SaveChanges();

                    return Ok("User removed successfully");
                }
                else
                {
                    return NotFound("User not found");
                }
            }
            catch (Exception ex)
            {

                return BadRequest("Error removing patron: " + ex.Message);
            }
        }
        [HttpPut("UpdateUser")]
        public IActionResult UpdateUser(int ID, string Name, string Email, string password)
        {
            try
            {
                User user = _context.Users.SingleOrDefault(x => x.Id == ID);

                if (user != null)
                {
                    user.Name = Name;
                    user.Email = password;
                    user.Email = password;

                    _context.Update(user);
                    _context.SaveChanges();

                    return Ok("User updated successfully");
                }
                else
                {
                    return NotFound("User not found");
                }
            }
            catch (Exception ex)
            {
                
                return BadRequest("Error updating User: " + ex.Message);
            }
        }

        [HttpGet("GetUserId")]
        public IActionResult GetuserId(string email,string password)
        {
            var user = _context.Users.SingleOrDefault(u=>u.Email == email && u.Password == password);
            if (user != null)
            {
                return Ok(user.Id);
            }
            else
            {
                return NotFound("User not found");
            }
        }


    }
}