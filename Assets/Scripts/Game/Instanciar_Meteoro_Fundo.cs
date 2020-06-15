using UnityEngine;

public class Instanciar_Meteoro_Fundo : MonoBehaviour {
    public GameObject Meteoro;
    public GameObject Meteoro2;
    private GameObject auxMeto;
    private int meteoros;
    public float intervalSpawn;
    public float Currentime;
    private float posicao;
    private float x;
    public float PosA;
    public float PosB;
    public float PosC;

	// Use this for initialization
	void Start () {
        Currentime = 0;
	}
	
	// Update is called once per frame
	void Update () {

        Sorteio();

	}

    public void Sorteio()
    {
         Currentime += Time.deltaTime;

        if (Currentime >= intervalSpawn)
        {
            Currentime = 0;
            posicao = Random.Range(1, 100);
            meteoros = Random.Range(1, 100);
            if (posicao < 30)
            {
                x = PosA;
            }
            else if (posicao > 30 && posicao < 60)
            {
                x = PosB;
            }
            else
            {
                x = PosC;
            }

            if (meteoros > 50)
            {
                auxMeto = Meteoro;
            }
            else
            {
                auxMeto = Meteoro2;
            }

            GameObject prefab = Instantiate(auxMeto) as GameObject;
            prefab.transform.position = new Vector3(x, transform.position.y);
        }
    }
}
