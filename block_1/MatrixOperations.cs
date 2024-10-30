namespace Matrix
{
    public partial class MyMatrix
    {
        
        // Оператор додавання двох матриць
        public static MyMatrix operator +(MyMatrix m1, MyMatrix m2)
        {
            if (m1.Height != m2.Height || m1.Width != m2.Width)
                throw new ArgumentException("Матриці повинні бути однакового розміру.");

            var result = new MyMatrix(m1.Height, m1.Width);
            for (int i = 0; i < m1.Height; i++)
            {
                for (int j = 0; j < m1.Width; j++)
                {
                    // Сума елементів двох матриць з округленням до двох знаків після коми
                    result[i, j] = Math.Round((m1[i, j] + m2[i, j]), 2);
                }
            }
            return result;
        }

        // Оператор множення двох матриць
        public static MyMatrix operator *(MyMatrix m1, MyMatrix m2)
        {
            if (m1.Width != m2.Height)
                throw new ArgumentException("Кількість стовпчиків першої матриці має дорівнювати кількості рядків другої матриці.");

            var result = new MyMatrix(m1.Height, m2.Width);
            for (int i = 0; i < result.Height; i++)
            {
                for (int j = 0; j < result.Width; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < m1.Width; k++)
                    {
                        sum += m1[i, k] * m2[k, j];
                    }
                    // Применяем округление один раз для каждой суммы
                    result[i, j] = Math.Round(sum, 2);
                }
            }

            return result;
        }


        // Метод для отримання транспонованого масиву матриці
        private double[,] GetTransponedArray()
        {
            var transposed = new double[Width, Height];
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    transposed[j, i] = _matrixData[i, j];
                }
            }
            return transposed;
        }

        // Повертає транспоновану копію поточної матриці
        public MyMatrix GetTransponedCopy()
        {
            return new MyMatrix(GetTransponedArray());
        }

        // Заміна поточної матриці на її транспоновану версію
        public void TransponeMe()
        {
            _matrixData = GetTransponedArray();
        }

        // Метод для обчислення детермінанта (визначника) матриці
        public double CalcDeterminant()
        {
            if (Height != Width)
                throw new InvalidOperationException("Детермінант можна обчислити тільки для квадратної матриці.");

            var matrixCopy = (double[,])_matrixData.Clone();
            return CalculateDeterminant(matrixCopy);
        }

        // Рекурсивний метод для обчислення детермінанта матриці
        private double CalculateDeterminant(double[,] matrix)
        {
            int size = matrix.GetLength(0);
            if (size == 1)
                return matrix[0, 0];

            double det = 0;
            for (int p = 0; p < size; p++)
            {
                double[,] subMatrix = CreateSubMatrix(matrix, 0, p);
                det += matrix[0, p] * CalculateDeterminant(subMatrix) * (p % 2 == 0 ? 1 : -1);
            }
            return det;
        }

        // Метод для створення матриці без заданого рядка та стовпця (використовується в розрахунку детермінанта)
        private double[,] CreateSubMatrix(double[,] matrix, int excludedRow, int excludedCol)
        {
            int size = matrix.GetLength(0);
            var result = new double[size - 1, size - 1];
            int rowOffset = 0, colOffset = 0;

            for (int i = 0; i < size; i++)
            {
                if (i == excludedRow)
                {
                    rowOffset = 1;
                    continue;
                }

                colOffset = 0;
                for (int j = 0; j < size; j++)
                {
                    if (j == excludedCol)
                    {
                        colOffset = 1;
                        continue;
                    }

                    result[i - rowOffset, j - colOffset] = matrix[i, j];
                }
            }
            return result;
        }
    }
}
