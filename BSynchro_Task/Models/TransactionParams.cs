namespace BSynchro_Task.Models
{
    // this model is used to be passed as a parameter for method called AddNewTransaction for specific account
    public class TransactionParams
    {
        public int AccountId { get; set; }

        public float InitialCredit { get; set; } = 0;
    }
}
