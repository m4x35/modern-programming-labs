namespace DeliveryService.Domain;

public class DeliveryOrder
{
    // Encapsulation:
    // зовнішній код не може напряму змінювати список посилок.
    private readonly List<Parcel> _parcels = new();

    public int Id { get; }
    public Customer Customer { get; }
    public IDeliveryMethod DeliveryMethod { get; }

    public Courier? Courier { get; private set; }

    // State
    public bool IsStarted { get; private set; }
    public bool IsCompleted { get; private set; }
    public bool IsCancelled { get; private set; }

    // Encapsulation + read-only access
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

    // Behavior
    public void AddParcel(Parcel parcel)
    {
        if (IsCancelled)
        {
            throw new InvalidOperationException(
                "Cannot add parcel to cancelled order.");
        }

        if (IsCompleted)
        {
            throw new InvalidOperationException(
                "Cannot add parcel to completed order.");
        }

        _parcels.Add(parcel);
    }

    // Behavior
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

    // Behavior
    public void StartDelivery()
    {
        if (IsCancelled)
        {
            throw new InvalidOperationException(
                "Cancelled order cannot be started.");
        }

        if (IsCompleted)
        {
            throw new InvalidOperationException(
                "Completed order cannot be started again.");
        }

        if (Courier is null)
        {
            throw new InvalidOperationException(
                "Courier must be assigned before starting delivery.");
        }

        IsStarted = true;
    }

    // Behavior
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

        if (IsCompleted)
        {
            throw new InvalidOperationException(
                "Delivery is already completed.");
        }

        IsCompleted = true;
    }

    // Behavior
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
        {
            return "Cancelled";
        }

        if (IsCompleted)
        {
            return "Completed";
        }

        if (IsStarted)
        {
            return "In delivery";
        }

        return "Created";
    }
}