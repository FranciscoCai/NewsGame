using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whales : MonoBehaviour
{
    public string whales;
    public string boats;
    private int Score = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag(whales))
        {
            Destroy(collision.gameObject);
            GameManager.instance.winConditionTwo = true;
        }

        if (collision.gameObject.CompareTag(boats))
        {
            Destroy(collision.gameObject);
            GameManager.instance.lose = true;
        }
    }
}
