using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
   
    public string boats;
    private int score = 0;
    public bool winConditionOne = false;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.CompareTag(boats))
        {
            Destroy(collision.gameObject);
            score = score + 1;
        }
    }
    public void Update()
    {
        if(score == 3)
        {
            winConditionOne = true;
        }
    }
}
