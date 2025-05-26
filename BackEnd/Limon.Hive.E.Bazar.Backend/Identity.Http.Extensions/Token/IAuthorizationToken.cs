namespace Identity.Http.Extensions.Token
{
    public interface IAuthorizationToken
    {
        string Type { get; }
        string Value { get; }

        string ToString();
    }
}