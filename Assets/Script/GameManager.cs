using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string Victory;
    public string GameOver;
    public Goal goal;
    public bool lose = false;
    public static GameManager instance;
    public int puntosNecesarios;

    private void Awake()
    {
        instance = this; 
    }
    void Update()
    {
        if(goal.winConditionOne == true && puntosNecesarios == SystemaDePuntos.instance.puntos)
        {
            SceneManager.LoadScene(Victory);
        }
        if (lose == true)
        {
            SceneManager.LoadScene(GameOver);
        }
    }
}
