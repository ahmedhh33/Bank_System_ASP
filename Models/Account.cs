using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace BankSystem_API.Models
{
    public class Account
    {
        [Key]
        public int AccountNumber { get; set; }
        public string AccountHolderName { get; set; }
        public decimal Balance { get; set; }
        public List<Transaction> TransactionHistory { get; private set; }


        [ForeignKey("User")]
        public int Id { get; set; }
        public User User { get; set; }
    }
}
