namespace BusinessRules.POC.Interfaces
{
    public struct LearnerValidationError
    {
        public LearnerValidationError(string learnerRefernenceNumber, string errorName)
        {
            LearnerReferenceNumber = learnerRefernenceNumber;
            ErrorName = errorName;
        }

        public string LearnerReferenceNumber;

        public string ErrorName;
    }
}
