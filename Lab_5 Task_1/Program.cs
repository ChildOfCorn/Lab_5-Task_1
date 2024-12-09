using System;
using System.Collections.Generic;
using System.Linq;

abstract class Vehicle
{
    public int Speed { get; set; }
    public int Capacity { get; set; }

    public abstract void Move();
}

class Human
{
    public int Speed { get; set; }

    public void Move()
    {
        Console.WriteLine($"A person moves with speed {Speed} km/h.");
    }
}

class Car : Vehicle
{
    public Car()
    {
        Speed = 120;
        Capacity = 5;
    }

    public override void Move()
    {
        Console.WriteLine($"The car is traveling at a speed of {Speed} km/h.");
    }
}

class Bus : Vehicle
{
    public Bus()
    {
        Speed = 80;
        Capacity = 50;
    }

    public override void Move()
    {
        Console.WriteLine($"The bus is traveling at a speed of {Speed} km/h.");
    }
}

class Train : Vehicle
{
    public Train()
    {
        Speed = 200;
        Capacity = 500;
    }

    public override void Move()
    {
        Console.WriteLine($"The train is moving at speed {Speed} km/h.");
    }
}

class Route
{
    public string StartPoint { get; set; }
    public string EndPoint { get; set; }
    public int Distance { get; set; }

    public Route(string startPoint, string endPoint, int distance)
    {
        StartPoint = startPoint;
        EndPoint = endPoint;
        Distance = distance;
    }

    public void CalculateOptimalRoute(Vehicle vehicle)
    {
        double time = (double)Distance / vehicle.Speed;
        Console.WriteLine($"The optimal route for {vehicle.GetType().Name}: {time:F2} h.");
    }
}

class TransportNetwork
{
    private List<Vehicle> vehicles = new List<Vehicle>();
    private Dictionary<Vehicle, int> passengers = new Dictionary<Vehicle, int>();

    public void AddVehicle(Vehicle vehicle)
    {
        vehicles.Add(vehicle);
        passengers[vehicle] = 0;
    }

    public void MoveAll()
    {
        foreach (var vehicle in vehicles)
        {
            vehicle.Move();
        }
    }

    public void BoardPassengers(Vehicle vehicle, int count)
    {
        if (!vehicles.Contains(vehicle))
        {
            Console.WriteLine($"Transportation {vehicle.GetType().Name} not available online.");
            return;
        }

        if (passengers[vehicle] + count > vehicle.Capacity)
        {
            Console.WriteLine($"Not enough space in the {vehicle.GetType().Name}. Balance: {vehicle.Capacity - passengers[vehicle]}.");
        }
        else
        {
            passengers[vehicle] += count;
            Console.WriteLine($"Boarded {count} passengers in {vehicle.GetType().Name}. Overall: {passengers[vehicle]}.");
        }
    }

    public void DropPassengers(Vehicle vehicle, int count)
    {
        if (!vehicles.Contains(vehicle))
        {
            Console.WriteLine($"Transport {vehicle.GetType().Name} not available online.");
            return;
        }

        if (passengers[vehicle] < count)
        {
            Console.WriteLine($"In {vehicle.GetType().Name} not enough passengers to disembark.");
        }
        else
        {
            passengers[vehicle] -= count;
            Console.WriteLine($"Disembarked {count} passengers from {vehicle.GetType().Name}. Remaining: {passengers[vehicle]}.");
        }
    }
}

class Program
{
    static void Main()
    {
        TransportNetwork network = new TransportNetwork();

        Vehicle car = new Car();
        Vehicle bus = new Bus();
        Vehicle train = new Train();

        network.AddVehicle(car);
        network.AddVehicle(bus);
        network.AddVehicle(train);

        network.BoardPassengers(car, 3);
        network.BoardPassengers(bus, 30);
        network.BoardPassengers(train, 200);

        network.MoveAll();

        Route route = new Route("Kyiv", "Lviv", 540);
        route.CalculateOptimalRoute(car);
        route.CalculateOptimalRoute(bus);
        route.CalculateOptimalRoute(train);

        network.DropPassengers(bus, 10);
    }
}
