
using UnityEngine;

namespace Assets.Scripts
{
    public class Contagem : MonoBehaviour
    {
        public GameObject contagem_fundo;
        public static bool cont;

        public void contagem()
        {
            contagem_fundo.SetActive(false);
            Time.timeScale = 1f;
            AudioListener.pause = false;
            cont = false; 
        }
    }
}
