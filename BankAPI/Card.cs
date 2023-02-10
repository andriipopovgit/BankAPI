namespace BankAPI
{
    public class Card : IIdentify
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime UsedTo { get; set; }
        public int CVI { get; set; }
        public Currency Currency { get; set; }
        public BankAccount BankAccount { get; set; }
        public string PIN { get; set; }
    }
}
