namespace BSynchro_Task.Models
{
    // this model is used to generate an object called transaction and as outoput data structure object for AddNewTransaction method
    public class Transaction
    {
        public int TransactioinId { get; set; }

        public float InitialCreditValue { get; set; }

        public int AccountId { get; set; }

        public DateTime AddedDate { get; set; }

    }
}
