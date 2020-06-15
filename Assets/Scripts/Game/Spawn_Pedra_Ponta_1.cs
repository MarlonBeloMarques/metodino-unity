using UnityEngine;

namespace Assets.Scripts
{
    class Spawn_Pedra_Ponta_1: Spawn
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

                float pos = Random.Range(0, 100);

                if (pos > 50)
                {
                    Obj_aux = Objetos[0];
                }
                else
                {
                    Obj_aux = Objetos[1];
                }

            }
        }
    }
}
