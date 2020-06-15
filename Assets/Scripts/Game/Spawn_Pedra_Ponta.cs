using UnityEngine;

namespace Assets.Scripts
{
    public class Spawn_Pedra_Ponta : Spawn
    {   

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

                float pos = Random.Range(0, 75);

                if (pos > 50)
                {
                    Obj_aux = Objetos[0];
                }
                else if (pos > 25)
                {
                    Obj_aux = Objetos[1];
                }
                else
                {
                    Obj_aux = Objetos[2];
                }
            }
        }
    }
}
