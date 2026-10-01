namespace DeliveryService.Domain;

public class Parcel
{
    public int Id { get; }
    public string Description { get; }
    public double Weight { get; }

    public Parcel(int id, string description, double weight)
    {
        if (weight <= 0)
        {
            throw new ArgumentException("Weight must be greater than 0.");
        }

        Id = id;
        Description = description;
        Weight = weight;
    }

    public override string ToString()
    {
        return $"{Description} ({Weight} kg)";
    }
}