using UnityEngine;

namespace Assets.Scripts
{
    class Pai_Luz: Moving_Obj
    {

        public void Update()
        {
            if(Dino.pontuacao > 300)
            {
                Velocidade = -5f;
            }
            else if(Dino.pontuacao > 200)
            {
                Velocidade = -4.5f;
            }
            else if(Dino.pontuacao > 100)
            {
                Velocidade = -4f;
            }
            else if(Dino.pontuacao > 50)
            {
                Velocidade = -3.5f;
            }
            else
            {
                Velocidade = -3;
            }

            Destruir_Object();
        }
    }
}
