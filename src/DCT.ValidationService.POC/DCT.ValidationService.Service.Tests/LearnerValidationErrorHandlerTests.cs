using BusinessRules.POC.Models;
using DCT.ILR.Model;
using DCT.ValidationService.Service.Implementation;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DCT.ValidationService.Service.Tests
{
    public class LearnerValidationErrorHandlerTests
    {
        [Fact]
        public void HandleError()
        {
            var learnerValidationErrorHandler = new LearnerValidationErrorHandler();

            learnerValidationErrorHandler.Handle(new MessageLearner() { LearnRefNumber = "Test" }, "TestErrorName");

            learnerValidationErrorHandler.ErrorBag.Should().HaveCount(1);
        }

        [Fact]
        public void HandleErrorParallel()
        {
            var learnerValidationErrorHandler = new LearnerValidationErrorHandler();

            Parallel.For(0, 1000, (i) =>
            {
                learnerValidationErrorHandler.Handle(new MessageLearner() { LearnRefNumber = i.ToString() }, "TestErrorName");
            });

            for (int i = 0; i< 1000; i++)
            {
                learnerValidationErrorHandler.ErrorBag.Should().ContainSingle(eb => eb.LearnerReferenceNumber == i.ToString());
            }
        }
    }
}
