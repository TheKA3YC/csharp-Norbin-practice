using System.Text;
public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Введите данные продукта через пробел (Даты в формате [ДД.ММ.ГГГГ]): \nНазвание, Производитель, Цена, Дата Производства, Дата Окончания срока годности.");
        string productInput = Console.ReadLine();
        string[] productStructure = productInput.Split(" ");

        string name = productStructure[0];
        string manufacturer = productStructure[1];

        if (!int.TryParse(productStructure[2], out int price))
        {
            throw new ArgumentException("Неверный формат цены");
        }

        if (!DateTime.TryParse(productStructure[3], out DateTime productionDate))
        {
            throw new ArgumentException("Ошибка: Неверный формат даты производства.");
        }

        if (!DateTime.TryParse(productStructure[4], out DateTime expirationDate))
        {
            throw new ArgumentException("Ошибка: Неверный формат даты окончания срока годности.");
        }

        var product = new Product
        {
            Name = name,
            Manufacturer = manufacturer,
            Price = price,
            ProductionDate = productionDate,
            ExpirationDate = expirationDate
        };

        Console.WriteLine(product);
    }
}

public class Product
{
    public required string Name { get; init; }
    public required string Manufacturer { get; init; }
    public required DateTime ProductionDate { get; init; }

    private int _price;
    public required int Price
    {
        get => _price;
        set
        {
            if (value > 0)
            {
                _price = value;
            }
            else
            {
                throw new ArgumentException("Цена не может быть равна 0 или меньше.");
            }
        }
    }

    private DateTime _expirationDate;
    public required DateTime ExpirationDate
    {
        get => _expirationDate;
        set
        {
            if (value > ProductionDate)
            {
                _expirationDate = value;
            }
            else
            {
                throw new ArgumentException("Дата окончания срока должна быть позже даты производства");
            }
        }
    }

    public override string ToString()
    {
        var descriptionProduct = new StringBuilder();
        descriptionProduct.AppendLine($"Название продукта: {Name}");
        descriptionProduct.AppendLine($"Производитель продукта: {Manufacturer}");
        descriptionProduct.AppendLine($"Цена продукта: {Price} руб.");
        descriptionProduct.AppendLine($"Дата изготовления: {ProductionDate:d}");
        descriptionProduct.AppendLine($"Дата окончания срока годности: {ExpirationDate:d}");
        return descriptionProduct.ToString();
    }
}
