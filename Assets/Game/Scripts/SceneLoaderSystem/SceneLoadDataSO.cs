namespace SceneLoaderSystem
{
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    [CreateAssetMenu(menuName = "Config/Scene Load Data")]
    public sealed class SceneLoadDataSO : ScriptableObject
    {
        //Can be changed by a serialized Dictionary
        [field: SerializeField]  public SceneLoadData[] Scenes {get; private set;}
    }

   

}