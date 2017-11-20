using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Implementation.Utility;

namespace Implementation.UnitTest.Utility
{
    [TestClass]
    public class AgeCalculatorTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Should_Throw_ArgumentException_When_Reference_Is_Before_DateOfBirth()
        {
            AgeCalculator.Calculate(DateTime.MaxValue, DateTime.MinValue);
        }

        [TestMethod]
        public void Should_Return_31_One_Month_Before_DateOfBirth()
        {
            int age = AgeCalculator.Calculate(new DateTime(1978, 9, 27), new DateTime(2010, 8, 27));
            Assert.AreEqual(31, age);
        }

        [TestMethod]
        public void Should_Return_31_One_Day_Before_DateOfBirth()
        {
            int age = AgeCalculator.Calculate(new DateTime(1978, 9, 27), new DateTime(2010, 9, 26));
            Assert.AreEqual(31, age);
        }

        [TestMethod]
        public void Should_Return_32_On_DateOfBirth()
        {
            int age = AgeCalculator.Calculate(new DateTime(1978, 9, 27), new DateTime(2010, 9, 27));
            Assert.AreEqual(32, age);
        }

        [TestMethod]
        public void Should_Return_32_One_Month_Past_DateOfBirth()
        {
            int age = AgeCalculator.Calculate(new DateTime(1978, 9, 27), new DateTime(2010, 10, 27));
            Assert.AreEqual(32, age);
        }
    }
}
