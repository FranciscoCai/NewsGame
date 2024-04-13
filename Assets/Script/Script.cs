using NUnit;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Script : Movimiento
{
    [SerializeField] private GameObject SpaceMovement;
    [SerializeField] private LayerMask layerMaskBarco;
    [SerializeField] private LayerMask layerMaskMover;
    [SerializeField] private float Velocity;
    private void OnMouseDown()
    {
        Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero, Mathf.Infinity,layerMaskBarco);
        if (hit.collider == null)
        { return; }
        if (hit.collider.gameObject == this.gameObject)
        {
            SpaceMovement.SetActive(true);
        }
    }
    private void OnMouseUp()
    {
        Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero, Mathf.Infinity, layerMaskMover);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject == SpaceMovement)
            {
                Vector2 direccion = (clickPosition - (Vector2)transform.position).normalized;
                float distancia = Vector2.Distance(transform.position, clickPosition);
                transform.up = direccion;
                rb.velocity = transform.up * InitialVelocity* Mathf.Pow(2, distancia);
            }
        }
        else
        {
            Vector2 direction = ((Vector2)SpaceMovement.transform.position - clickPosition).normalized;
            RaycastHit2D hitP = Physics2D.Raycast(clickPosition, direction, Mathf.Infinity, layerMaskMover);
            if (hitP.collider != null)
            {
                // Verificar si el rayo golpea el objeto final
                if (hitP.collider.gameObject == SpaceMovement)
                {
                    Vector2 direccion = (hitP.point - (Vector2)transform.position).normalized;
                    float distancia = Vector2.Distance(transform.position, hitP.point);
                    transform.up = direccion;
                    rb.velocity = transform.up * InitialVelocity * Mathf.Pow(2, distancia);
                }
            }
        }
        SpaceMovement.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Pared"))
        {
            SceneManager.LoadScene("GameOver");
        }
        if (collision.gameObject.CompareTag("Boats"))
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
