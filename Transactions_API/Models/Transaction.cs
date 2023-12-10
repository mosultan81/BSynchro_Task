namespace Transactions_API.Models
{
    // Generated Model for Transactions to be used as a Transaction object
    public class Transaction
    {
        public int TransactioinId { get; set; }

        public float InitialCreditValue { get; set; }

        public int AccountId { get; set; }

        public DateTime AddedDate { get; set; }

    }
}
