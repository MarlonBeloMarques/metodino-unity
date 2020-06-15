using UnityEngine;

public class Controle_ResoluctionTela : MonoBehaviour {

    public RectTransform screen;
    public float x;
    private float reserv;
    public float screenwidht;
    public float screenheight;
    // Use this for initialization
    void Awake() {
        reserv = screen.sizeDelta.x;

        screenwidht = Screen.width;
        screenheight = Screen.height;

        screen.sizeDelta = new Vector2(reserv * (screenwidht / screenheight / x), screen.sizeDelta.y);
    }
}
