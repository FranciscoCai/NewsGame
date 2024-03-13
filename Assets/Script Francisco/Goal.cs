using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
   
    public string boats;
    public int scoreNeed = 0;
    private int score = 0;  
    public bool winConditionOne = false;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.CompareTag(boats))
        {
            CamaraFollowSize.instance.targets.Remove(collision.transform);
            CameraFollowBoats.instance.targets.Remove(collision.transform);
            Destroy(collision.gameObject);
            score = score + 1;
        }
    }
    public void Update()
    {
        if(score == scoreNeed)
        {
            winConditionOne = true;
        }
    }
}
