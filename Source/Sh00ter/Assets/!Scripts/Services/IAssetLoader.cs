using Cysharp.Threading.Tasks;

namespace ShooterGame.Services
{
    public interface IAssetLoader
    {
        UniTask<T> LoadAssetAsync<T>(string address);
    }
}