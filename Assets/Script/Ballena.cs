using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EstadoBarco
{
    SeguirBarco, SeguirBallena, Independiente
}
public class Ballena : Movimiento
{
    public EstadoBarco Estado;
    [SerializeField] private GameObject ObjetoASeguir;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(Estado == EstadoBarco.SeguirBarco)
        {
            return;
        }
        else if(collision.gameObject.CompareTag("Boats"))
        {
            Estado = EstadoBarco.SeguirBarco;
            ObjetoASeguir = collision.gameObject;
        }
        else if(Estado == EstadoBarco.SeguirBallena)
        {
            return;
        }
        else if(collision.gameObject.CompareTag("Whale"))
        {
            Estado = EstadoBarco.SeguirBallena;
            ObjetoASeguir = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Estado = EstadoBarco.Independiente;
        ObjetoASeguir = null;
    }
    private void Update()
    {
        if (ObjetoASeguir != null)
        {
            Vector2 direccion = (Vector2)ObjetoASeguir.transform.position - (Vector2)transform.position;
            direccion.Normalize();
            rb.velocity = direccion * InitialVelocity;
        }
        
    }
}
