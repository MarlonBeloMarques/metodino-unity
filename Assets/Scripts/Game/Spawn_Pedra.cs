using UnityEngine;

namespace Assets.Scripts
{
    public class Spawn_Pedra: Spawn
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

                if (pos > 75)
                {
                    Obj_aux = Objetos[0];
                }
                else if (pos > 50)
                {
                    Obj_aux = Objetos[1];
                }
                else if (pos > 25)
                {
                    Obj_aux = Objetos[2];
                }
                else
                {
                    Obj_aux = Objetos[3];
                }

            }
        }
    }
}
