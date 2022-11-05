namespace TagIt;

public interface IUserContextFactory
{
    Task<IUserContext> CreateAsync(CancellationToken cancellationToken);


    Task<User> GetUserAsync(CancellationToken cancellationToken);
}

public interface IUserContextAccessor
{
    IUserContext? Context { get; set; }
}

public class CouldNotAccessUserContextException : Exception
{
    public CouldNotAccessUserContextException() : base("Could not access user context")
    {

    }

    public static Exception New() => throw new CouldNotAccessUserContextException();
}

public interface IUserContext
{
    public bool IsAuthenticated { get; }

    User User { get; }
}
