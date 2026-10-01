namespace DeliveryService.Domain;

public class Customer
{
    public int Id { get; }
    public string Name { get; }
    public string Phone { get; }

    public Customer(int id, string name, string phone)
    {
        Id = id;
        Name = name;
        Phone = phone;
    }

    public override string ToString()
    {
        return $"{Name}, {Phone}";
    }
}