using System;
using System.Collections;
using EnemyFactorySystem;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace LevelSystem
{

    public class GameManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text text_tempo;
        [SerializeField] private GameObject gameoverTela;

        private void Start()
        {
            gameoverTela.SetActive(false);
            StartCoroutine(Temporizador());
        }

        private void OnDestroy()
        {
            StopCoroutine(Temporizador());
        }

        public void ReiniciarCena()
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }

        public IEnumerator Temporizador()
        {
            while (true)
            {
                var time = TimeSpan.FromSeconds(Time.timeSinceLevelLoad);
                var tempoFormatado = $"{time.Minutes:00}:{time.Seconds:00}";
                text_tempo.SetText(tempoFormatado);
                yield return new WaitForSeconds(1);


            }
        }
    }
}
