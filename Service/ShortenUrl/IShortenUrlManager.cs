namespace Service.ShortenUrl
{
    public interface IShortenUrlManager
    {
        public string Shorten(string url);

        public string Translate(string url);
    }
}