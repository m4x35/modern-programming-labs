namespace DeliveryService.Domain;

public class StandardDelivery : IDeliveryMethod
{
    public string Name => "Standard Delivery";

    public decimal CalculateCost(double totalWeight)
    {
        return 50m + (decimal)totalWeight * 10m;
    }
}