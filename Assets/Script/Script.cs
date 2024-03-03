using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Script : MonoBehaviour
{
    [SerializeField] private GameObject SpaceMovement;
    [SerializeField] private LayerMask layerMask;
    [SerializeField]  private float InitialVelocity;
    [SerializeField]  private float Velocity;
    private Rigidbody2D rb;
    private void OnMouseDown()
    {
        Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero, Mathf.Infinity);
        if (hit.collider.gameObject == this.gameObject)
        {
            SpaceMovement.SetActive(true);
        }
    }
    private void OnMouseUp()
    {
        Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero, Mathf.Infinity, layerMask);
        SpaceMovement.SetActive(false);
        if (hit.collider != null)
        {
            if ( hit.collider.gameObject == SpaceMovement)
            {
                Vector2 direccion = (clickPosition - (Vector2)transform.position).normalized;
                float distancia = Vector2.Distance(transform.position, clickPosition);
                rb.velocity = direccion* Mathf.Pow(2, distancia)*Velocity;

                float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
                Quaternion rotacion = Quaternion.Euler(0f, 0f, angulo-90);
                transform.rotation = rotacion;
            }
        }
        else
        {

        }
    }
    private void Start()
    {
        rb=gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * InitialVelocity;
    }
}
