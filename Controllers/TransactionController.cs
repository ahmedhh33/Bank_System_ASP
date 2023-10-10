using BankSystem_API.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace BankSystem_API.Controllers
{
    //[ApiController]
    //[Route("api/[controller]")]
    //public class TransactionController : ControllerBase
    //{
    //    public static ApplicationDBContext _context;

    //    public TransactionController(ApplicationDBContext DB)
    //    {
    //        _context = DB;
    //    }

    //    [HttpPost("MakeDeposit")]
    //    public IActionResult MakeDeposit(int accountNumber, decimal amount)
    //    {
    //        if (amount <= 0)
    //        {
    //            return BadRequest("Invalid deposit amount.");
    //        }

    //        try
    //        {
    //            var account = _context.Accounts.SingleOrDefault(a => a.AccountNumber == accountNumber);

    //            if (account != null)
    //            {
    //                account.Balance += amount;
    //                int rowsAffected = _context.SaveChanges();

    //                if (rowsAffected > 0)
    //                {
    //                    Transaction depositTransaction = new Transaction
    //                    {
    //                        Amount = amount,
    //                        Type = TransactionType.Deposit,
    //                        AccountNumber = account.AccountNumber,
    //                        Timestamp = DateTime.Now
    //                    };

    //                    _context.Transactions.Add(depositTransaction);
    //                    int transactionRowsAffected = _context.SaveChanges();

    //                    if (transactionRowsAffected > 0)
    //                    {
    //                        Log.Information($"Depositing successful. Transaction registered.");
    //                        return Ok("Depositing successful. Transaction registered.");
    //                    }
    //                    else
    //                    {
    //                        return BadRequest("Transaction not registered.");
    //                    }
    //                }
    //                else
    //                {
    //                    return BadRequest("Depositing unsuccessful. Balance not updated.");
    //                }
    //            }
    //            else
    //            {
    //                return NotFound("Account not found.");
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //            Log.Error($"Error making a transaction: {e.Message}");
    //            return StatusCode(500, "An error occurred while making the transaction.");
    //        }
    //    }

    //    [HttpPost("Withdraw")]
    //    public IActionResult Withdraw(int accountNumber, decimal amount,int accountHolderID)
    //    {
    //        if (amount <= 0)
    //        {
    //            return BadRequest("Invalid withdrawal amount.");
    //        }

    //        try
    //        {
    //            var account = _context.Accounts.SingleOrDefault(a => a.AccountNumber == accountNumber && a.Id == accountHolderID);

    //            if (account != null)
    //            {
    //                if (account.Balance >= amount)
    //                {
    //                    account.Balance -= amount;
    //                    int rowsAffected = _context.SaveChanges();

    //                    if (rowsAffected > 0)
    //                    {
    //                        Transaction withdrawalTransaction = new Transaction
    //                        {
    //                            Amount = amount,
    //                            Type = TransactionType.Withdrawal,
    //                            AccountNumber = accountNumber,
    //                            Timestamp = DateTime.Now
    //                        };

    //                        _context.Transactions.Add(withdrawalTransaction);
    //                        int transactionRowsAffected = _context.SaveChanges();

    //                        if (transactionRowsAffected > 0)
    //                        {
    //                            Log.Information($"Withdrawal successful. Transaction registered.");
    //                            return Ok("Withdrawal successful. Transaction registered.");
    //                        }
    //                        else
    //                        {
    //                            return BadRequest("Transaction not registered.");
    //                        }
    //                    }
    //                    else
    //                    {
    //                        return BadRequest("Withdrawal unsuccessful. Balance not updated.");
    //                    }
    //                }
    //                else
    //                {
    //                    return BadRequest("Insufficient funds.");
    //                }
    //            }
    //            else
    //            {
    //                return NotFound("Account not found.");
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //            Log.Error($"Error making a withdrawal: {e.Message}");
    //            return StatusCode(500, "An error occurred while making the withdrawal.");
    //        }
    //    }

    //    [HttpPost("Transfer")]
    //    public IActionResult Transfer(int sourceAccountNumber, int targetAccountNumber, decimal amount,int accountHolderID)
    //    {
    //        if (amount <= 0)
    //        {
    //            return BadRequest("Invalid transfer amount.");
    //        }

            
    //            using (var transaction = _context.Database.BeginTransaction())
    //            {
    //                try
    //                {
    //                    var sourceAccount = _context.Accounts.SingleOrDefault(a => a.AccountNumber == sourceAccountNumber && a.Id == accountHolderID);
    //                    var targetAccount = _context.Accounts.SingleOrDefault(a => a.AccountNumber == targetAccountNumber);

    //                    if (sourceAccount == null || targetAccount == null)
    //                    {
    //                        return NotFound("One of the accounts doesn't exist.");
    //                    }

    //                    if (sourceAccount.Balance >= amount)
    //                    {
    //                        sourceAccount.Balance -= amount;
    //                        targetAccount.Balance += amount;

    //                        _context.SaveChanges();

    //                        var transferTransaction = new Transaction
    //                        {
    //                            Amount = amount,
    //                            Type = TransactionType.Transfer,
    //                            SourceAccountNumber = sourceAccountNumber,
    //                            TargetAccountNumber = targetAccountNumber,
    //                            AccountNumber = sourceAccountNumber,
    //                            Timestamp = DateTime.Now
    //                        };
    //                        var transferTargetTransaction = new Transaction
    //                        {
    //                            Amount = amount,
    //                            Type = TransactionType.Transfer,
    //                            SourceAccountNumber = sourceAccountNumber,
    //                            TargetAccountNumber = targetAccountNumber,
    //                            AccountNumber = targetAccountNumber,
    //                            Timestamp = DateTime.Now
    //                        };
    //                        _context.Transactions.Add(transferTransaction);
    //                        _context.Transactions.Add(transferTargetTransaction);
    //                        _context.SaveChanges();

    //                        transaction.Commit();

    //                        Log.Information($"Transferred {amount} OMR from account {sourceAccountNumber} to account {targetAccountNumber}.");
    //                        return Ok($"Transferred {amount} OMR from account {sourceAccountNumber} to account {targetAccountNumber}.");
    //                    }
    //                    else
    //                    {
    //                        return BadRequest("Insufficient funds in the source account.");
    //                    }
    //                }
    //                catch (Exception e)
    //                {
    //                    transaction.Rollback();
    //                    Log.Error($"An error occurred: {e.Message}");
    //                    return StatusCode(500, "An error occurred while making the transfer.");
    //                }
    //            }
            
    //    }


    //    [HttpGet("GetAccountHistory")]
    //    public IActionResult GetAccountHistory(int accountNumber,int accountHolderID)
    //    {
    //        var account = _context.Accounts.SingleOrDefault(a => a.AccountNumber == accountNumber && a.Id == accountHolderID);

    //        if (account == null)
    //        {
    //            return NotFound("Account not found.");
    //        }

    //        var transactions = _context.Transactions
    //            .Where(t => t.AccountNumber == accountNumber)
    //            .ToList();

    //        if (transactions.Any())
    //        {
    //            return Ok(transactions); // Return the transactions as JSON
    //        }
    //        else
    //        {
    //            return NotFound("No transaction history found for this account.");
    //        }
    //    }
    //}
}
 