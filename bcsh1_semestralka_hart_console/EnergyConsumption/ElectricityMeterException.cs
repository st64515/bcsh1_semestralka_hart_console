



public class ElectricityMeterException : Exception
{
    public ElectricityMeterException()
    {
    }

    public ElectricityMeterException(string? message) : base(message)
    {
    }
    public ElectricityMeterException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
    
}
