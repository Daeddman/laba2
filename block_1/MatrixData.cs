using System;

namespace Matrix
{
    public partial class MyMatrix
    {
        // Поле для зберігання елементів матриці
        private double[,] _matrixData;

        // Конструктор, що приймає кількість рядків і стовпців
        public MyMatrix(int height, int width)
        {
            if (height <= 0 || width <= 0)
                throw new ArgumentException("Розмір матриці має бути більше за нуль.");

            _matrixData = new double[height, width];
        }

        // Конструктор з двовимірного масиву
        public MyMatrix(double[,] array)
        {
            // Копіюємо значення масиву, щоб уникнути зміни оригіналу
            _matrixData = (double[,])array.Clone();
        }

        // Конструктор з "зубчастого" масиву
        public MyMatrix(double[][] jaggedArray)
        {
            // Перевірка, чи масив є прямокутним
            if (!IsRectangular(jaggedArray))
                throw new ArgumentException("Масив не є прямокутним.");

            _matrixData = new double[jaggedArray.Length, jaggedArray[0].Length];
            for (int i = 0; i < jaggedArray.Length; i++)
            {
                for (int j = 0; j < jaggedArray[i].Length; j++)
                {
                    _matrixData[i, j] = jaggedArray[i][j];
                }
            }
        }

        // Конструктор з масиву рядків
        public MyMatrix(string[] rows)
        {
            var rowCount = rows.Length;
            // Визначаємо кількість стовпців з першого рядка
            var colCount = rows[0].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;

            _matrixData = new double[rowCount, colCount];
            for (int i = 0; i < rowCount; i++)
            {
                var values = rows[i].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (values.Length != colCount)
                    throw new ArgumentException("Масив рядків не є прямокутним.");

                for (int j = 0; j < colCount; j++)
                {
                    // Конвертуємо кожен елемент рядка у число
                    if (!double.TryParse(values[j], out _matrixData[i, j]))
                        throw new ArgumentException("Невірний формат числа.");
                }
            }
        }
        
        // Метод для генерації матриці зі випадковими числами
        public static MyMatrix GenerateRandomMatrix(int height, int width)
        {
            if (height <= 0 || width <= 0)
                throw new ArgumentException("Розмір матриці має бути більше за нуль.");

            var matrix = new MyMatrix(height, width);
            Random random = new Random();

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    // Генерація чисел від 0 до 10 з округленням до одного знака після коми
                    matrix[i, j] = Math.Round(random.NextDouble() * 10, 1);
                }
            }

            Console.WriteLine("\nЗгенерована матриця:");
            Console.WriteLine(matrix);
            return matrix;
        }

        // Конструктор з рядка
        public MyMatrix(string matrixString)
        {
            var rows = matrixString.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var colCount = rows[0].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;

            _matrixData = new double[rows.Length, colCount];
            for (int i = 0; i < rows.Length; i++)
            {
                var values = rows[i].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (values.Length != colCount)
                    throw new ArgumentException("Невірний формат матриці.");

                for (int j = 0; j < colCount; j++)
                {
                    if (!double.TryParse(values[j], out _matrixData[i, j]))
                        throw new ArgumentException("Невірний формат числа.");
                }
            }
        }

        // Властивості Height і Width для отримання розмірів матриці
        public int Height => _matrixData.GetLength(0);
        public int Width => _matrixData.GetLength(1);

        // Додаткові методи для отримання розмірів
        public int GetHeight() => Height;
        public int GetWidth() => Width;

        // Індексатор для доступу до елементів матриці
        public double this[int row, int col]
        {
            get => _matrixData[row, col];
            set => _matrixData[row, col] = value;
        }

        // Альтернативні методи для отримання та зміни елементів
        public double GetElement(int row, int col) => _matrixData[row, col];
        public void SetElement(int row, int col, double value) => _matrixData[row, col] = value;

        // Метод для введення даних матриці вручну з консолі
        public static MyMatrix InputMatrixData(int rows, int cols)
        {
            var matrix = new MyMatrix(rows, cols);
            for (int i = 0; i < rows; i++)
            {
                Console.WriteLine($"Введіть елементи {i + 1}-го рядка через пробіл:");
                string[] rowValues = Console.ReadLine().Split(' ');
                if (rowValues.Length != cols)
                {
                    throw new ArgumentException("Кількість елементів у рядку не відповідає кількості стовпців.");
                }

                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = double.Parse(rowValues[j]);
                }
            }
            return matrix;
        }

        // Перевизначений метод ToString() для форматованого виводу матриці
        public override string ToString()
        {
            int maxLength = 0;

            // Знаходимо максимальну довжину чисел для вирівнювання
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    int length = _matrixData[i, j].ToString("0.0").Length;
                    if (length > maxLength)
                        maxLength = length;
                }
            }

            // Формуємо рядки матриці з вирівняним форматуванням
            var result = "";
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    result += Math.Round(_matrixData[i, j], 2).ToString().PadLeft(maxLength) + "\t";
                }
                result = result.TrimEnd() + "\n";
            }
            return result.TrimEnd();
        }

        // Метод для перевірки, чи є масив прямокутним
        private bool IsRectangular(double[][] jaggedArray)
        {
            var rowLength = jaggedArray[0].Length;
            foreach (var row in jaggedArray)
            {
                if (row.Length != rowLength)
                    return false;
            }
            return true;
        }
    }
}
