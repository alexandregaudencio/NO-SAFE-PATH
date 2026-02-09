using SceneLoaderSystem;
using UnityEngine;
using Zenject;

namespace Core
{


    public sealed class Bootstrapper : IInitializable
    {
        private readonly ISceneLoader sceneLoader;

        public Bootstrapper(ISceneLoader sceneLoader)
        {
            this.sceneLoader = sceneLoader;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public async void Initialize()
        {
            await sceneLoader.LoadSingleAsync(SceneKey.MainMenu);
        }
    }
}
