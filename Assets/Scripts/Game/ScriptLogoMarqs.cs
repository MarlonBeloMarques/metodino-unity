
using UnityEngine;

public class ScriptLogoMarqs : MonoBehaviour {

    public GameObject MetoDIno;
    public GameObject Dino;

    public void Ativar_PressMenu()
    {
        MetoDIno.SetActive(true);
    }

    public void DesativarLogo()
    {
        gameObject.SetActive(false);
        Dino.SetActive(true);
    }

}
