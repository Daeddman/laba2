using System;


class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Введіть чисельник (nom):");
        long nom = long.Parse(Console.ReadLine());

        Console.WriteLine("Введіть знаменник (denom):");
        long denom = long.Parse(Console.ReadLine());

        MyFrac frac1 = new MyFrac(nom, denom);

        Console.WriteLine("Введіть інший чисельник (nom):");
        long nom2 = long.Parse(Console.ReadLine());

        Console.WriteLine("Введіть інший знаменник (denom):");
        long denom2 = long.Parse(Console.ReadLine());

        MyFrac frac2 = new MyFrac(nom2, denom2);

        Console.WriteLine($"frac1 = {frac1}");
        Console.WriteLine($"frac2 = {frac2}");
        Console.WriteLine($"frac1 + frac2 = {frac1.Add(frac2)}");
        Console.WriteLine($"frac1 - frac2 = {frac1.Subtract(frac2)}");
        Console.WriteLine($"frac1 * frac2 = {frac1.Multiply(frac2)}");
        Console.WriteLine($"frac1 / frac2 = {frac1.Divide(frac2)}");

        Console.WriteLine($"frac1 з цілою частиною = {frac1.ToStringWithIntPart()}");
        Console.WriteLine($"frac2 з цілою частиною = {frac2.ToStringWithIntPart()}");

        Console.WriteLine($"frac1 як дійсне число = {frac1.ToDouble()}");
        Console.WriteLine($"frac2 як дійсне число = {frac2.ToDouble()}");

        Console.WriteLine("Введіть ціле число для виразу:");
        int expr = int.Parse(Console.ReadLine());

        Console.WriteLine($"CalcExpr1({expr}) = {MyFrac.CalcExpr1(expr)}");
        Console.WriteLine($"CalcExpr2({expr}) = {MyFrac.CalcExpr2(expr)}");
    }
}
