using UnityEngine;

namespace Assets.Scripts
{
   public class Meteoro_Fundo : Moving_Obj
    {
        public float currentime_me;
        public float limit_me;
        private float y;
        public AudioSource explo;

        private void Update()
        {
            Destroir_Object_Fundo();

            currentime_me += Time.deltaTime;

            if(currentime_me>=limit_me)
            {
                currentime_me = 0;
                explo.Play();
            }
        }

        public void Destroir_Object_Fundo()
        {
            y = transform.position.y;
            x = transform.position.x;

            y += Velocidade * Time.deltaTime;
            x -= Velocidade * Time.deltaTime;

            transform.position = new Vector3(x, y,z);

            if (transform.position.y <= Limite)
            {
                Destroy(gameObject);
            }
        }
    }
}
