using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    private bool pausa = false;
    public void Pausa()
    {
        if (pausa == true)
        {
            Time.timeScale = 0f;
            pausa = false;
        }
        else if (pausa == false)
        {
            Time.timeScale = 1f;
            pausa=true;
        }
    }
}
