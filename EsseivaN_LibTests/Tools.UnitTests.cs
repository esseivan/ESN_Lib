using EsseivaN.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace EsseivaN.UnitTests
{
    [TestClass]
    public class ToolsTests
    {
        [TestMethod]
        public void DecimalToEngineer_ValidDefault_Valid()
        {
            // Arrange
            double dec = 36689.8774;

            // Act
            string engineer = Tools.Tools.DecimalToEngineer(dec);

            // Assert 
            Assert.AreEqual(engineer, "36,69k");
        }

        [TestMethod]
        public void DecimalToEngineer_ValidCustom_Valid()
        {
            // Arrange
            double dec = 36689.8774;

            // Act
            string engineer = Tools.Tools.DecimalToEngineer(dec, 6);

            // Assert 
            Assert.AreEqual(engineer, "36,689877k");
        }

        [TestMethod]
        public void DecimalToEngineer_Invalid_Null()
        {
            // Arrange
            double dec = double.NaN;

            // Act
            string engineer = Tools.Tools.DecimalToEngineer(dec);

            // Assert 
            Assert.AreEqual(engineer, null);
        }

        [TestMethod]
        public void EngineerToDecimal_Valid_Valid()
        {
            // Arrange
            string engineer = "36,689k";

            // Act
            double dec = Tools.Tools.EngineerToDecimal(engineer);
            
            // Assert
            Assert.AreEqual(dec, 36689);
        }

        [TestMethod]
        public void EngineerToDecimal_Invalid_NaN()
        {
            // Arrange
            string engineer = "36,689ke";

            // Act
            double dec = Tools.Tools.EngineerToDecimal(engineer);

            // Assert
            Assert.AreEqual(dec, double.NaN);
        }

        [TestMethod]
        public void GetErrorPercent_110And100_10Percent()
        {
            // Arrange
            double value = 110;
            double real = 100;

            // Act
            double error = Tools.Tools.GetErrorPercent(value, real);

            // Assert
            Assert.AreEqual(error, 10);
        }

        [TestMethod]
        public void GetErrorPercent_90And100_Minus10Percent()
        {
            // Arrange
            double value = 90;
            double real = 100;

            // Act
            double error = Tools.Tools.GetErrorPercent(value, real);

            // Assert
            Assert.AreEqual(error, -10);
        }

        [TestMethod]
        public void GetErrorPercent_Invalid_NaN()
        {
            // Arrange
            double value = 90;
            double real = 0;

            // Act
            double error = Tools.Tools.GetErrorPercent(value, real);

            // Assert
            Assert.AreEqual(error, double.NaN);
        }
    }
}
