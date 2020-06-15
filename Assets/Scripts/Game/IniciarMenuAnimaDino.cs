using UnityEngine;

public class IniciarMenuAnimaDino : MonoBehaviour {
    public float x;
    // Update is called once per frame
    private void Start()
    {
        AudioListener.pause = false;
    }
    void Update () {
        transform.Translate(new Vector3(x * Time.deltaTime ,0));

        if(transform.position.x >= 19.00)
        {
            Destroy(gameObject);
        }
	}

}
