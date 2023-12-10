namespace BSynchro_Task.Models
{
    // this model is used as a return model type for function that returns the user information, balance and transactions
    public class CustomerInformation
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public float Balance { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
