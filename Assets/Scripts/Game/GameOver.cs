using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameOver : MonoBehaviour {

        public GameObject gameover;
        public GameObject dino;
        public GameObject NumM;
        public GameObject ButtonPause;
        public static bool resetgame;

        public UnityEngine.UI.Text ponto;
        public UnityEngine.UI.Text Record;

        // Update is called once per frame
        void Update() {

            ponto.text = PlayerPrefs.GetInt("pontuacao").ToString();
            Record.text = PlayerPrefs.GetInt("Record").ToString();

            if (Dino.Morreu)
            {
                StartCoroutine("Ativa_GameOver");
            }

        }

        public void ResetarGame()
        {
            resetgame = true;
            Dino.pontuacao = 0;
            Dino.Morreu = false;
            Dino.EstaVivo = true;
            SceneManager.LoadScene("JogoMetoDIno");
            Time.timeScale = 1f;
            IniciarMenu.Iniciou = true;
            StartCoroutine("Ativa_Som");
        }

        public void ReturnMenu()
        {
            Restart();
            Time.timeScale = 1f;
            StartCoroutine("Ativa_Som");

        }

        public void Restart()
        {
            IniciarMenu.Iniciou = false;
            Dino.pontuacao = 0;
            Dino.Morreu = false;
            Dino.EstaVivo = true;
            gameover.SetActive(false);
            SceneManager.LoadScene("JogoMetoDIno");
            PressIniciar.IniciouMenu = true;
        }

        IEnumerator Ativa_Som()
        {
            yield return new WaitForSeconds(0.2f);
            AudioListener.pause = false;
        }

        IEnumerator Ativa_GameOver()
        {
            yield return new WaitForSeconds(0.8f);
            gameover.SetActive(true);
            AudioListener.pause = true;
            IniciarMenu.Iniciou = false;
            Time.timeScale = 0f;
        }
    }
}

