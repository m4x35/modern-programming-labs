namespace DeliveryService.Domain;

public class ExpressDelivery : IDeliveryMethod
{
    public string Name => "Express Delivery";

    public decimal CalculateCost(double totalWeight)
    {
        return 100m + (decimal)totalWeight * 20m;
    }
}