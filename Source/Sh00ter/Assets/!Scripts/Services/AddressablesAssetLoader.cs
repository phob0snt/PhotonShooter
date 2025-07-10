using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace ShooterGame.Services
{
    public class AddressablesAssetLoader : IAssetLoader
    {
        public UniTask<T> LoadAssetAsync<T>(string address)
        {
            return Addressables.LoadAssetAsync<T>(address).ToUniTask();
        }
    }
}