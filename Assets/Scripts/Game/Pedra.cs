using UnityEngine;

namespace Assets.Scripts
{
    class Pedra : Moving_Obj
    {

        public GameObject receb;

        private void Update()
        {

            Logica_Veloc_Dificul_Pedra();
            Destruir_Object();
        }

        public void OnTriggerEnter2D(Collider2D collision_1)
        {
            if (collision_1.tag == "PLATAFORMA_2")
            {
                verif_plat_2 = true;
                receb = collision_1.gameObject;
                Velocidade = receb.GetComponent<Plataforma_2>().Velocidade;
            }

            else if (collision_1.tag == "PLATAFORMA_1")
            {
                verif_plat_1 = true;
                receb = collision_1.gameObject;
                Velocidade = receb.GetComponent<Plataforma_1>().Velocidade;
            }

            else if (collision_1.tag == "PLATAFORMA_1_0")
            {
                verif_plat_1_0 = true;
                receb = collision_1.gameObject;
                Velocidade = receb.GetComponent<Plataforma_1_0>().Velocidade;
            }

            else if (collision_1.tag == "PLATAFORMA_1_1")
            {
                verif_plat_1_1 = true;
                receb = collision_1.gameObject;
                Velocidade = receb.GetComponent<Plataforma_1_1>().Velocidade;
            }
        }

        public void Logica_Veloc_Dificul_Pedra()
        {
            if (!(verif_plat_1 || verif_plat_1_0 || verif_plat_1_1 || verif_plat_2))
            {
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
            }
        }
    }
}
