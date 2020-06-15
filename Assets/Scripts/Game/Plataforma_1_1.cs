﻿using UnityEngine;

namespace Assets.Scripts
{
    class Plataforma_1_1: Moving_Obj
    {
        private void Update()
        {
            Logica_Veloc_Dificul();
            Destruir_Object();
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Plataforma_2")
            {
                Velocidade = -4.6f;
                verif_plat_2 = true;
            }

            else if (collision.tag == "Plataforma_1_1")
            {
                Velocidade = -5.3f;
                verif_plat_1_1 = true;
            }

            else if (collision.tag == "Plataforma_1")
            {
                Velocidade = -4.2f;
                verif_plat_1 = true;
            }

            else if (collision.tag == "Plataforma_1_0")
            {
                Velocidade = -5.6f;
                verif_plat_1_0 = true;
            }
        }

    }
}
