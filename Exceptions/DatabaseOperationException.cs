namespace LegacyOrderService.Exceptions
{
    public class DatabaseOperationException : Exception
    {
        public DatabaseOperationException(string exceptionMessage, Exception innerException) : 
            base(exceptionMessage, innerException)
        {
            
        }
    }
}