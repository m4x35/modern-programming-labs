namespace DeliveryService.Domain;

public class DeliveryOrder
{
    private readonly List<Parcel> _parcels = new();

    public int Id { get; }
    public Customer Customer { get; }
    public IDeliveryMethod DeliveryMethod { get; }

    public Courier? Courier { get; private set; }

    public bool IsStarted { get; private set; }
    public bool IsCompleted { get; private set; }
    public bool IsCancelled { get; private set; }

    public IReadOnlyCollection<Parcel> Parcels => _parcels.AsReadOnly();

    public DeliveryOrder(
        int id,
        Customer customer,
        IDeliveryMethod deliveryMethod)
    {
        Id = id;
        Customer = customer;
        DeliveryMethod = deliveryMethod;
    }

    public void AddParcel(Parcel parcel)
    {
        if (IsCancelled || IsCompleted)
        {
            throw new InvalidOperationException(
                "Cannot add parcel to completed or cancelled order.");
        }

        _parcels.Add(parcel);
    }

    public void AssignCourier(Courier courier)
    {
        if (IsCancelled)
        {
            throw new InvalidOperationException(
                "Courier cannot be assigned to cancelled order.");
        }

        if (IsCompleted)
        {
            throw new InvalidOperationException(
                "Courier cannot be assigned to completed order.");
        }

        Courier = courier;
    }

    public void StartDelivery()
    {
        if (IsCancelled)
        {
            throw new InvalidOperationException(
                "Cancelled order cannot be started.");
        }

        if (Courier is null)
        {
            throw new InvalidOperationException(
                "Courier must be assigned before starting delivery.");
        }

        IsStarted = true;
    }

    public void CompleteDelivery()
    {
        if (IsCancelled)
        {
            throw new InvalidOperationException(
                "Cancelled order cannot be completed.");
        }

        if (!IsStarted)
        {
            throw new InvalidOperationException(
                "Delivery must be started before completion.");
        }

        IsCompleted = true;
    }

    public void Cancel()
    {
        if (IsCompleted)
        {
            throw new InvalidOperationException(
                "Completed delivery cannot be cancelled.");
        }

        IsCancelled = true;
    }

    public double GetTotalWeight()
    {
        return _parcels.Sum(parcel => parcel.Weight);
    }

    public decimal GetDeliveryCost()
    {
        return DeliveryMethod.CalculateCost(GetTotalWeight());
    }

    public string GetStatus()
    {
        if (IsCancelled)
            return "Cancelled";

        if (IsCompleted)
            return "Completed";

        if (IsStarted)
            return "In delivery";

        return "Created";
    }
}