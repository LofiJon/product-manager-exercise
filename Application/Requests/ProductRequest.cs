namespace Application.Requests;

public class ProductRequest
{
    public string? Name { get; set; }
    public string? PartNumber { get; set; }
    public int? StockQuantity { get; set; }
    public decimal? Price { get; set; }
    public int? ConsumedQuantity { get; set; }
    public decimal? ConsumedPrice { get; set; }

}