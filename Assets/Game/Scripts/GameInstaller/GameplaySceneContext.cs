using System.Linq;
using UnityEngine;
using Zenject;

namespace Installer
{
    public sealed class GameSceneContext : SceneContext
    {
        [Tooltip("Auto fill Extra Installers in this gameObject.")]
        private MonoInstaller[] extraInstallers;

        protected override void RunInternal()
        {
            extraInstallers = GetComponents<MonoInstaller>();
            foreach (var installer in extraInstallers)
            {
                Installers.Append(installer);
            }

            base.RunInternal();
        }
    }

}
