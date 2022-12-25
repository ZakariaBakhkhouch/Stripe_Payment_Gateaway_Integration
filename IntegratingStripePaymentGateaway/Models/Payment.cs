namespace IntegratingStripePaymentGateaway.Models
{
    public class Payment
    {
        public long Amount { get; set; }

        public string ProductName { get; set; }
        
        public string Currency { get; set; }
        
        public int Quantity { get; set; }
    }
}
