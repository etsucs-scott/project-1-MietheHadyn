using PizzaDeliverySystem.Core.Models;

namespace PizzaDeliverySystem.Core.Services;

// Intentionally awful "monster class" for a SOLID refactoring activity. 
  //this NEEDS to be several classes, each doing different things and THEN speaking to one another in one more general class/run
public class PizzaOrderSystem
{
    private readonly string _dbConnection = "Server=fake-sql;Database=PizzaOrders;User=admin;Pwd=admin";
    private int _lastOrderNumber = 1000;

    public void PlaceOrder(Order order)
    {
        // TODO: this is getting huge...
        Console.WriteLine("--- PIZZA ORDER SYSTEM ---");
        Console.WriteLine($"[{DateTime.Now}] Starting order flow...");

        if (order == null)
        {
            Console.WriteLine("Order is null. Nothing to do.");
            return;
        }

        // Validation (SRP? nah)
        if (string.IsNullOrWhiteSpace(order.CustomerName))
        {
            Console.WriteLine("Missing customer name.");
            return; // quick fix for now
        }

        if (order.IsDelivery && string.IsNullOrWhiteSpace(order.Address))
        {
            Console.WriteLine("Delivery requires an address.");
            return;
        }

        if (order.Pizzas == null || order.Pizzas.Count == 0)
        {
            Console.WriteLine("No pizzas in the order.");
            return;
        }

        if (order.Timestamp == DateTime.MinValue)
        {
            order.Timestamp = DateTime.Now;
        }

        // Pricing logic + promo codes + delivery fees
        decimal total = 0m;
        foreach (var pizza in order.Pizzas)
        {
            decimal pizzaPrice = pizza.BasePrice;

            switch (pizza.Size)  //violation; switch grows with each new size, create seoerate methods in Pizza class for each size and have them calculate their own price
            {
                case PizzaSize.Small:
                    pizzaPrice += 0m;
                    break;
                case PizzaSize.Medium:
                    pizzaPrice += 2m;
                    break;
                case PizzaSize.Large:
                    pizzaPrice += 4m;
                    break;
            }

            // hack: duplicate logic (topping pricing is repeated later too) 
              //this is gonna cause problems, with the pricing getting repeated. it should be an individual class/m public method that gets called
            var toppingCount = pizza.Toppings?.Count ?? 0;
            pizzaPrice += toppingCount * 1.25m;
            total += pizzaPrice;
        }

        // promo switch (OCP violation #1)  
        //alter the discount cases to seperate methods/interfaces, preventing the switch from growing with each new promo code and having the logic for each promo code
        switch ((order.PromoCode ?? string.Empty).Trim().ToUpperInvariant())
        {
            case "HALFOFF":
                total *= 0.5m;
                Console.WriteLine("Applied HALFOFF promo.");
                break;
            case "FREEDRINK":
                total -= 1.50m; // quick fix for now
                Console.WriteLine("Applied FREEDRINK promo.");
                break;
            case "STUDENT10":
                total *= 0.9m;
                Console.WriteLine("Applied STUDENT10 promo.");
                break;
            default:
                // no promo
                break;
        }

        if (order.IsDelivery)
        {
            total += 3.99m; // flat delivery fee
        }

        // Payment processing (OCP violation #2) 
          //alter cases and switch here to separate payprocessing classes/interfaces
        switch (order.PaymentType)
        {
            case PaymentType.Cash:
                Console.WriteLine("Cash payment selected. Collect at door.");
                break;
            case PaymentType.Card:
                Console.WriteLine("Charging credit card...");
                Console.WriteLine("Approved.");
                break;
            case PaymentType.GiftCard:
                Console.WriteLine("Checking gift card balance...");
                Console.WriteLine("Gift card accepted.");
                break;
            default:
                Console.WriteLine("Unknown payment type, defaulting to cash.");
                order.PaymentType = PaymentType.Cash;
                break;
        }

        // Loyalty points (because why not do it here too) 
        int points = 0;
        switch (order.CustomerType)
        {
            case CustomerType.Student:
                points = (int)(total * 2); // student bonus
                break;
            case CustomerType.Regular:
            default:
                points = (int)(total * 1);
                break;
        }

        order.Total = total;

        // "Database" persistence (DIP violation)
        SaveToDatabase(order, points);

        // Delivery scheduling (more mixed responsibilities) //yeah this one thing does too many things, separate them?
        if (order.IsDelivery)
        {
            var driver = AssignDriver();
            var etaMinutes = new Random().Next(20, 50);
            Console.WriteLine($"Driver {driver} assigned. ETA {etaMinutes} minutes.");
        }

        // Receipt printing (console is hard-coded) 
        PrintReceipt(order, points);

        // Notifications (email + SMS? nah)
        SendEmail(order, points);

        Console.WriteLine("Order complete.");
    }

    private void SaveToDatabase(Order order, int points)
    {
        // TODO: remove inline SQL once we get time
        Console.WriteLine($"Connecting to DB: {_dbConnection}");
        Console.WriteLine("INSERT INTO Orders (...) VALUES (...)");
        Console.WriteLine($"Saved order {order.Id} with total {order.Total:C} and {points} points.");
        _lastOrderNumber++;
    }

    private void PrintReceipt(Order order, int points)
    {
        Console.WriteLine("--- RECEIPT ---");
        Console.WriteLine($"Order: {order.Id}");
        Console.WriteLine($"Customer: {order.CustomerName}");

        // hack: duplicate logic (topping pricing is repeated again)
        foreach (var pizza in order.Pizzas)
        {
            var toppings = pizza.Toppings?.Count ?? 0;
            var price = pizza.BasePrice + (toppings * 1.25m);
            Console.WriteLine($"- {pizza.Name} ({pizza.Size}) {price:C}");
        }

        if (order.IsDelivery)
        {
            Console.WriteLine("Delivery fee: $3.99");
            Console.WriteLine($"Deliver to: {order.Address}");
        }

        Console.WriteLine($"Total: {order.Total:C}");
        Console.WriteLine($"Loyalty Points Earned: {points}");
    }

    private void SendEmail(Order order, int points)
    {
        var email = string.IsNullOrWhiteSpace(order.CustomerEmail)
            ? "unknown@example.com"
            : order.CustomerEmail;

        Console.WriteLine($"Sending email to {email}...");
        Console.WriteLine($"Hi {order.CustomerName}, thanks for your order! You earned {points} points.");
    }

    private string AssignDriver()
    {
        // quick fix for now - static list
        var drivers = new[] { "Sam", "Pat", "Riley", "Jordan" };
        return drivers[new Random().Next(0, drivers.Length)];
    }
}