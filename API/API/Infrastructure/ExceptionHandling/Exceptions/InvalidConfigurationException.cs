namespace API.Infrastructure.ExceptionHandling.Exceptions
{
    public class InvalidConfigurationException : Exception
    {
        public InvalidConfigurationException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
