using UnityEngine;

namespace Assets.Scripts
{
    public class Moving_Obj : MonoBehaviour
    {
        public float Velocidade;
        public float Limite;
        protected float x;
        public float z;

        public bool verif_plat_2;
        public bool verif_plat_1;
        public bool verif_plat_1_1;
        public bool verif_plat_1_0;
        public float Currentime;
        public float limitetime;

        public void Start()
        {
            Currentime = 0;
        }

        public void Destruir_Object()
        {
            x = transform.position.x;
            x += Velocidade * Time.deltaTime;

            transform.position = new Vector3(x, transform.position.y, z);

            if (transform.position.x <= Limite)
            {
                Destroy(transform.gameObject);
            }
        }

        public void Logica_Veloc_Dificul()
        {
            if (limitetime != 50)
            {
                Currentime += Time.deltaTime;
            }

            if (Currentime >= limitetime)
            {
                Currentime = 0;

                if(Dino.pontuacao > 250)
                {
                    Velocidade = -8.5f;
                }
                else if (Dino.pontuacao > 150)
                {
                    Velocidade = -8f;
                }
                else if (Dino.pontuacao > 100)
                {
                    Velocidade = -7.5f;
                }
                else if (Dino.pontuacao > 50)
                {
                    Velocidade = -7f;
                }
                else
                {
                    Velocidade = -6f;
                }

                limitetime = 50;
            }

        }
    }
}

