namespace Application.Requests;

public class AddStockRequest
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}