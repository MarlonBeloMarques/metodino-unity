using UnityEngine;

namespace Assets.Scripts
{
    public class Controle_Obst_me : Controle_Obst {
        
        // Update is called once per frame
        void Update() {

            Controle_Obstaculos_Plat_me();

        }

        public void Controle_Obstaculos_Plat_me()
        {
            if (Interval_Spawn != 20)
            {
                Currentime += Time.deltaTime;
            }

            if (Currentime>=Interval_Spawn)
            {
                Currentime = 0;

                if (Plat_Atual == Plat_Receb.GetComponent<Spawn_Plataforma_me>().Objetos[0] || Plat_Atual == Plat_Receb.GetComponent<Spawn_Plataforma_me>().Objetos[1] ||
                    Plat_Atual == Plat_Receb.GetComponent<Spawn_Plataforma_me>().Objetos[2] || Plat_Atual == Plat_Receb.GetComponent<Spawn_Plataforma_me>().Objetos[3])
                {
                    Insta_Pedra_Ponta.GetComponent<Instanciar_Pedra_Ponta>().Instanciar_Obstaculo(Pedra_Ponta.GetComponent<Spawn_Pedra_Ponta>().Obj_aux, Insta_Pedra_Ponta.GetComponent<Instanciar_Pedra_Ponta>().Pos);

                    if (Pedra_Ponta.GetComponent<Spawn_Pedra_Ponta>().Obj_aux == Pedra_Ponta.GetComponent<Spawn_Pedra_Ponta>().Objetos[0])
                    {
                        Insta_Pedra.GetComponent<Instanciar_Pedra>().Instanciar_Obstaculo(Pedra.GetComponent<Spawn_Pedra>().Obj_aux, Insta_Pedra.GetComponent<Instanciar_Pedra>().Pos);
                    }
                }

                Interval_Spawn = 20;
            }
        }
    }
}
