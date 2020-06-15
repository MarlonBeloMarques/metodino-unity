using UnityEngine;

namespace Assets.Scripts
{
    public class Spawn_Plataforma_Mme: Spawn
    {
        public GameObject[] Obj_AUX { get; set; }

        private void Start()
        {
            Obj_AUX = new GameObject[2];
        }

        private void Update()
        {
            Sorteio();
        }

        public override void Sorteio()
        {
          
            Currentime += Time.deltaTime;

            if (Currentime >= Interval_Spawn)
            {
                Currentime = 0;

                for (int i = 0; i < 2; i++)
                {

                    float pos = Random.Range(10, 80);

                    if (pos > 70)
                    {
                        Obj_AUX[i] = Objetos[0];
                    }
                    else if (pos > 60)
                    {
                        Obj_AUX[i] = Objetos[1];
                    }
                    else if (pos > 50)
                    {
                        Obj_AUX[i] = Objetos[2];
                    }
                    else if (pos > 40)
                    {
                        Obj_AUX[i] = Objetos[3];
                    }
                    else if (pos > 30)
                    {
                        Obj_AUX[i] = Objetos[4];
                    }
                    else if (pos > 20)
                    {
                        Obj_AUX[i] = Objetos[5];
                    }
                    else if (pos > 10)
                    {
                        Obj_AUX[i] = Objetos[6];
                    }
       
                }
            }     
        }
    }
}
