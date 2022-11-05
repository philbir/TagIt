namespace TagIt.Security;

public class UserContextAccessor : IUserContextAccessor
{
    private static AsyncLocal<UserContextHolder> _userContextCurrent = new();

    /// <inheritdoc/>
    public IUserContext? Context
    {
        get => _userContextCurrent.Value?.Context;
        set
        {
            UserContextHolder? holder = _userContextCurrent.Value;
            if (holder != null)
            {
                // Clear current UserContext trapped in the AsyncLocals, as its done.
                holder.Context = null;
            }

            if (value != null)
            {
                // Use an object indirection to hold the UserContext in the AsyncLocal,
                // so it can be cleared in all ExecutionContexts when its cleared.
                _userContextCurrent.Value = new UserContextHolder { Context = value };
            }
        }
    }

    private class UserContextHolder
    {
        public IUserContext? Context;
    }
}


public class UserContextFactory : IUserContextFactory
{
    public UserContextFactory()
    {

    }

    public async Task<IUserContext> CreateAsync(CancellationToken cancellationToken)
    {
        User user = new User
        {
            Id = Guid.NewGuid(),
            Email = "foo@tagit.com"
        };

        return new DefaultUserContext(user);
    }

    public Task<User> GetUserAsync(CancellationToken cancellationToken)
    {
        throw new ApplicationException("No user found");
    }

}

public class DefaultUserContext : IUserContext
{
    public DefaultUserContext(User user)
    {
        User = user;
        IsAuthenticated = true;
    }

    public bool IsAuthenticated { get; }

    public User User { get; }
}
