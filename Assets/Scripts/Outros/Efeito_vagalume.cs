using UnityEngine;

public class Efeito_vagalume : MonoBehaviour {

    public Light lt;

    public float limite;
    public float Limite_Destroir;

    public float Currentime;
    public float limit_varia;
    public float limit_varia2;
    // Use this for initialization
    void Start () {
        lt = GetComponent<Light>();
        Currentime = 0;
	}
	
	// Update is called once per frame
	void Update () {  

        if (transform.position.x <= Limite_Destroir)
        {
            Destroy(transform.gameObject);
        }

        Controle_Vagalume();
 
    }

    public void Controle_Vagalume()
    {
        if (Anoitecer.E_noite)
        {
            Currentime += Time.deltaTime;

            if (Currentime <= limit_varia)
            {
                lt.intensity += 2f * Time.deltaTime;
            }
            else
            {
                lt.intensity -= 2f * Time.deltaTime;
            }

            if (Currentime >= limit_varia2)
            {
                Currentime = 0;
            }
        }

        else
        {
            lt.intensity = 0;
        }
    }
}
