using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

namespace SceneLoaderSystem
{
    using Cysharp.Threading.Tasks;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.ResourceManagement.AsyncOperations;
    using UnityEngine.ResourceManagement.ResourceProviders;

    public sealed class AddressableSceneLoader : ISceneLoader
    {
        private readonly SceneLoadDataSO sceneDataSo;
        private readonly Dictionary<SceneKey, AsyncOperationHandle<SceneInstance>> _loaded =
            new();

        public AddressableSceneLoader(SceneLoadDataSO sceneDataSO)
        {
            sceneDataSo = sceneDataSO;
        }

        public async UniTask LoadSingleAsync(SceneKey scene)
        {
            await UnloadAllAsync();

            await LoadInternal(scene, LoadSceneMode.Single);
        }

        public async UniTask LoadAdditiveAsync(SceneKey scene)
        {
            await LoadInternal(scene, LoadSceneMode.Additive);
        }

        public async UniTask UnloadAsync(SceneKey scene)
        {
            if (!_loaded.TryGetValue(scene, out var handle))
                return;

            await Addressables.UnloadSceneAsync(handle).ToUniTask();
            Addressables.Release(handle);

            _loaded.Remove(scene);
        }

        private async UniTask LoadInternal(SceneKey key, LoadSceneMode mode)
        {
            var entry = FindEntry(key);

            var handle = Addressables.LoadSceneAsync(
                entry.SceneReference,
                mode,
                activateOnLoad: true
            );

            await handle.ToUniTask();

            _loaded[key] = handle;
        }

        private SceneLoadData FindEntry(SceneKey key)
        {
            foreach (var entry in sceneDataSo.Scenes)
                if (entry.Key == key)
                    return entry;

            throw new System.Exception($"Scene {key} not found");
        }

        private async UniTask UnloadAllAsync()
        {
            foreach (var key in _loaded.Keys.ToArray())
                await UnloadAsync(key);
        }
    }

}