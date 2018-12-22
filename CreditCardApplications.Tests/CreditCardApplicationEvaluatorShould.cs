using System;
using Xunit;
using Moq;

namespace CreditCardApplications.Tests
{
    public class CreditCardApplicationEvaluatorShould
    {
        [Fact]
        public void AcceptHighIncomeApplications()
        {
            Mock<IFrequentFlyerNumberValidator> mockValidator = new Mock<IFrequentFlyerNumberValidator>();

            //sut means system under test
            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);

            var creditCardApplication = new CreditCardApplication {GrossAnnualIncome = 100_000};

            CreditCardApplicationDecision decision = sut.Evaluate(creditCardApplication);

            Assert.Equal(CreditCardApplicationDecision.AutoAccepted, decision);
        }

        [Fact]
        public void ReferYoungApplications()
        {
            Mock<IFrequentFlyerNumberValidator> mockValidator = new Mock<IFrequentFlyerNumberValidator>();

            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);

            mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);

            var creditCardApplication = new CreditCardApplication {Age = 19};

            CreditCardApplicationDecision decision = sut.Evaluate(creditCardApplication);

            Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision);
        }

        [Fact]
        public void DeclineLowIncomeApplications()
        {
            Mock<IFrequentFlyerNumberValidator> mockValidator = new Mock<IFrequentFlyerNumberValidator>();

            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);

            mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);
            //mockValidator.Setup(x => x.IsValid(It.Is<string>((number => number.StartsWith('x'))))).Returns(true);
            //mockValidator.Setup(x => x.IsValid(It.IsIn("x", "y", "z"))).Returns(true);
            //mockValidator.Setup(x => x.IsValid(It.IsInRange("b", "z", Range.Inclusive))).Returns(true);
            //mockValidator.Setup(x => x.IsValid(It.IsRegex("[a-z]", System.Text.RegularExpressions.RegexOptions.None)))
            //    .Returns(true);


            var creditCardApplication = new CreditCardApplication()
            {
                GrossAnnualIncome = 17_000,
                Age = 42,
                //FrequentFlyerNumber = "108"
            };

          
            CreditCardApplicationDecision decision = sut.Evaluate(creditCardApplication);

            Assert.Equal(CreditCardApplicationDecision.AutoDeclined, decision);
        }

        [Fact]
        public void ReferInvalidFrequentFlyerApplications()
        {
            Mock<IFrequentFlyerNumberValidator> mockValidator = 
                new Mock<IFrequentFlyerNumberValidator>(MockBehavior.Strict);

            mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(false);      

            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);

            var application = new CreditCardApplication();

            CreditCardApplicationDecision decision = sut.Evaluate(application);

            Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision);
        }

        [Fact]
        public void DeclineLowIncomeApplicationsOutDemo()
        {
            Mock<IFrequentFlyerNumberValidator> mockValidator = new Mock<IFrequentFlyerNumberValidator>();

            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);

            bool isValid = true;

            mockValidator.Setup(x => x.IsValid(It.IsAny<string>(), out isValid));

            var creditCardApplication = new CreditCardApplication()
            {
                GrossAnnualIncome = 17_000,
                Age = 42         
            };

            CreditCardApplicationDecision decision = sut.EvaluateUsingOut(creditCardApplication);

            Assert.Equal(CreditCardApplicationDecision.AutoDeclined, decision);
        }




    }
}
