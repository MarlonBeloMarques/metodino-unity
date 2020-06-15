using UnityEngine;

public class Instanciar_Obst : MonoBehaviour {
    public GameObject Obj;
    public GameObject Pos;
	// Use this for initialization

    public void Instanciar_Obstaculo(GameObject Obj, GameObject Pos)
    {
            GameObject prefab = Instantiate(Obj, Pos.transform.position, Quaternion.identity);
    }
}
