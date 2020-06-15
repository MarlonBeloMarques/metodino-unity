using UnityEngine;

public class Luz_Objetos : MonoBehaviour {
    public Light lt;
    public float limit;
    public float Pos_Destroi;
	// Update is called once per frame
	void Update () {
		
        if(Anoitecer.E_noite)
        {
            lt.intensity += 2 * Time.deltaTime;

            if(lt.intensity >limit)
            {
                lt.intensity = limit;
            }
        }
        else
        {
            lt.intensity = 0;
        }
	}
}
