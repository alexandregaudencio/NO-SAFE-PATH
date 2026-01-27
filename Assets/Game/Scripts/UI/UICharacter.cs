using Game.CharacterSystem;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class UICharacter : MonoBehaviour
    {
        [Inject]
        private IPlayableCharacter playableCharacter;

        [SerializeField] private GameObject[] heartsObjects;

        private void OnEnable()
        {
            playableCharacter.HealthChanged += UpdateHearts;
        }

        private void OnDisable()
        {
            playableCharacter.HealthChanged -= UpdateHearts;

        }

        private void UpdateHearts(int health)
        {
            for (int i = 0; i < heartsObjects.Length; i++)
            {
                if (health > i)
                {
                    heartsObjects[i].SetActive(true);
                }
                else
                {
                    heartsObjects[i].SetActive(false);
                }
            }

        }


    }
}
