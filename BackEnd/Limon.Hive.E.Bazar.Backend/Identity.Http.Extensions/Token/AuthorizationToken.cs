namespace Identity.Http.Extensions.Token
{
    public class AuthorizationToken : IAuthorizationToken
    {
        public string Type { get; }

        public string Value { get; }

        public AuthorizationToken(string type, string value)
        {
            Type = type;
            Value = value;
        }

        public override string ToString() => $"{Type} {Value}";
    }
}