namespace Application.Contracts.Product;

public interface IResetDailyConsumption
{
    Task Execute(Guid id);
}