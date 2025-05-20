namespace App.Application.Contracts.Persistance
{
    public record ProductDto(int Id, string Name, decimal Price, int Stock, int CategoryId);
}
