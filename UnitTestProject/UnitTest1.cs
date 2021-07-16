using System;
using CalcLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CalculateTestMethod1()
        {
            String[] a = Calc.GetOperands("23+4,5");
            Assert.AreEqual("23", a[0]);
            Assert.AreEqual("4,5", a[1]);
        }

        [TestMethod]
        public void CalculateTestMethod2()
        {
            String a = Calc.GetOperation("23+4,5");
            Assert.AreEqual("+", a);
        }

        [TestMethod]
        public void ResultTestMethod()
        {
            Assert.AreEqual("27,5", Calc.DoubleOperation["+"](23, 4.5).ToString());
            string result = Calc.DoOperation("23+4,5");
            Assert.AreEqual("27,5", result);
        }

    }
}
