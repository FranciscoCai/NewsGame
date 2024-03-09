using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SquareOfDeath : MonoBehaviour
{
    public string boats;
    public int lost = 0;
    public string GameOver;


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag(boats))
        {
            Destroy(collision.gameObject);
            lost = lost + 1;
        }
        if (lost == 2)
        {
            SceneManager.LoadScene(GameOver);
        }
    }
   
}
