using System;

class MyFrac
{
    // Поля для збереження чисельника та знаменника дробу
    private long nom;
    private long denom;

    // Властивості для доступу до чисельника та знаменника
    public long Numerator => nom;
    public long Denominator => denom;

    // Конструктор, який приймає чисельник і знаменник, та спрощує дріб
    public MyFrac(long numerator, long denominator)
    {
        if (denominator == 0) // Перевірка, щоб знаменник не дорівнював нулю
        {
            throw new ArgumentException("Знаменник не може бути рівний нулю.");
        }

        // Знаходження НОД для скорочення дробу
        long gcd = GCD(Math.Abs(numerator), Math.Abs(denominator));
        nom = numerator / gcd;
        denom = denominator / gcd;

        // Якщо знаменник від’ємний, змінюємо знак чисельника та знаменника
        if (denom < 0)
        {
            nom = -nom;
            denom = -denom;
        }
    }

    // Метод для представлення дробу у вигляді рядка
    public override string ToString()
    {
        return denom == 1 ? $"{nom}" : $"{nom}/{denom}";
    }

    // Метод для представлення дробу у вигляді з цілою частиною
    public string ToStringWithIntPart()
    {
        if (nom == 0) // Якщо чисельник 0, повертаємо "0"
        {
            return "0";
        }

        long intPart = nom / denom; // Ціла частина
        long newNom = Math.Abs(nom % denom); // Залишок для дробової частини

        if (newNom == 0) // Якщо залишок 0, повертаємо лише цілу частину
        {
            return $"{intPart}";
        }
        else if (intPart != 0) // Якщо є і ціла, і дробова частини
        {
            return $"{intPart}|{newNom}/{denom}";
        }
        else // Якщо цілої частини немає
        {
            return $"{newNom}/{denom}";
        }
    }

    // Метод для перетворення дробу у число з плаваючою точкою
    public double ToDouble()
    {
        return (double)nom / denom;
    }

    // Метод для додавання двох дробів
    public MyFrac Add(MyFrac other)
    {
        long newNom = nom * other.denom + denom * other.nom; // Новий чисельник
        long newDenom = denom * other.denom; // Новий знаменник
        return new MyFrac(newNom, newDenom); // Повертаємо новий об’єкт MyFrac
    }

    // Метод для віднімання двох дробів
    public MyFrac Subtract(MyFrac other)
    {
        long newNom = nom * other.denom - denom * other.nom;
        long newDenom = denom * other.denom;
        return new MyFrac(newNom, newDenom);
    }

    // Метод для множення двох дробів
    public MyFrac Multiply(MyFrac other)
    {
        long newNom = nom * other.nom;
        long newDenom = denom * other.denom;
        return new MyFrac(newNom, newDenom);
    }

    // Метод для ділення двох дробів
    public MyFrac Divide(MyFrac other)
    {
        if (other.nom == 0) // Перевірка, чи знаменник не дорівнює нулю
        {
            throw new DivideByZeroException("Не можна ділити на дріб з нульовим чисельником.");
        }

        long newNom = nom * other.denom;
        long newDenom = denom * other.nom;
        return new MyFrac(newNom, newDenom);
    }

    // Обчислення першого виразу: сума дробів 1 / (i * (i + 1)), де i = 1..n
    public static MyFrac CalcExpr1(int n)
    {
        MyFrac result = new MyFrac(0, 1);
        for (int i = 1; i <= n; i++)
        {
            MyFrac add = new MyFrac(1, i * (i + 1));
            result = result.Add(add); // Додаємо поточний дріб
        }
        return result;
    }

    // Обчислення другого виразу: добуток виразів (1/i^2 - 1/i^2), що завжди дорівнює нулю
    public static MyFrac CalcExpr2(int n)
    {
        MyFrac result = new MyFrac(1, 1);
        for (int i = 2; i <= n; i++)
        {
            MyFrac term = new MyFrac(1, i * i);
            result = result.Multiply(term.Subtract(term)); // Підрахунок добутку, що в результаті дорівнює нулю
        }
        return result;
    }

    // Метод для обчислення НОД двох чисел
    private static long GCD(long a, long b)
    {
        while (b != 0)
        {
            long temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }
}
