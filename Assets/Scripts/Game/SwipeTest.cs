using UnityEngine;

namespace Assets.Scripts
{
    public class SwipeTest : MonoBehaviour
    {
        public Swipe swipecontrols;
        public bool pular;
        public bool abaixar;
        // Update is called once per frame
        void Update()
        {

            // desiredPosition recebe os valores de movimento para cima, baixo, direito, esquerdo
            if (IniciarMenu.Iniciou)
            {
                if (Dino.EstanoChao && !PauseMenu.GameIsPause && !Contagem.cont)
                {
                    if (swipecontrols.SwipeUp)
                    {
                        pular = true;
                    }
                    if (swipecontrols.SwipeDown)
                    {
                        abaixar = true;
                    }
                }

            }

        }
    }
}
