
using UnityEngine;

public class ControleMenu : MonoBehaviour {

    public GameObject Menu;

	// Update is called once per frame
	void Update () {
        menu();
	}

    public void menu()
    {
        if (PressIniciar.IniciouMenu)
        {
            Menu.SetActive(true);
        }
    }


    public void credits()
    {
        PressIniciar.IniciouMenu = false;
        Menu.SetActive(false);
    }

    public void Config()
    {
        PressIniciar.IniciouMenu = false;
        Menu.SetActive(false);
    }

    public void Ranking()
    {
        PressIniciar.IniciouMenu = false;
        Menu.SetActive(false);
    }
}
