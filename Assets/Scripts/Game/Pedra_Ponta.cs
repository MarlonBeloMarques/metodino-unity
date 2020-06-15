using UnityEngine;

namespace Assets.Scripts
{
    class Pedra_Ponta : Moving_Obj
    {

        public GameObject receb = null;

        private void Update()
        {
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
    }
}
