namespace LeagueTableApp.BLL.Exceptions;

public class MatchCreationException : Exception
{
    public MatchCreationException()
    {
    }

    public MatchCreationException(string message) : base(message)
    {
    }

    public MatchCreationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

