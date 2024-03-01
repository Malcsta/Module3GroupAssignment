using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ADEV.ACE.RRC.HumanResources;

namespace HumanResources.Testing
{
    [TestClass]
    public class HourlyEmployeeTests
    {
        /*
        * HourlyEmployee() Constructor
        */


        [TestMethod]
        public void Constructor_NameWithNonWhitespaceCharacters_ThrowsArgumentException()
        {
            //Arrange
            string name = " ";

            //Act
            ArgumentException exception =
                 Assert.ThrowsException<ArgumentException>(
                     () => new HourlyEmployee(name, 22, 18)
                     );

            //Assert
            Assert.AreEqual("name", exception.ParamName);
            Assert.AreEqual("The argument must contain at least one non-whitespace character.", GetExceptionMessage(exception.Message));
        }

        [TestMethod]
        public void Constructor_HoursWorkedLessThanZero_ThrowsArgumentOutOfRangeException()
        {
            //Arrange
            int hoursWorked = -12;


            //Act
            ArgumentOutOfRangeException exception =
                Assert.ThrowsException<ArgumentOutOfRangeException>(
                    () => new HourlyEmployee("Malcolm", hoursWorked, 27)
                );


            //Assert
            Assert.AreEqual("hoursWorked", exception.ParamName);
            Assert.AreEqual("The argument value must between zero and 100 (inclusive).", GetExceptionMessage(exception.Message));
        }

        [TestMethod]
        public void Constructor_HoursWorkedGreaterThanOneHundred_ThrowsArgumentOutOfRangeException()
        {
            //Arrange
            int hoursWorked = 101;

            //Act
            ArgumentOutOfRangeException exception =
                Assert.ThrowsException<ArgumentOutOfRangeException>(
                    () => new HourlyEmployee("Danny", hoursWorked, 26)
                );

            //Assert
            Assert.AreEqual("hoursWorked", exception.ParamName);
            Assert.AreEqual("The argument value must between zero and 100 (inclusive).", GetExceptionMessage(exception.Message));
        }

        [TestMethod]
        public void Constructor_RateOfPayLessThanZero_ThrowsArgumentOutOfRangeException()
        {
            //Arrange
            decimal rateOfPay = -4;

            //Act
            ArgumentOutOfRangeException exception =
                Assert.ThrowsException<ArgumentOutOfRangeException>(
                    () => new HourlyEmployee("Alex", 22, rateOfPay)
                    );

            //Assert
            Assert.AreEqual("rateOfPay", exception.ParamName);
            Assert.AreEqual("The argument value must be zero or greater.", GetExceptionMessage(exception.Message));

        }

        [TestMethod]
        public void Constructor_ValidArgumentValues_InitializesState()
        {
            //Arrange
            string name = "Winston";
            double hoursWorked = 40;
            decimal rateOfPay = 40;

            //Act
            HourlyEmployee testEmployee = new HourlyEmployee(name, hoursWorked, rateOfPay);

            //Assert
            PrivateObject target;

            target = new PrivateObject(testEmployee);

            double actualHoursWorked = (double)target.GetField("hoursWorked");
            decimal actualRateOfPay = (decimal)target.GetField("rateOfPay");

            PrivateType privateType;

            privateType = new PrivateType(typeof(Employee));

            target = new PrivateObject(testEmployee, privateType);

            string actualName = (string)target.GetField("name");

            Assert.AreEqual(name, actualName);
            Assert.AreEqual(hoursWorked,actualHoursWorked);
            Assert.AreEqual(rateOfPay, actualRateOfPay);

        }

        /*
        * Name Property tests
        */

        [TestMethod]
        public void SetName_NoNonWhitespaceCharacters_ThrowsArgumentException()
        {
            //Arrange
            string employeeName = "Peter";
            HourlyEmployee testEmployee = new HourlyEmployee(employeeName, 40, 40);

            //Act
            ArgumentException exception =
                Assert.ThrowsException<ArgumentException>(
                    () => testEmployee.Name = " "
                );

            //Assert
            Assert.AreEqual("value", exception.ParamName);
            Assert.AreEqual("The value must contain at least one non-whitespace character.",
                GetExceptionMessage(exception.Message));

            //Ensure state has not changed
            PrivateObject target = new PrivateObject(testEmployee, new PrivateType(typeof(Employee)));
            string actual = (string)target.GetField("name");
            Assert.AreEqual(employeeName, actual);
        }

        [TestMethod]
        public void SetRateOfPay_ValueLessThanZero_ThrowsArgumentOutOfRangeException()
        {
            //Arrange
            HourlyEmployee testEmployee = new HourlyEmployee("Michael", 40, 40);

            //Act
            ArgumentOutOfRangeException exception =
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => testEmployee.RateOfPay = -10m
                );

            //Assert
            Assert.AreEqual("value", exception.ParamName);
            Assert.AreEqual("The value must be zero or greater.", GetExceptionMessage(exception.Message));
        }

        [TestMethod]
        public void SetRateOfPay_ValidValue_ChangeState()
        {
            // Arrange
            decimal rateOfPay = 10;

            HourlyEmployee testEmployee = new HourlyEmployee("Alpha", 90, 100);

            rateOfPay = 20;

            // Act
            testEmployee.RateOfPay = rateOfPay;

            // Assert
            PrivateObject target =
                new PrivateObject(testEmployee, new PrivateType(typeof(HourlyEmployee)));

            decimal actual = (decimal)target.GetField("rateOfPay");

            Assert.AreEqual(rateOfPay, actual);
        }


        #region Helper Methods

        /// <summary>
        /// Helper method to obtain only the message from an Exception object.
        /// </summary>
        /// <param name="exceptionMessage">The Exception's Message state.</param>
        /// <returns>The Exception's message with the parameter omitted.</returns>
        /// <remarks>
        /// The Exception.Message property returns the Exception's message on line 1 and
        /// the parameter name on line 2. This method reads the first line and returns
        /// the message.
        /// </remarks>
        private string GetExceptionMessage(string exceptionMessage)
        {
            return new System.IO.StringReader(exceptionMessage).ReadLine();
        }

        #endregion
    }
}
