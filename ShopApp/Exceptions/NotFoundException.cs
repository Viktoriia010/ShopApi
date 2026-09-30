namespace Shop.Api.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string messsage) : base(messsage)
    {
    }

}