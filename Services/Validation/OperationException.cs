namespace CardStorage.Services.Validation
{
    public class OperationException
    {
        public OperationException(IOperationResult operationResult)
        {
            Result = operationResult;
        }

        public IOperationResult Result { get; set; }
    }
}
