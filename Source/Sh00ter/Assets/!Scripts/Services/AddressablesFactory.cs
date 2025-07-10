using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ShooterGame.Services
{
    public class AddressablesFactory : IAddressablesFactory
    {
        public async UniTask<T> CreateMonoBehaviour<T>(string address) where T : MonoBehaviour
        {
            var handle = Addressables.InstantiateAsync(address, null);
            GameObject obj = await handle.ToUniTask();
            return obj.GetComponent<T>();
        }

        public async UniTask<GameObject> CreateGameObject(string address)
        {
            var handle = Addressables.InstantiateAsync(address, null);
            return await handle.ToUniTask();
        }
    }
}