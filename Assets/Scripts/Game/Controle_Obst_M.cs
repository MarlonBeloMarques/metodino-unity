using UnityEngine;

namespace Assets.Scripts
{
    public class Controle_Obst_M : Controle_Obst
    {
        public GameObject Pedra_out = null;
        public GameObject Insta_Pedra_out = null;
        public GameObject Pedra_Ponta_out = null;
        public GameObject Insta_Pedra_Ponta_out = null;
        // Use this for initialization

        // Update is called once per frame
        void Update()
        {
            Controle_Obstaculos_Plat_M();
        }

        public void Controle_Obstaculos_Plat_M()
        {
            if (Interval_Spawn != 20)
            {
                Currentime += Time.deltaTime;
            }

            if (Currentime >= Interval_Spawn)
            {
                Currentime = 0;
               
                if (Plat_Atual == Plat_Receb.GetComponent<Spawn_Plataforma_M>().Objetos[2])
                {
                    Insta_Pedra_Ponta.GetComponent<Instanciar_Pedra_Ponta>().Instanciar_Obstaculo(Pedra_Ponta.GetComponent<Spawn_Pedra_Ponta>().Obj_aux, Insta_Pedra_Ponta.GetComponent<Instanciar_Pedra_Ponta>().Pos);
                    Insta_Pedra.GetComponent<Instanciar_Pedra>().Instanciar_Obstaculo(Pedra.GetComponent<Spawn_Pedra>().Obj_aux, Insta_Pedra.GetComponent<Instanciar_Pedra>().Pos);
                }

                else 
                
                if (Plat_Atual == Plat_Receb.GetComponent<Spawn_Plataforma_M>().Objetos[1])
                {
                    Insta_Pedra_Ponta.GetComponent<Instanciar_Pedra_Ponta>().Instanciar_Obstaculo(Pedra_Ponta.GetComponent<Spawn_Pedra_Ponta>().Obj_aux, Insta_Pedra_Ponta.GetComponent<Instanciar_Pedra_Ponta>().Pos);
                    Insta_Pedra_Ponta_out.GetComponent<Instanciar_Pedra_Ponta>().Instanciar_Obstaculo(Pedra_Ponta_out.GetComponent<Spawn_Pedra_Ponta_1>().Obj_aux, Insta_Pedra_Ponta_out.GetComponent<Instanciar_Pedra_Ponta>().Pos);

                    if (Pedra_Ponta.GetComponent<Spawn_Pedra_Ponta>().Obj_aux == Pedra_Ponta.GetComponent<Spawn_Pedra_Ponta>().Objetos[0])
                    {
                        Insta_Pedra.GetComponent<Instanciar_Pedra_1>().Instanciar_Obstaculo(Pedra.GetComponent<Spawn_Pedra_1>().Obj_aux, Insta_Pedra.GetComponent<Instanciar_Pedra_1>().Pos);
                        Insta_Pedra.GetComponent<Instanciar_Pedra_1>().Instanciar_Obstaculo(Pedra.GetComponent<Spawn_Pedra_1>().Obj_aux, Insta_Pedra.GetComponent<Instanciar_Pedra_1>().Pos1);
                        Insta_Pedra.GetComponent<Instanciar_Pedra_1>().Instanciar_Obstaculo(Pedra.GetComponent<Spawn_Pedra_1>().Obj_aux, Insta_Pedra.GetComponent<Instanciar_Pedra_1>().Pos2);
                    }
                    else
                    {
                        Insta_Pedra_out.GetComponent<Instanciar_Pedra>().Instanciar_Obstaculo(Pedra_out.GetComponent<Spawn_Pedra>().Obj_aux, Insta_Pedra_out.GetComponent<Instanciar_Pedra>().Pos);
                    } 

                 } 

                 else
                 
                 if (Plat_Atual == Plat_Receb.GetComponent<Spawn_Plataforma_M>().Objetos[0])
                 {
                    
                      Insta_Pedra_Ponta.GetComponent<Instanciar_Pedra_Ponta>().Instanciar_Obstaculo(Pedra_Ponta.GetComponent<Spawn_Pedra_Ponta>().Obj_aux, Insta_Pedra_Ponta.GetComponent<Instanciar_Pedra_Ponta>().Pos);

                      if (Pedra_Ponta.GetComponent<Spawn_Pedra_Ponta>().Obj_aux == Pedra_Ponta.GetComponent<Spawn_Pedra_Ponta>().Objetos[0])
                      {
                            Insta_Pedra.GetComponent<Instanciar_Pedra_1>().Instanciar_Obstaculo(Pedra.GetComponent<Spawn_Pedra_1>().Obj_aux, Insta_Pedra.GetComponent<Instanciar_Pedra_1>().Pos);
                            Insta_Pedra.GetComponent<Instanciar_Pedra_1>().Instanciar_Obstaculo(Pedra.GetComponent<Spawn_Pedra_1>().Obj_aux, Insta_Pedra.GetComponent<Instanciar_Pedra_1>().Pos1);
                            Insta_Pedra.GetComponent<Instanciar_Pedra_1>().Instanciar_Obstaculo(Pedra.GetComponent<Spawn_Pedra_1>().Obj_aux, Insta_Pedra.GetComponent<Instanciar_Pedra_1>().Pos2);
                            Insta_Pedra_out.GetComponent<Instanciar_Pedra>().Instanciar_Obstaculo(Pedra_out.GetComponent<Spawn_Pedra>().Obj_aux, Insta_Pedra_out.GetComponent<Instanciar_Pedra>().Pos);
                      }
                      else
                      {
                            Insta_Pedra.GetComponent<Instanciar_Pedra_1>().Instanciar_Obstaculo(Pedra.GetComponent<Spawn_Pedra_1>().Obj_aux, Insta_Pedra.GetComponent<Instanciar_Pedra_1>().Pos);
                            Insta_Pedra.GetComponent<Instanciar_Pedra_1>().Instanciar_Obstaculo(Pedra.GetComponent<Spawn_Pedra_1>().Obj_aux, Insta_Pedra.GetComponent<Instanciar_Pedra_1>().Pos1);
                            Insta_Pedra_out.GetComponent<Instanciar_Pedra>().Instanciar_Obstaculo(Pedra_out.GetComponent<Spawn_Pedra>().Obj_aux, Insta_Pedra_out.GetComponent<Instanciar_Pedra>().Pos);
                      } 
                } 

                Interval_Spawn = 20;
            }
        }
    }
}
