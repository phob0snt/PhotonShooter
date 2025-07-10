using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Fusion;
using ShooterGame.Services;
using ShooterGame.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace ShooterGame.Network
{
    public class NetworkManager : INetworkManager
    {
        private NetworkRunner _networkRunner;

        private readonly IAddressablesFactory _factory;
        private readonly IAssetLoader _loader;
        private readonly DiContainer _container;
        private readonly Dictionary<string, NetworkObject> _loadedPrefabs = new();

        public NetworkManager(IAddressablesFactory factory, DiContainer container, IAssetLoader loader)
        {
            _factory = factory;
            _container = container;
            _loader = loader;
        }

        public async UniTask StartGame(GameMode gameMode, string lobbyName)
        {
            _networkRunner = await _factory.CreateMonoBehaviour<NetworkRunner>(AddressableAssetsPaths.NETWORK_RUNNER_PREFAB);
            _networkRunner.ProvideInput = true;

            await _networkRunner.StartGame(new StartGameArgs
            {
                GameMode = gameMode,
                SessionName = lobbyName,
                Scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex),
                SceneManager = _networkRunner.GetComponent<NetworkSceneManagerDefault>()
            }).AsUniTask();
        }

        public async Task<T> Spawn<T>(string address, Vector3 pos, Quaternion rot, PlayerRef authority) where T : NetworkBehaviour
        {
            var prefabGO = await _loader.LoadAssetAsync<GameObject>(address);
            var networkObject = prefabGO.GetComponent<NetworkObject>();
            
            if (networkObject == null)
            {
                Debug.LogError($"GameObject loaded from {address} doesn't have NetworkObject component!");
                Object.Destroy(prefabGO);
                return null;
            }

            if (!_loadedPrefabs.ContainsKey(address))
            {
                _loadedPrefabs[address] = networkObject;
            }

            var spawned = _networkRunner.Spawn(
                prefab: networkObject,
                position: pos,
                rotation: rot,
                inputAuthority: authority,
                onBeforeSpawned: (runner, spawnedObject) =>
                {
                    _container.InjectGameObject(spawnedObject.gameObject);
                    Debug.Log($"Dependencies injected into spawned NetworkObject: {spawnedObject.gameObject.name}");
                });

            return spawned.GetComponent<T>();
        }

        /// <summary>
        /// Очищает кэш загруженных префабов
        /// </summary>
        public void ClearPrefabCache()
        {
            foreach (var prefab in _loadedPrefabs.Values)
            {
                if (prefab != null)
                {
                    Object.Destroy(prefab.gameObject);
                }
            }
            _loadedPrefabs.Clear();
        }

        /// <summary>
        /// Получает кэшированный префаб по адресу
        /// </summary>
        public NetworkObject GetCachedPrefab(string address)
        {
            return _loadedPrefabs.TryGetValue(address, out var prefab) ? prefab : null;
        }

        /// <summary>
        /// Получает ссылку на локального игрока
        /// </summary>
        public PlayerRef GetLocalPlayer()
        {
            return _networkRunner != null ? _networkRunner.LocalPlayer : PlayerRef.None;
        }

        /// <summary>
        /// Проверяет, запущен ли NetworkRunner
        /// </summary>
        public bool IsRunning => _networkRunner != null && _networkRunner.IsRunning;
    }
}