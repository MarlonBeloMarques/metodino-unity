using UnityEngine;

namespace Assets.Scripts
{
    public class IniciarMenu : MonoBehaviour
    {

        public GameObject iniciarMenu;
        public GameObject NumM;
        public GameObject ButtonPause;
        public static bool playgame;
        public static bool Iniciou;
        public static bool activeTuto_2;

        public GameObject dino;
        public SettingsMenu settMenu;

        private void Start()
        {
            settMenu.GetComponent<SettingsMenu>().audiomixer.SetFloat("volume", PlayerPrefs.GetFloat("OnGui_vol"));
        }

        // Update is called once per frame
        void Update()
        {
            if (dino.GetComponent<Dino>().Verif_Metros)
            {
                StartGame();
                NumM.SetActive(true);
            }


            if (playgame)
            {
                activeTuto_2 = true;
                Iniciou = true;
                PressIniciar.IniciouMenu = false;
            }
            playgame = false;

            if(Iniciou==true)
            {
                iniciarMenu.SetActive(false);

                if (PressIniciar.active_tuto)
                {
                    if (activeTuto_2)
                    {
                        NumM.SetActive(false);
                    }
                }
                else if (activeTuto_2)
                {
                    activeTuto_2 = false;
                    dino.GetComponent<Dino>().Verif_Metros = true;
                }
                else if(GameOver.resetgame)
                {
                    activeTuto_2 = false;
                    dino.GetComponent<Dino>().Verif_Metros = true;
                }
            }
        }

        public void StartGame()
        {
            ButtonPause.SetActive(true);
        }

        public void QuitGame()
        {
            Debug.Log("saiu");
            Application.Quit();
        }


        public void Playgame(bool active)
        {
            playgame = active;
        }
    }
}
