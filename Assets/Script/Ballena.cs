using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;
using static UnityEngine.GraphicsBuffer;

public enum EstadoBarco
{
    SeguirBarco, SeguirBallena, Independiente
}
public class Ballena : MonoBehaviour
{
    protected Rigidbody2D rb;
    [SerializeField] protected float InitialVelocity;
    public EstadoBarco Estado;
    [SerializeField] private GameObject ObjetoASeguir;
    [SerializeField] private float TiempoDeEspera;
    private bool EstaEsperando = false;
    private Camera camara;
    private Renderer rend;
    private bool movimientoIniciado = false;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        camara = GameObject.Find("Camara").GetComponent<Camera>();
        rend = GetComponent<Renderer>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Estado == EstadoBarco.SeguirBarco)
        {
            return;
        }
        else if (collision.gameObject.CompareTag("Boats"))
        {
            Estado = EstadoBarco.SeguirBarco;
            ObjetoASeguir = collision.gameObject;
            StartCoroutine(GiroAleatorio());
        }
        else if (Estado == EstadoBarco.SeguirBallena)
        {
            return;
        }
        else if (collision.gameObject.CompareTag("Whale"))
        {
            Estado = EstadoBarco.SeguirBallena;
            ObjetoASeguir = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(Estado == EstadoBarco.SeguirBarco)
        {
            return;
        }
        Estado = EstadoBarco.Independiente;
        ObjetoASeguir = null;
    }
    private void Update()
    {
        if (rend.isVisible && !movimientoIniciado)
        {
            // Marcar que el movimiento ha sido iniciado
            movimientoIniciado = true;

            // Empezar a moverse
            rb.velocity = transform.up * InitialVelocity;
        }
        if (ObjetoASeguir != null && Estado != EstadoBarco.SeguirBarco)
        {
            Vector2 direccion = (Vector2)ObjetoASeguir.transform.position - (Vector2)transform.position;
            direccion.Normalize();
            transform.up = direccion;
        }
        if (EstaEsperando == false)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, transform.up);
        }
    }
    private IEnumerator GiroAleatorio()
    {
        EstaEsperando = true;
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(TiempoDeEspera);
        EstaEsperando = false;
        float angle = Random.Range(0f, 360f);
        // Convierte el ¨¢ngulo a radianes
        float radianAngle = angle * Mathf.Deg2Rad;
        // Calcula las componentes x e y de la velocidad basadas en el ¨¢ngulo
        float velocityX = Mathf.Cos(radianAngle) * InitialVelocity;
        float velocityY = Mathf.Sin(radianAngle) * InitialVelocity;
        // Aplica la velocidad al Rigidbody2D
        transform.up = new Vector2(velocityX, velocityY);
        rb.velocity = transform.up * InitialVelocity;
    }
}
