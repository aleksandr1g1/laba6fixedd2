using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Circuit circuit = new Circuit(new List<double> { 10, 20, 30 }, new SeriesConnection());
        circuit.CalculateResistance();

        circuit.ConnectionType = new ParallelConnection();
        circuit.CalculateResistance();

        Console.ReadLine();
    }
}

interface IConnection
{
    double CalculateResistance(List<double> resistances);
}

class SeriesConnection : IConnection
{
    public double CalculateResistance(List<double> resistances)
    {
        double totalResistance = 0;
        foreach (var resistance in resistances)
        {
            if (resistance <= 0)
                throw new ArgumentException("Сопротивление должно быть положительным числом.");
            totalResistance += resistance;
        }
        Console.WriteLine($"Сопротивление при последовательном соединении: {totalResistance} Ом");
        return totalResistance;
    }
}

class ParallelConnection : IConnection
{
    public double CalculateResistance(List<double> resistances)
    {
        double totalReciprocal = 0;
        foreach (var resistance in resistances)
        {
            if (resistance <= 0)
                throw new ArgumentException("Сопротивление должно быть положительным числом.");
            totalReciprocal += 1 / resistance;
        }

        double totalResistance = 1 / totalReciprocal;
        Console.WriteLine($"Сопротивление при параллельном соединении: {totalResistance} Ом");
        return totalResistance;
    }
}

class Circuit
{
    private List<double> resistances;

    public Circuit(List<double> resistances, IConnection connection)
    {
        this.resistances = resistances;
        ConnectionType = connection;
    }

    public IConnection ConnectionType { private get; set; }

    public void CalculateResistance()
    {
        ConnectionType.CalculateResistance(resistances);
    }
}
