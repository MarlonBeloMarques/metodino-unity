
using UnityEngine;

namespace Assets.Scripts
{
    public class Som_Fundo : MonoBehaviour
    {
        public AudioSource aud1;
        public AudioSource aud2;
        public float current_dia;
        public float current_noite;
        private float limit1;
        private float limit2;

        // Use this for initialization
        void Start()
        {
            limit1 = 1;
            limit2 = 1;
            current_dia = 0;
            current_noite = 0;
        }

        // Update is called once per frame
        void Update()
        {

            if(!Anoitecer.E_noite)
            {
                if (aud2.volume >= 1)
                {
                    aud2.volume -= Time.deltaTime;
                    aud2.Stop();

                }

                if (aud1.volume <= 1)
                {
                    aud1.volume += Time.deltaTime;
                }

                current_dia += Time.deltaTime;

                if(current_dia>=limit1)
                {
                    current_dia = 0;
                    aud1.Play();
                    limit1=50;
                    limit2 = 0;
                }

            }
            else
            {
                if (aud1.volume >= 1)
                {
                    aud1.volume -= Time.deltaTime;
                    aud1.Stop();
                }

                if (aud2.volume <= 1)
                {
                    aud2.volume += Time.deltaTime;
                }

                current_noite += Time.deltaTime;

                if(current_noite>=limit2)
                {
                    current_noite = 0;
                    aud2.Play();
                    limit2 = 46;
                    limit1 = 0;
                }
            }
           

        }
    }
}
