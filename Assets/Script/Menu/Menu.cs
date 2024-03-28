using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    private bool pausa = false;
    [SerializeField] private GameObject[] GameObjectToActive;
    public void Pausa()
    {
        if (pausa == true)
        {
            for (int i = 0; i < GameObjectToActive.Length; i++)
            {
                GameObjectToActive[i].SetActive(true);
            }
            Time.timeScale = 0f;
            pausa = false;
        }
        else if (pausa == false)
        {
            for (int i = 0; i < GameObjectToActive.Length; i++)
            {
                GameObjectToActive[i].SetActive(false);
            }
            Time.timeScale = 1f;
            pausa=true;
        }
    }
}
