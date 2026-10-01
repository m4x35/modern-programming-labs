using DeliveryService.Domain;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("Modern Programming Course");
Console.WriteLine();

Console.WriteLine("Student: Кулик Максим");
Console.WriteLine("Group: 401-ТК");
Console.WriteLine();

Console.WriteLine("Variant: 5");
Console.WriteLine("Domain: Delivery Service");
Console.WriteLine();

Console.WriteLine(".NET: 10");
Console.WriteLine();

var customer = new Customer(
    1,
    "Кулик Максим",
    "+380663915108");

var courier = new Courier(
    1,
    "Владислав Бублій",
    "+380998788767");

IDeliveryMethod deliveryMethod = new StandardDelivery();

var order = new DeliveryOrder(
    1,
    customer,
    deliveryMethod);

order.AddParcel(
    new Parcel(1, "Ноутбук", 2.5));

order.AddParcel(
    new Parcel(2, "Документи", 0.5));

order.AssignCourier(courier);

Console.WriteLine($"Customer: {customer}");
Console.WriteLine($"Delivery method: {deliveryMethod.Name}");
Console.WriteLine($"Parcels: {order.Parcels.Count}");
Console.WriteLine($"Total weight: {order.GetTotalWeight()} kg");
Console.WriteLine($"Delivery cost: {order.GetDeliveryCost()} грн");
Console.WriteLine($"Status: {order.GetStatus()}");

order.StartDelivery();

Console.WriteLine();
Console.WriteLine($"Status after start: {order.GetStatus()}");

order.CompleteDelivery();

Console.WriteLine($"Status after completion: {order.GetStatus()}");