using Microsoft.VisualStudio.TestTools.UnitTesting;
using Matrix;

namespace MatrixTests
{
    [TestClass]
    public class MyMatrixTests
    {
        [TestMethod]
        public void Constructor_HeightWidth_CreatesMatrixWithCorrectSize()
        {
            var matrix = new MyMatrix(3, 4);
            Assert.AreEqual(3, matrix.Height);
            Assert.AreEqual(4, matrix.Width);
        }

        [TestMethod]
        public void Constructor_From2DArray_CopiesArrayValues()
        {
            double[,] array = { { 1, 2 }, { 3, 4 } };
            var matrix = new MyMatrix(array);

            Assert.AreEqual(1, matrix[0, 0]);
            Assert.AreEqual(2, matrix[0, 1]);
            Assert.AreEqual(3, matrix[1, 0]);
            Assert.AreEqual(4, matrix[1, 1]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_FromJaggedArray_ThrowsForNonRectangularArray()
        {
            double[][] jaggedArray = { new double[] { 1, 2 }, new double[] { 3 } };
            var matrix = new MyMatrix(jaggedArray);
        }

        [TestMethod]
        public void AddOperator_AddsTwoMatricesCorrectly()
        {
            double[,] array1 = { { 1, 2 }, { 3, 4 } };
            double[,] array2 = { { 5, 6 }, { 7, 8 } };
            var m1 = new MyMatrix(array1);
            var m2 = new MyMatrix(array2);

            var result = m1 + m2;

            Assert.AreEqual(6, result[0, 0]);
            Assert.AreEqual(8, result[0, 1]);
            Assert.AreEqual(10, result[1, 0]);
            Assert.AreEqual(12, result[1, 1]);
        }

        [TestMethod]
        public void MultiplyOperator_MultipliesTwoMatricesCorrectly()
        {
            double[,] array1 = { { 1, 2 }, { 3, 4 } };
            double[,] array2 = { { 5, 6 }, { 7, 8 } };
            var m1 = new MyMatrix(array1);
            var m2 = new MyMatrix(array2);

            var result = m1 * m2;

            Assert.AreEqual(19, result[0, 0]);
            Assert.AreEqual(22, result[0, 1]);
            Assert.AreEqual(43, result[1, 0]);
            Assert.AreEqual(50, result[1, 1]);
        }

        [TestMethod]
        public void TransponeMe_TransposesMatrixCorrectly()
        {
            double[,] array = { { 1, 2, 3 }, { 4, 5, 6 } };
            var matrix = new MyMatrix(array);

            matrix.TransponeMe();

            Assert.AreEqual(1, matrix[0, 0]);
            Assert.AreEqual(4, matrix[0, 1]);
            Assert.AreEqual(2, matrix[1, 0]);
            Assert.AreEqual(5, matrix[1, 1]);
            Assert.AreEqual(3, matrix[2, 0]);
            Assert.AreEqual(6, matrix[2, 1]);
        }

        [TestMethod]
        public void CalcDeterminant_CalculatesDeterminantCorrectly()
        {
            double[,] array = { { 1, 2 }, { 3, 4 } };
            var matrix = new MyMatrix(array);

            double determinant = matrix.CalcDeterminant();

            Assert.AreEqual(-2, determinant);
        }

        [TestMethod]
        public void ToString_ReturnsFormattedMatrixString()
        {
            double[,] array = { { 1.1, 2.2 }, { 3.3, 4.4 } };
            var matrix = new MyMatrix(array);
            string result = matrix.ToString();

            // Очищуємо форматування, щоб полегшити порівняння
            string expected = " 1.1\n 2.2\n 3.3\n 4.5";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GenerateRandomMatrix_CreatesMatrixWithRandomValues()
        {
            var matrix = MyMatrix.GenerateRandomMatrix(3, 3);

            Assert.AreEqual(3, matrix.Height);
            Assert.AreEqual(3, matrix.Width);
            foreach (var value in matrix.ToString().Split(new[] { '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries))
            {
                Assert.IsTrue(double.TryParse(value, out _));
            }
        }
    }
}
