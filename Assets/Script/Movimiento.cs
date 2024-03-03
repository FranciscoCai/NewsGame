using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movimiento : MonoBehaviour
{
    protected Rigidbody2D rb;
    [SerializeField] protected float InitialVelocity;
    protected void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * InitialVelocity;
        Debug.Log(transform.up * InitialVelocity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
