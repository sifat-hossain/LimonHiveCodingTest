namespace Identity.Http.Extensions.Token
{
    public interface IAuthorizationTokenProvider
    {
        (bool, IAuthorizationToken) TryGet();
    }
}