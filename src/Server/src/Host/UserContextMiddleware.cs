using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TagIt;

public class UserContextMiddleware
{
    private readonly IUserContextFactory _userContextFactory;
    private readonly IUserContextAccessor _userContextAccessor;
    private readonly RequestDelegate _next;

    public UserContextMiddleware(
        RequestDelegate next,
        IUserContextFactory userContextFactory,
        IUserContextAccessor userContextAccessor)
    {
        _next = next;
        _userContextFactory = userContextFactory;
        _userContextAccessor = userContextAccessor;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            _userContextAccessor.Context = await _userContextFactory
                .CreateAsync(context.RequestAborted);
        }
        catch (Exception ex)
        {

        }

        await _next(context);
    }
}
