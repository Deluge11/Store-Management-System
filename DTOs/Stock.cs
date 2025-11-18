namespace DTOs;

public class Stock
{
   public int id { get; set; }
   public int productStoreMapping { get; set; }
   public int quantity { get; set; }
   public int sellerId { get; set; }
   public DateTime? expiryDate { get; set; }
}
