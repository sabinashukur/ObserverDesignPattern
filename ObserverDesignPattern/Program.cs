namespace ObserverDesignPattern;

public interface IObserver
{
    string Fullname { get; set; }
    void Update(bool isAvailable);
}

public class CustomerObserver : IObserver
{
    public string Fullname { get; set; }
    public void Update(bool isAvailable)
    {
        if (isAvailable)
            Console.WriteLine($"Hi, {Fullname}, your looking forward product is now available on our store");
    }
    public CustomerObserver(string fullname, ISubject subject)
    {
        Fullname = fullname;
        subject.AddSubscriber(this);
    }
}

public interface ISubject
{
    void AddSubscriber(IObserver observer);
    void RemoveSubscriber(IObserver observer);
    void NotifySubscribers();
}

public class Store : ISubject
{
    public List<IObserver> subscribers = new();
    public string ProductName { get; set; }
    public double ProductPrice { get; set; }

    private bool _isAvailable;

    public bool IsAvailable
    {
        get { return _isAvailable; }
        set
        {
            _isAvailable = value;
            if (value)
            {
                Console.WriteLine("Product availability changed!!! It's IN STOCK.");
                NotifySubscribers();
            }
            else
                Console.WriteLine("The product currently is not available .");
        }
    }
    public void AddSubscriber(IObserver subscriber)
    {
        subscribers.Add(subscriber);
        Console.WriteLine($"New subscriber is added: {subscriber.Fullname}");
    }

    public void RemoveSubscriber(IObserver subscriber)
    {
        subscribers.Remove(subscriber);
    }

    public void NotifySubscribers()
    {
        Console.WriteLine($"Product details: {ProductName}, Price: {ProductPrice}\n\n");
        foreach (IObserver subscriber in subscribers)
        {
            subscriber.Update(IsAvailable);
        }
    }
    public Store(string productName, double productPrice, bool isAvailable)
    {
        ProductName = productName;
        ProductPrice = productPrice;
        IsAvailable = isAvailable;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Store Product = new("Iphone 14", 3100, false);

        IObserver customer1 = new CustomerObserver("Leyla Gocayeva", Product);
        IObserver customer2 = new CustomerObserver("Nihat Rustamli", Product);
        IObserver customer3 = new CustomerObserver("Kenan Nebizade", Product);
        IObserver customer4 = new CustomerObserver("Ferman Esedov", Product);
        IObserver customer5 = new CustomerObserver("Elgun Salmanov", Product);

        Console.WriteLine("\n\n");

        Product.RemoveSubscriber(customer4);

        Product.IsAvailable = true;


    }
}