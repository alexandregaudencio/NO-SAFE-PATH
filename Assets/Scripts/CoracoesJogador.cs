using Game.CharacterSystem;
using UnityEngine;

namespace Game.UI
{
    public class CoracoesJogador : MonoBehaviour
    {
        [SerializeField]
        private PlayableCharacter playableCharacter;

        [SerializeField] private GameObject[] coracoes;

        private void OnEnable()
        {
            playableCharacter.HealthChanged += AatualizarCoracoes;
        }

        private void OnDisable()
        {
            playableCharacter.HealthChanged -= AatualizarCoracoes;

        }

        private void AatualizarCoracoes(int vida)
        {
            for (int i = 0; i < coracoes.Length; i++)
            {
                if (vida > i)
                {
                    coracoes[i].SetActive(true);
                }
                else
                {
                    coracoes[i].SetActive(false);
                }
            }

        }


    }
}
