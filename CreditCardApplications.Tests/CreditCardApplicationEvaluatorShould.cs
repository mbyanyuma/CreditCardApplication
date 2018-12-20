using System;
using Xunit;

namespace CreditCardApplications.Tests
{
    public class CreditCardApplicationEvaluatorShould
    {
        [Fact]
        public void AcceptHighIncomeApplications()
        {
            //sut means system under test
            var sut = new CreditCardApplicationEvaluator(null);

            var creditCardApplication = new CreditCardApplication {GrossAnnualIncome = 100_000};

            CreditCardApplicationDecision decision = sut.Evaluate(creditCardApplication);

            Assert.Equal(CreditCardApplicationDecision.AutoAccepted, decision);
        }

        [Fact]
        public void ReferYoungApplications()
        {
            var sut = new CreditCardApplicationEvaluator(null);

            var creditCardApplication = new CreditCardApplication {Age = 19};

            CreditCardApplicationDecision decision = sut.Evaluate(creditCardApplication);

            Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision);
        }

        //other evaluator test conditions
    }
}
