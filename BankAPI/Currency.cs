namespace BankAPI
{
    public class Currency : IIdentify
    {
        public int Id { get; set; }
        public char? Symbol { get; set; }
        public decimal? ExchangeRate { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
    }
}
