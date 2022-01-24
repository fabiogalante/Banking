namespace Account.Service.Request
{
    public class TransferAccount
    {
        public string AccountNumber { get; set; }
        public decimal Value { get; set; }
        public CreditDebit Type { get; set; }
    }

    public enum CreditDebit
    {
        Credit,
        Debit
    }
}
