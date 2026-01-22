using UnityEngine;

public class CoracoesJogador : MonoBehaviour
{
    [SerializeField] private Jogador jogador;

    [SerializeField] private GameObject[] coracoes;

    private void OnEnable()
    {
        jogador.VidaMudou += AatualizarCoracoes;
    }

    private void OnDisable()
    {
        jogador.VidaMudou -= AatualizarCoracoes;

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
