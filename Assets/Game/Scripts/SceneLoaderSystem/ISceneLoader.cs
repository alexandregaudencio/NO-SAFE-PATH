namespace SceneLoaderSystem
{

    using Cysharp.Threading.Tasks;

    public interface ISceneLoader
    {
        UniTask LoadSingleAsync(SceneKey scene);
        UniTask LoadAdditiveAsync(SceneKey scene);
        UniTask UnloadAsync(SceneKey scene);
    }

    
}