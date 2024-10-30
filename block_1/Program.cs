using System;

namespace Matrix
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                MyMatrix matrix1 = GetMatrixFromUser("першої");
                MyMatrix matrix2 = GetMatrixFromUser("другої");

                // Вибір операції
                Console.WriteLine("\nОберіть операцію:");
                Console.WriteLine("1. Додавання матриць");
                Console.WriteLine("2. Множення матриць");
                Console.WriteLine("3. Транспонування першої матриці (створити копію)");
                Console.WriteLine("4. Транспонування першої матриці (змінити поточну)");
                Console.WriteLine("5. Обчислення детермінанта першої матриці");
                Console.WriteLine("6. Обчислення детермінанта другої матриці");
                Console.Write("Ваш вибір: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        if (matrix1.Height == matrix2.Height && matrix1.Width == matrix2.Width)
                        {
                            MyMatrix sumMatrix = matrix1 + matrix2;
                            Console.WriteLine("\nРезультат додавання матриць:");
                            Console.WriteLine(sumMatrix);
                        }
                        else
                        {
                            Console.WriteLine("Матриці мають бути однакових розмірів для додавання.");
                        }
                        break;

                    case 2:
                        if (matrix1.Width == matrix2.Height)
                        {
                            MyMatrix productMatrix = matrix1 * matrix2;
                            Console.WriteLine("\nРезультат множення матриць:");
                            Console.WriteLine(productMatrix);
                        }
                        else
                        {
                            Console.WriteLine("Кількість стовпців першої матриці має дорівнювати кількості рядків другої матриці.");
                        }
                        break;

                    case 3:
                        MyMatrix transposedMatrix = matrix1.GetTransponedCopy();
                        Console.WriteLine("\nТранспонована перша матриця (копія):");
                        Console.WriteLine(transposedMatrix);
                        break;

                    case 4:
                        matrix1.TransponeMe();
                        Console.WriteLine("\nПерша матриця була транспонована (модифікація на місці):");
                        Console.WriteLine(matrix1);
                        break;

                    case 5:
                        if (matrix1.Height == matrix1.Width)
                        {
                            double determinant = matrix1.CalcDeterminant();
                            Console.WriteLine($"\nДетермінант першої матриці: {determinant}");
                        }
                        else
                        {
                            Console.WriteLine("Детермінант можна обчислити тільки для квадратної матриці.");
                        }
                        break;

                    case 6:
                        if (matrix2.Height == matrix2.Width)
                        {
                            double determinant = matrix2.CalcDeterminant();
                            Console.WriteLine($"\nДетермінант другої матриці: {determinant}");
                        }
                        else
                        {
                            Console.WriteLine("Детермінант можна обчислити тільки для квадратної матриці.");
                        }
                        break;

                    default:
                        Console.WriteLine("Невірний вибір операції.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }

       static MyMatrix GetMatrixFromUser(string matrixName)
        {
            Console.WriteLine($"\nВведіть спосіб заповнення {matrixName} матриці:");
            Console.WriteLine("1. Ввести розміри і елементи вручну");
            Console.WriteLine("2. Задати з рядка");
            Console.WriteLine("3. Задати з зубчастого масиву");
            Console.WriteLine("4. Задати з масиву рядків");
            Console.WriteLine("5. Згенерувати випадкову матрицю");
            Console.Write("Ваш вибір: ");
            int inputMethod = int.Parse(Console.ReadLine());

            switch (inputMethod)
            {
                case 1:
                    Console.WriteLine($"Введіть розміри {matrixName} матриці (рядки і стовпчики через enter):");
                    Console.Write("Кількість рядків: ");
                    int rows = int.Parse(Console.ReadLine());
                    Console.Write("Кількість стовпців: ");
                    int cols = int.Parse(Console.ReadLine());
                    Console.WriteLine($"Введіть елементи {matrixName} матриці (кожен рядок через пробіл):");
                    return MyMatrix.InputMatrixData(rows, cols);

                case 2:
                    Console.WriteLine($"Введіть {matrixName} матрицю у вигляді рядка (рядки розділені '\\n'):");
                    string matrixString = Console.ReadLine();
                    return new MyMatrix(matrixString);

                case 3:
                    Console.WriteLine($"Введіть кількість рядків {matrixName} матриці:");
                    Console.Write("Кількість рядків: ");
                    int jaggedRows = int.Parse(Console.ReadLine());
                    double[][] jaggedArray = new double[jaggedRows][];

                    for (int i = 0; i < jaggedRows; i++)
                    {
                        Console.WriteLine($"Введіть елементи {i + 1}-го рядка через пробіл:");
                        string[] rowValues = Console.ReadLine().Split(' ');
                        jaggedArray[i] = Array.ConvertAll(rowValues, double.Parse);
                    }

                    return new MyMatrix(jaggedArray);

                case 4:
                    Console.WriteLine($"Введіть кількість рядків {matrixName} матриці:");
                    Console.Write("Кількість рядків: ");
                    int strArrayRows = int.Parse(Console.ReadLine());
                    string[] stringRows = new string[strArrayRows];

                    for (int i = 0; i < strArrayRows; i++)
                    {
                        Console.WriteLine($"Введіть рядок {i + 1}:");
                        stringRows[i] = Console.ReadLine();
                    }

                    return new MyMatrix(stringRows);

                case 5:
                    Console.WriteLine($"Введіть розміри випадкової {matrixName} матриці (рядки і стовпчики через enter):");
                    Console.Write("Кількість рядків: ");
                    int randomRows = int.Parse(Console.ReadLine());
                    Console.Write("Кількість стовпців: ");
                    int randomCols = int.Parse(Console.ReadLine());
                    return MyMatrix.GenerateRandomMatrix(randomRows, randomCols);

                default:
                    throw new ArgumentException("Невірний вибір методу введення.");
            }
        }
    }
        
}
