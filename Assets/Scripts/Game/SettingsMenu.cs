using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour {

    public AudioMixer audiomixer;

   public void SetVolume(float volume)
   {
        audiomixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("OnGui_vol", volume);
   }

    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
        StartCoroutine("reset");
    }

    IEnumerator reset()
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("IniciarMenu");
    }
}
