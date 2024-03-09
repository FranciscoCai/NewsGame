using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string Victory;
    public string GameOver;
    public Goal goal;
    public bool winConditionTwo = false;
    public bool lose = false;
    
    


    void Update()
    {
        if(goal.winConditionOne == true && winConditionTwo == true)
        {
            SceneManager.LoadScene(Victory);
        }
        if (lose == true)
        {
            SceneManager.LoadScene(GameOver);
        }
    }
}
