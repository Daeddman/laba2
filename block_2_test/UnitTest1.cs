using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

[TestClass]
public class MyFracTests
{
    [TestMethod]
    public void Constructor_SimplifiesFraction()
    {
        var frac = new MyFrac(4, 8);
        Assert.AreEqual(1, frac.Numerator);
        Assert.AreEqual(2, frac.Denominator);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Constructor_ZeroDenominator_ThrowsException()
    {
        var frac = new MyFrac(1, 0);
    }

    [TestMethod]
    public void ToString_ReturnsCorrectString()
    {
        var frac1 = new MyFrac(4, 2);
        var frac2 = new MyFrac(3, 4);
        Assert.AreEqual("2", frac1.ToString());
        Assert.AreEqual("3/4", frac2.ToString());
    }

    [TestMethod]
    public void ToStringWithIntPart_ReturnsCorrectString()
    {
        var frac1 = new MyFrac(5, 2);
        var frac2 = new MyFrac(3, 4);
        Assert.AreEqual("2|1/2", frac1.ToStringWithIntPart());
        Assert.AreEqual("3/4", frac2.ToStringWithIntPart());
    }

    [TestMethod]
    public void ToDouble_ReturnsCorrectValue()
    {
        var frac = new MyFrac(1, 2);
        Assert.AreEqual(0.5, frac.ToDouble(), 0.0001);
    }

    [TestMethod]
    public void Add_AddsFractionsCorrectly()
    {
        var frac1 = new MyFrac(1, 2);
        var frac2 = new MyFrac(1, 3);
        var result = frac1.Add(frac2);
        Assert.AreEqual(5, result.Numerator);
        Assert.AreEqual(6, result.Denominator);
    }

    [TestMethod]
    public void Subtract_SubtractsFractionsCorrectly()
    {
        var frac1 = new MyFrac(3, 4);
        var frac2 = new MyFrac(1, 4);
        var result = frac1.Subtract(frac2);
        Assert.AreEqual(1, result.Numerator);
        Assert.AreEqual(2, result.Denominator);
    }

    [TestMethod]
    public void Multiply_MultipliesFractionsCorrectly()
    {
        var frac1 = new MyFrac(1, 2);
        var frac2 = new MyFrac(2, 3);
        var result = frac1.Multiply(frac2);
        Assert.AreEqual(1, result.Numerator);
        Assert.AreEqual(3, result.Denominator);
    }

    [TestMethod]
    public void Divide_DividesFractionsCorrectly()
    {
        var frac1 = new MyFrac(1, 2);
        var frac2 = new MyFrac(1, 4);
        var result = frac1.Divide(frac2);
        Assert.AreEqual(2, result.Numerator);
        Assert.AreEqual(1, result.Denominator);
    }

    [TestMethod]
    [ExpectedException(typeof(DivideByZeroException))]
    public void Divide_ByZeroFraction_ThrowsException()
    {
        var frac1 = new MyFrac(1, 2);
        var frac2 = new MyFrac(0, 1);
        var result = frac1.Divide(frac2);
    }

    [TestMethod]
    public void CalcExpr1_ReturnsCorrectFraction()
    {
        MyFrac expected = new MyFrac(5, 6);     
        MyFrac actual = MyFrac.CalcExpr1(5);
        Assert.AreEqual(expected.Numerator, actual.Numerator);
        Assert.AreEqual(expected.Denominator, actual.Denominator);
    }


    [TestMethod]
    public void CalcExpr2_ReturnsZero()
    {
        var result = MyFrac.CalcExpr2(3); // (1/4 - 1/4) * (1/9 - 1/9)...
        Assert.AreEqual(0, result.Numerator);
        Assert.AreEqual(1, result.Denominator);
    }
}
