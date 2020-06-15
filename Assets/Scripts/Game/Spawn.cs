using System.Collections.Generic;
using UnityEngine;

public abstract class Spawn : MonoBehaviour {
    public List<GameObject> Objetos = new List<GameObject>();
    public float Interval_Spawn;
    public float Currentime;
    public GameObject Obj_aux { get; set; } 

    // Use this for initialization

    public abstract void Sorteio();
}
