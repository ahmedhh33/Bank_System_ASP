using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BankSystem_API.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        public DateTime Timestamp { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public int SourceAccountNumber { get; set; }
        public int TargetAccountNumber { get; set; }


        [ForeignKey("account")]
        public int AccountNumber { get; set; }
        public Account account { get; set; }
    }
    public enum TransactionType
    {
        Deposit,
        Withdrawal,
        Transfer
    }
}
