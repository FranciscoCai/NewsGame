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
        SpaceMovement.SetActive(false);
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

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Boats")||collision.gameObject.CompareTag("Pared"))
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
