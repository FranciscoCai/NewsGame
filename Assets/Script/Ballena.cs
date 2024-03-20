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
    [SerializeField] private float TiempoDeEspera;
    private bool EstaEsperando = false;

    public Camera mainCamera;
    private Renderer rend;
    private bool isVisible = false;
    private bool movimientoIniciado = false;
    public float velocidad = 5f;
    public GameObject Jesus;
    public Vector3 DireccionVelocidad;
    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rend = GetComponent<Renderer>();
    }
    new void Start()
    {

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Estado == EstadoBarco.SeguirBarco)
        {
            return;
        }
        else if (collision.gameObject.CompareTag("Boats"))
        {
            GameObject instantiate =Instantiate(Jesus,gameObject.transform.position,Quaternion.identity);
            instantiate.transform.parent = gameObject.transform;
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

            rb.velocity = DireccionVelocidad * InitialVelocity;
        }
        else if (ObjetoASeguir != null && Estado != EstadoBarco.SeguirBarco)
        {
            Vector2 direccion = (Vector2)ObjetoASeguir.transform.position - (Vector2)transform.position;
            direccion.Normalize();
            rb.velocity = direccion * InitialVelocity;
        }
        if (EstaEsperando == false)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle-180, Vector3.back);
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
        rb.velocity = new Vector2(velocityX, velocityY);
    }
}
