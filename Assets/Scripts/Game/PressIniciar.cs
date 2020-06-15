
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressIniciar : MonoBehaviour {

    public GameObject Press;
    public static bool IniciouMenu;
    public static int cont;
    public static bool active_tuto;

    public GameObject Gibi;

	// Use this for initialization
	void Start () {
        cont = 0;
	}
	
	// Update is called once per frame

    public void pressIniciar()
    {
        Press.SetActive(true);
    }

    public void IniciarGibi()
    {

        if (PlayerPrefs.GetInt("cont", cont) == 0)
        {
            cont += 1;
            PlayerPrefs.SetInt("cont", cont);
            active_tuto = true;
        }

        Gibi.SetActive(true);
        IniciouMenu = true;
    }
}
