using UnityEngine;

namespace SceneLoaderSystem
{
    using Zenject;
    using UnityEngine;

    public sealed class SceneInstaller : MonoInstaller
    {
        [SerializeField] private SceneLoadDataSO sceneLoadData;

        public override void InstallBindings()
        {
            Container.Bind<ISceneLoader>()
                .To<AddressableSceneLoader>()
                .AsSingle()
                .WithArguments(sceneLoadData);
        }
    }

}
