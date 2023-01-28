namespace BankAPI
{
    public class BankAccount : IIdentify 
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public decimal Sum { get; set; }


        
    }
}
