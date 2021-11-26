namespace Common.Models
{
    public class AuthResource
    {
        public string Uid { get; set; }
        public string Token { get; set; }

        public bool HasUid => !string.IsNullOrEmpty(Uid);

        public bool HasToken => !string.IsNullOrEmpty(Token);

        public override string ToString()
        {
            return $"Bearer {Token}";
        }
    }
}
