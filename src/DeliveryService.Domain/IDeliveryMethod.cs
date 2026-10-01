namespace DeliveryService.Domain;

public interface IDeliveryMethod
{
    string Name { get; }

    decimal CalculateCost(double totalWeight);
}