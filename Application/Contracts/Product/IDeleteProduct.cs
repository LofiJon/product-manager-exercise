namespace Application.Contracts.Product;

public interface IDeleteProduct
{
    Task Execute(Guid id);

}