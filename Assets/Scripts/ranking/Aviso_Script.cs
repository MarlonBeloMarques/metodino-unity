using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aviso_Script : MonoBehaviour {

  public void Desativa()
    {
        gameObject.SetActive(false);
        FacebookAndPlayFabFunctions.aviso_restricao = false;
    }
}
