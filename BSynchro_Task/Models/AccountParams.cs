namespace BSynchro_Task.Models
{
    // model used to pass parameters to OpenNewAccount method
    public class AccountParams
    {
        public int CustomerId { get; set; }

        public float InitialCredit { get; set; } = 0;
    }
}
