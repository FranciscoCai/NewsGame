using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public EstadoBarco Estado;

    private Camera camara;
    [SerializeField]  private Renderer rend;

    [SerializeField] private GameObject ObjetoASeguir;
    [SerializeField] private LineaEmpty lineaDeSonido;
    [SerializeField] private GameObject Mareo;

    [SerializeField] protected float InitialVelocity;
    [SerializeField] private float TiempoDeEspera;

    private bool EstaEsperando = false;
    private bool movimientoIniciado = false;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        camara = CamaraFollowSize.instance.gameObject.GetComponent<Camera>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Estado == EstadoBarco.SeguirBarco)
        {
            return;
        }
        else if (collision.gameObject.CompareTag("Boats"))
        {
            Estado = EstadoBarco.SeguirBarco;
            lineaDeSonido.EfectoDestruir();
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
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Whale"))
        {
            Destroy(lineaDeSonido.gameObject);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Boats"))
        {
            GameManager.instance.lose = true;
        }
    }
    private void Update()
    {
        if (rend.isVisible && !movimientoIniciado)
        {
            movimientoIniciado = true;
            rb.velocity = transform.up * InitialVelocity;
        }
        if (ObjetoASeguir != null && Estado != EstadoBarco.SeguirBarco)
        {
            Vector2 direccion = (Vector2)ObjetoASeguir.transform.position - (Vector2)transform.position;
            direccion.Normalize();
            transform.up = direccion;
            rb.velocity = InitialVelocity*direccion*3;
        }
        if (EstaEsperando == false)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, transform.up);
        }
    }
    private IEnumerator GiroAleatorio()
    {
        GameObject mareo = Instantiate(Mareo,gameObject.transform.position, Quaternion.identity);
        mareo.transform.parent = gameObject.transform;
        EstaEsperando = true;
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(TiempoDeEspera);
        EstaEsperando = false;
        Quaternion rotacionActual = transform.rotation;
        float angleChange = Random.Range(90f, 270f);
        Quaternion rotacionNueva = Quaternion.Euler(0, 0, rotacionActual.eulerAngles.z + angleChange);
        // Convierte el ¨¢ngulo a radianes
        transform.rotation = rotacionNueva;
        rb.velocity = transform.up * InitialVelocity;
    }
    public void EmpezarCorutina()
    {
        StartCoroutine(GiroAleatorio());
    }

}
