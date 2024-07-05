namespace Core.Entities;

public class Product : BaseEntity
{
    public string? Name { get; set; }
    public string? PartNumber { get; set; }
    public int? StockQuantity { get; set; }
    public decimal? Price { get; set; }
    public int? ConsumedQuantity { get; set; }
    public decimal? ConsumedPrice { get; set; }
    
    public bool Consume(int quantity)
    {
        if (StockQuantity >= quantity)
        {
            StockQuantity -= quantity;
            ConsumedQuantity += quantity;
            ConsumedPrice += quantity * Price.GetValueOrDefault();
            UpdatedAt = DateTime.Now;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddStock(int quantity, decimal price)
    {
        if (StockQuantity.HasValue && StockQuantity.Value > 0)
        {
            Price = ((Price.GetValueOrDefault() * StockQuantity.Value) + (price * quantity)) / (StockQuantity.Value + quantity);
        }
        else
        {
            Price = price;
        }

        StockQuantity = StockQuantity.GetValueOrDefault() + quantity;
        UpdatedAt = DateTime.Now;
    }

    public void ResetDailyConsumption()
    {
        ConsumedQuantity = 0;
        ConsumedPrice = 0;
        UpdatedAt = DateTime.Now;
    }
}