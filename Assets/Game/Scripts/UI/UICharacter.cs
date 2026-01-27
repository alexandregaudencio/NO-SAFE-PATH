using System;
using Game.CharacterSystem;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class UICharacter : MonoBehaviour
    {
        [Inject]
        private IPlayableCharacter playableCharacter;

        [SerializeField] private GameObject[] heartsObjects;
        private IDisposable healthSubscription;
        private void Start()
        {
          healthSubscription =   playableCharacter.Health
              .Subscribe(UpdateHearts)
              .AddTo(this);
        }


        private void OnDestroy()
        {
            healthSubscription.Dispose();
        }


        private void  UpdateHearts(int health)
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
