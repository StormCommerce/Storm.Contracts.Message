using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enferno.Services.Contracts.Tests
{
    [TestClass]
    public class ErrorMessageV2Tests
    {
        [TestMethod, TestCategory("UnitTest")]
        public void BasicMessageAsADictionaryTest()
        {
            // Arrange
            var em = new ErrorMessage_v2(new ApplicationException("Test"));

            // Act
            em.Messages.Add("FaultCode", "4711");

            // Assert
            Assert.AreEqual("4711", em.Messages["FaultCode"]);
        }
    }
}
