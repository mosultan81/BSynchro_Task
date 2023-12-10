namespace Transactions_API.Models
{
    // Generate this model to be used as a parameter for AddNewTransaction method 
    public class TransactionParams
    {
        public int AccountId { get; set; }

        public float InitialCredit { get; set; } = 0;
    }
}
