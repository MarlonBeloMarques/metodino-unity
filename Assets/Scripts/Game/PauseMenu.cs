using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class PauseMenu : MonoBehaviour
    {
        public static bool GameIsPause = false;
        public GameObject MenuPauseUI;
       // public GameObject MenuInicial;
        public bool startpause;

        public GameObject contagem_fundo;

        // Update is called once per frame
        void Update()
        {
            if (startpause)
            {
                if (GameIsPause)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
            startpause = false;
        }

        public void Resume()
        {
            contagem_fundo.SetActive(true);
            MenuPauseUI.SetActive(false);
            GameIsPause = false;
        }

        public void Pause()
        {
            MenuPauseUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPause = true;
            Contagem.cont = true;
        }

        public void StartPause(bool active)
        {
            startpause = active;
            AudioListener.pause = true;
        }

        public void LoadMenu()
        { 
            IniciarMenu.Iniciou = false;
            Dino.pontuacao = 0;
            Time.timeScale = 1f;
            GameIsPause = false;
            SceneManager.LoadScene("JogoMetoDino");
            PressIniciar.IniciouMenu = true;
            Contagem.cont = false;
            StartCoroutine("Ativa_Som");
        }

        IEnumerator Ativa_Som()
        {
            yield return new WaitForSeconds(0.2f);
            AudioListener.pause = false;
        }

    }
}
