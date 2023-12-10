namespace BSynchro_Task.Models
{
    // this model is used to create the customer object and generate new instances from it 
    public class Customer
    {
        public int CustomerId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime CreatedDate { get; set; }

        public int NumberOfAccounts { get; set; }

    }
}
