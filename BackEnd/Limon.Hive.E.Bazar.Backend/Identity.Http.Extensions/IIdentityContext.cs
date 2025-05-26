namespace Identity.Http.Extensions;

public interface IIdentityContext
{
    string UserName { get; }
    string AuthorizationHeader { get; }
    string AuthorizationScheme { get; }
}