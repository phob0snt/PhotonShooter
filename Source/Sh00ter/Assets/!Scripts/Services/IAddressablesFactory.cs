using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ShooterGame.Services
{
    public interface IAddressablesFactory
    {
        UniTask<T> CreateMonoBehaviour<T>(string address) where T : MonoBehaviour;
        UniTask<GameObject> CreateGameObject(string address);
    }
}