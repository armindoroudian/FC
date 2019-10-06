using System;
using FINRA.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace FINRA.UnitTests
{
    [TestClass]
    public class CalculationTests
    {
        [TestMethod]
        public void CalculateTotalPossibilities_PhoneIs2404376186_Return_81920()
        {
            //for PhopneNumber 2404376186 possible permutations is 81,920

            //Arrange
            var result = new Result();
            result.Request = new PermutationRequest
            {
                Number = "2404376186"
            };
            result.CalculateTotalPossibilities();

            //Act
            var ResultIsOk = result.PossiblePermutations == 81920;

            //Assert
            Assert.IsTrue(ResultIsOk);
        }
        [TestMethod]
        public void CalculateActualPermutations_PhoneIs2404376188_Return_81920()
        {
            //for PhopneNumber 2404376186 possible permutations is 81,920

            //Arrange
            var result = new Result();
            result.Request = new PermutationRequest
            {
                Number = "2404376188"
            };
            result.CalculateTotalPossibilities();

            //Act
            var ResultIsOk = result.PossiblePermutations == 81920;

            //Assert
            Assert.IsTrue(ResultIsOk);
        }


    }
}
