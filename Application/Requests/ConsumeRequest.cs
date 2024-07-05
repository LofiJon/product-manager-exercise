namespace Application.Requests;

public class ConsumeRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}