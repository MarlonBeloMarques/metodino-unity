using UnityEngine;

public class Anoitecer : MonoBehaviour {
    public Light lt;
    public float limite;
    public float Currenttime;
    public float Iniciarnoite;
    public float TerminarNoite;

    public static bool E_noite;

	// Use this for initialization
	void Start () {
        lt = GetComponent<Light>();
        Currenttime = 0;
	}
	
	// Update is called once per frame
	void Update () {

        if(Currenttime>=41)
        {
            Currenttime = 0;
        }
        else
        {
            Currenttime += Time.deltaTime;
        }

        Logica_Tempo();
             
	}

    public void Logica_Tempo()
    {
        if (Currenttime >= Iniciarnoite && Currenttime <= TerminarNoite)
        {
            E_noite = true;

            lt.intensity -= Time.deltaTime ;
        }
        else
        {
            E_noite = false;
        }

        if (Currenttime >= TerminarNoite)
        {
            lt.intensity += Time.deltaTime;
            if (lt.intensity >= limite)
            {
                lt.intensity = limite;
            }
        }
    }
}
