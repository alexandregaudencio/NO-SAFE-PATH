using UnityEngine.AddressableAssets;

namespace SceneLoaderSystem
{
    [System.Serializable]
    public sealed class SceneLoadData
    {
        public SceneKey Key;
        public AssetReference SceneReference;
    }
}