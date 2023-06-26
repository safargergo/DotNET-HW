namespace LeagueTableApp.BLL.Exceptions;

public class AlreadyUsedNameAtInsertException : Exception
{
    public AlreadyUsedNameAtInsertException()
    {
    }

    public AlreadyUsedNameAtInsertException(string message) : base(message)
    {
    }

    public AlreadyUsedNameAtInsertException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
