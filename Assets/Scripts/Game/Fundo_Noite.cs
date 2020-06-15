
using UnityEngine;

namespace Assets.Scripts
{
    public class Fundo_Noite : MonoBehaviour
    {
        public SpriteRenderer fundo;
        public float a;

        // Use this for initialization
        void Start()
        {
            fundo = GetComponent<SpriteRenderer>();
            a = 1;
        }

        // Update is called once per frame
        void Update()
        {
            if(Anoitecer.E_noite)
            {
                a -= Time.deltaTime;
       
                fundo.color = new Color(fundo.color.r, fundo.color.g, fundo.color.b,a);

                if (a < 0)
                {
                    a = 0;
                }
            }
            else
            {
                a += Time.deltaTime;

                fundo.color = new Color(fundo.color.r, fundo.color.g, fundo.color.b, a);

                if( a >=1)
                {
                    a = 1;
                }
            }

        }
    }
}
