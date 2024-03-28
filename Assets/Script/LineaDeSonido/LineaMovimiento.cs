using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineaMovimiento : MonoBehaviour
{
    [SerializeField] private GameObject[] limites;
    [SerializeField] private float velocity;
    [SerializeField] private float[] RangeVelocity;

    private bool directionState = true;
    private void Start()
    {
        velocity = Random.Range(RangeVelocity[0], RangeVelocity[1]);
    }
    private void FixedUpdate()
    {

        if (directionState == true)
        {
            Vector3 direction = (limites[0].transform.position-gameObject.transform.position).normalized;
            transform.Translate(direction * velocity * Time.deltaTime, Space.World);
            if (gameObject.transform.position.x <= limites[0].transform.position.x)
            {
                directionState = false;
            }
        }
        else if (directionState == false)
        {
            Vector3 direction = (limites[1].transform.position - gameObject.transform.position).normalized;
            transform.Translate(direction * velocity * Time.deltaTime, Space.World);
            if (gameObject.transform.position.x >= limites[1].transform.position.x)
            {
                directionState = true;
            }
        }
    }

}
