using UnityEngine;

namespace Game.UI
{
    public class MobileInputController : MonoBehaviour
    {
        #if UNITY_EDITOR
        [SerializeField] private bool activeInEditor = true;
        #endif

        private void Awake()
        {
            #if UNITY_EDITOR
            if (activeInEditor) return;
            #endif

            if (!IsMobilePlatform())
            {
                Destroy(this.gameObject);
            }
        }


        bool IsMobilePlatform()
        {
            return Application.platform == RuntimePlatform.Android
                   || Application.platform == RuntimePlatform.IPhonePlayer;
        }

    }
}
