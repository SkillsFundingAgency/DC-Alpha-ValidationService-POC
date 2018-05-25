using BusinessRules.POC.Interfaces;
using DCT.ILR.Model;
using System;

namespace BusinessRules.POC.SharedRules
{
    public class LearnerDoBShouldNotBeNullRule : ISharedRule<MessageLearner, bool>
    {
        public LearnerDoBShouldNotBeNullRule()
        {
        }

        public bool Evaluate(MessageLearner objectToValidate)
        {
            return objectToValidate.DateOfBirth == null ? true : false;
        }
    }
}
