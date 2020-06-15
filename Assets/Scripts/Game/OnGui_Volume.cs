using UnityEngine;

public class OnGui_Volume : MonoBehaviour {
    public UnityEngine.UI.Slider slid;

    private void Start()
    {
        slid.value = PlayerPrefs.GetFloat("OnGui_vol");
    }
}
