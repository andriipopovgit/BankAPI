namespace BankAPI
{
    public class BankAccount : IIdentify
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public decimal Sum { get; set; }
        public Person Person { get; set; }
    }
}
