namespace Service.ShortenUrl.Encrypt
{
    public interface IEncryptService
    {
        public string Encode(string str);
        public string Decode(string str);
    }
}