using System;
using FINRA.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FINRA.UnitTests
{
    [TestClass]
    public class NumberValidationTests
    {
        [TestMethod]
        public void PhoneNumberIsValid_PhoneIsValied_ReturnTrue()
        {
            //Arrange
            var result = new Result();
            result.Request = new PermutationRequest
            {
                Number = "2404376186"
            };
            //Act
            var Valid = result.IsValid;
            //Assert
            Assert.IsTrue(Valid);
        }
        [TestMethod]
        public void PhoneNumberIsValid_PhoneIsNotValied_ReturnShortNumberExplanation()
        {
            //Arrange
            var result = new Result();
            result.Request = new PermutationRequest
            {
                Number = "2404"
            };
            //Act
            result.Request.Number = "240";
            var ValidationExplanation = result.WhyIsNotValid() == "Phone Number can be 10 or 7 digits.";

            //Assert
            Assert.IsTrue(ValidationExplanation);
        }
        [TestMethod]
        public void PhoneNumberIsValid_PhoneIsNotValied_ReturnNotAllDigitErrorExplanation()
        {
            //Arrange
            var result = new Result();
            result.Request = new PermutationRequest
            {
                Number = "abc"
            };
            //Act
            var ValidationExplanation = result.WhyIsNotValid() == "Phone Number should only contain digits.";

            //Assert
            Assert.IsTrue(ValidationExplanation);
        }
        [TestMethod]
        public void PhoneNumberIsValid_PhoneIsNotValied_ReturnStartWithZeroErrorExplanation()
        {
            //Arrange
            var result = new Result();
            result.Request = new PermutationRequest
            {
                Number = "0244376186"
            };
            //Act
            var ValidationExplanation = result.WhyIsNotValid() == "Phone Number can not start with '0'";

            //Assert
            Assert.IsTrue(ValidationExplanation);
        }
    }



}
