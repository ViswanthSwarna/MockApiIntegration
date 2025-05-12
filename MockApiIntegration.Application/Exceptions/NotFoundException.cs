namespace MockApiIntegration.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string id) : base($"Product with id {id} not found") { }
    }

}
