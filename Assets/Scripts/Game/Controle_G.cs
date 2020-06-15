using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public class Controle_G : MonoBehaviour {
        public GameObject Plat_M;
        public GameObject Plat_me;
        public GameObject Pedra;
        public GameObject Plat_Mme;

        public float Currentime;
        public float Intervalo_Spawn;

        public float[] Pos_Y_Plat_Mme;
        public float Pos_Y_Plat;
        public float Pos_Y_Pedra;

        private float pos;
        public int[] cont;

        public List<GameObject> Object_Scene = new List<GameObject>();


        // Use this for initialization
        void Start() {
            Currentime = 0;
            AudioListener.pause = false;
        }

        // Update is called once per frame
        void Update() {

            if (IniciarMenu.Iniciou)
            {
                if (!Controle_Tutorial.tutorial)
                {
                    if (Dino.pontuacao > 250) // 250
                    {
                        Sorteio_Geral_Dificil();
                    } 
                    else if (Dino.pontuacao > 120) // 120
                    {
                        Sorteio_Geral_Medio();
                    }
                    else if(Dino.pontuacao > 50) // 50
                    {
                        Sorteio_Geral_Facil_1();
                    }   
                    else
                    {
                        Sorteio_Geral_Facil_0();
                    }
                }

                Logica_Veloc_Dificul();
            }
        }

        public void Sorteio_Geral_Facil_0()
        {
            Currentime += Time.deltaTime;

            if (Currentime >= Intervalo_Spawn)
            {
                Currentime = 0;

                Instanciar(Pedra.GetComponent<Spawn_Pedra>().Obj_aux, Pos_Y_Pedra);

                Sorteio_Interval_Spawn(1.2f,1.5f,0.8f);
            }
        }

        public void Sorteio_Geral_Facil_1()
        {

            Currentime += Time.deltaTime;

            if(Currentime>=Intervalo_Spawn)
            {
                Currentime = 0;

                Sorteio_Interval_Spawn(1.6f, 1.8f, 2.2f);

                pos = Random.Range(0, 100);

                if(pos > 70)
                {
                    Instanciar(Pedra.GetComponent<Spawn_Pedra>().Obj_aux, Pos_Y_Pedra);
                }
                else
                {
                    Instanciar(Plat_me.GetComponent<Spawn_Plataforma_me>().Obj_aux, Pos_Y_Plat);
                }
            }
        }

        public void Sorteio_Geral_Medio()
        {
            Controle_Interval_Spawn(3.4f, 3.5f, 3.8f, cont[0]);

            Currentime += Time.deltaTime;

            if(Currentime>=Intervalo_Spawn)
            {
                Currentime = 0;

                Sorteio_Interval_Spawn(3.4f, 3.5f, 3.8f);

                pos = Random.Range(0, 100);

                if (pos > 20)
                {
                    Instanciar(Plat_M.GetComponent<Spawn_Plataforma_M>().Obj_aux, Pos_Y_Plat);
                }
                else if (pos > 5)
                {
                    Instanciar(Plat_me.GetComponent<Spawn_Plataforma_me>().Obj_aux, Pos_Y_Plat);
                }
                else
                {
                    Instanciar(Pedra.GetComponent<Spawn_Pedra>().Obj_aux, Pos_Y_Pedra);
                }
            }
        }

        public void Sorteio_Geral_Dificil()
        {
            Controle_Interval_Spawn(3.6f, 3.8f, 4, cont[1]);

            Currentime += Time.deltaTime;

            if(Currentime>=Intervalo_Spawn)
            {
                Currentime = 0;

                Sorteio_Interval_Spawn(3.6f, 3.8f, 4);

                pos = Random.Range(0, 100);

                if (pos > 90)  // correto : pos > 90
                {
                    Instanciar(Plat_M.GetComponent<Spawn_Plataforma_M>().Obj_aux, Pos_Y_Plat);
                }
                else if (pos > 85) // correto : pos > 85
                {
                    Instanciar(Plat_me.GetComponent<Spawn_Plataforma_me>().Obj_aux, Pos_Y_Plat);
                }
                else if (pos > 80) // correto: pos > 80
                {
                    Instanciar(Pedra.GetComponent<Spawn_Pedra>().Obj_aux, Pos_Y_Pedra);
                }
                else
                {
                    for (int i = 0; i < 2; i++)
                    {
                        Instanciar(Plat_Mme.GetComponent<Spawn_Plataforma_Mme>().Obj_AUX[i], Pos_Y_Plat_Mme[i]);
                    }

                }
            }
        }

        public void Instanciar(GameObject obj,float y)
        {
            GameObject prefab = Instantiate(obj) as GameObject;
            prefab.transform.position = new Vector3(transform.position.x, y);
        }

        public void Sorteio_Interval_Spawn(float n1, float n2, float n3)
        {
            float pos = Random.Range(0, 100);

            if (pos > 40)
            {
                Intervalo_Spawn = n1;
            }
            else if (pos > 10)
            {
                Intervalo_Spawn = n2;
            }
            else
            {
                Intervalo_Spawn = n3;
            }
        }

        public void Controle_Interval_Spawn(float n1, float n2, float n3, int aux)
        {
            if (aux < 1)
            {
                aux++;
                Sorteio_Interval_Spawn(n1, n2, n3);
            }
        }

        public void Logica_Veloc_Dificul()
        {
            if(Dino.pontuacao > 250)
            {
                Object_Scene[0].GetComponent<Moving_Offset>().veloc = 0.06f;
                Object_Scene[1].GetComponent<Moving_Offset>().veloc = 0.2f;
                Object_Scene[2].GetComponent<Moving_Offset>().veloc = 0.1f;
                Object_Scene[3].GetComponent<Moving_Offset>().veloc = 0.2f;
            }
            else if(Dino.pontuacao > 150)
            {
                Object_Scene[0].GetComponent<Moving_Offset>().veloc = 0.05f;
                Object_Scene[1].GetComponent<Moving_Offset>().veloc = 0.18f;
                Object_Scene[2].GetComponent<Moving_Offset>().veloc = 0.09f;
                Object_Scene[3].GetComponent<Moving_Offset>().veloc = 0.18f;
            }
            else if(Dino.pontuacao > 100)
            {
                Object_Scene[0].GetComponent<Moving_Offset>().veloc = 0.04f;
                Object_Scene[1].GetComponent<Moving_Offset>().veloc = 0.16f;
                Object_Scene[2].GetComponent<Moving_Offset>().veloc = 0.08f;
                Object_Scene[3].GetComponent<Moving_Offset>().veloc = 0.16f;
            }
            else if(Dino.pontuacao > 50)
            {
                Object_Scene[0].GetComponent<Moving_Offset>().veloc = 0.03f;
                Object_Scene[1].GetComponent<Moving_Offset>().veloc = 0.14f;
                Object_Scene[2].GetComponent<Moving_Offset>().veloc = 0.07f;
                Object_Scene[3].GetComponent<Moving_Offset>().veloc = 0.14f;
            }
            else
            {
                Object_Scene[0].GetComponent<Moving_Offset>().veloc = 0.02f;
                Object_Scene[1].GetComponent<Moving_Offset>().veloc = 0.12f;
                Object_Scene[2].GetComponent<Moving_Offset>().veloc = 0.06f;
                Object_Scene[3].GetComponent<Moving_Offset>().veloc = 0.12f;
            }
        }

    }
}
