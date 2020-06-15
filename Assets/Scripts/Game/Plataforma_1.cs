using UnityEngine;

namespace Assets.Scripts
{
    class Plataforma_1: Moving_Obj
    {
        private void Update()
        {
            Destruir_Object();
            Logica_Veloc_Dificul();

        } 

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Plataforma_2")
            {
                 Velocidade = -5.5f;
                verif_plat_2 = true;
            }

            else if (collision.tag == "Plataforma_1")
            {
                Velocidade = -5;
                verif_plat_1 = true;
            }

            else if (collision.tag == "Plataforma_1_1")
            {
                Velocidade = -6.4f;
                verif_plat_1_1 = true;
            }

            else if (collision.tag == "Plataforma_1_0")
            {
                Velocidade = -6.8f;
                verif_plat_1_0 = true;
            }

        }
    }
}
