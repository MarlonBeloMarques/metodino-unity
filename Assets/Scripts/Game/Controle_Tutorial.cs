
using UnityEngine;

namespace Assets.Scripts
{
    public class Controle_Tutorial : MonoBehaviour
    {
        public static bool tutorial;

        // Update is called once per frame
        void Update()
        {
            if (PressIniciar.active_tuto)
            {
                if (IniciarMenu.activeTuto_2)
                {
                    tutorial = true;
                }
            }
        }
    }

}
