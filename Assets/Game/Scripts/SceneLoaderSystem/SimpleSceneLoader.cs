using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SceneLoaderSystem
{
    public class SimpleSceneLoader : MonoBehaviour
    {
        [SerializeField] private AssetReference scene;

        public /*async*/ void LoadSceneAsync()
        {
           var handle = Addressables.LoadSceneAsync(scene);
           // await handle.ToUniTask();
        }
        
    }
}
