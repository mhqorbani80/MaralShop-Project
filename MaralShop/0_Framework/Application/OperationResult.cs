namespace _0_Framework.Application
{
    public class OperationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public OperationResult()
        {
            Success = false;
        }
        public OperationResult IsSuccess(string message="عملیات با موفقیت انجام شد")
        {
            Success = true;
            Message=message;
            return this;
        }

        public OperationResult IsFaild(string message)
        {
            Success=false;
            Message=message;
            return this;
        }
    }
}
