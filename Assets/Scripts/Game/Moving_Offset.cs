
using UnityEngine;

public class Moving_Offset : MonoBehaviour {
    private Material CurrentMat;
    public float veloc;
    private float Offset;
	// Use this for initialization
	void Start () {
        CurrentMat = GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
        Offset += veloc * Time.deltaTime;

        CurrentMat.SetTextureOffset("_MainTex", new Vector2(Offset, 0));
	}
}
