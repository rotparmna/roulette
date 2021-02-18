namespace Masiv.Roulette.API.Middleware.Cache
{
    public interface ICacheMiddleware<T>
    {
        T GetValue(string id);
        void SetValue(string id, T value);
    }
}