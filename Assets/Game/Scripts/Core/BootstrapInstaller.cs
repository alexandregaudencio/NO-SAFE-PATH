using UnityEngine;
using Zenject;

namespace Core
{

    public sealed class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<Bootstrapper>()
                .AsSingle();
        }
    }

}
