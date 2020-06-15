
using UnityEngine;

public class Spawn_Luz_Vaga : MonoBehaviour {

    public GameObject Lt;
    public GameObject Lt1;
    public GameObject Lt2;
    public GameObject Lt3;
    private GameObject Ltaux;
    public float LimiteSpawn;
    public float Currenttime;
    public float y;

    // Use this for initialization
    void Start () {
        Currenttime = 0;
        LimiteSpawn = 1;
	}
	
	// Update is called once per frame
	void Update () {

        if (Anoitecer.E_noite)
        {
            Currenttime += Time.deltaTime;

            if (Currenttime >= LimiteSpawn)
            {
                Currenttime = 0;
                SorteioVagalume();

                GameObject prefab = Instantiate(Lt) as GameObject;
                prefab.transform.position = new Vector3(transform.position.x, y);

                SorteioTempo();
            }
        }
	}

    public void SorteioVagalume()
    {
        int pos = Random.Range(1, 100);

        if(pos>75)
        {
            Ltaux = Lt;
        }
        else if(pos>50)
        {
            Ltaux = Lt1;
        }
        else if(pos>25)
        {
            Ltaux = Lt2;
        }
        else
        {
            Ltaux = Lt3;
        }
    }

    public void SorteioTempo()
    {
        int pos = Random.Range(1, 90);

        if (pos > 60)
        {
            LimiteSpawn = 4;
        }
        else if (pos > 30)
        {
            LimiteSpawn = 6;
        }
        else
        {
            LimiteSpawn = 5;
        }
    }
}
