using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whales : MonoBehaviour
{
    public string whales;
    public string boats;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag(whales))
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag(boats))
        {
            GameManager.instance.lose = true;
        }
    }
}
