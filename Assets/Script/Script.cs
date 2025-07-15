using NUnit;
using System.Collections;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.Net.WebRequestMethods;

public class Script : Movimiento
{
    [SerializeField] private GameObject SpaceMovement;
    [SerializeField] private LayerMask layerMaskBarco;
    [SerializeField] private LayerMask layerMaskMover;
    [SerializeField] private float Velocity;
    [SerializeField] private float acceleration = 5f;      // Unidades por segundo^2
    [SerializeField] private float angularSpeed = 180f;    // Grados por segundo
    private Coroutine ChangeDirectionVelocityCoroutine;
    private void OnMouseDown()
    {
        Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero, Mathf.Infinity,layerMaskBarco);
        if (hit.collider == null)
        { return; }
        if (hit.collider.gameObject == this.gameObject)
        {
            Handheld.Vibrate();
            SpaceMovement.SetActive(true);
        }
    }
    private void OnMouseUp()
    {
        if(ChangeDirectionVelocityCoroutine != null)
        {
            StopCoroutine(ChangeDirectionVelocityCoroutine);
            ChangeDirectionVelocityCoroutine = null;
        }
        Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero, Mathf.Infinity, layerMaskMover);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject == SpaceMovement)
            {
                Handheld.Vibrate();
                Vector2 direccion = (clickPosition - (Vector2)transform.position).normalized;
                float distancia = Vector2.Distance(transform.position, clickPosition);
                ChangeDirectionVelocityCoroutine = StartCoroutine(ChangeDirectionVelocity(direccion, InitialVelocity * Mathf.Pow(2, distancia)));
            }
        }
        else
        {
            Vector2 direction = ((Vector2)SpaceMovement.transform.position - clickPosition).normalized;
            RaycastHit2D hitP = Physics2D.Raycast(clickPosition, direction, Mathf.Infinity, layerMaskMover);
            if (hitP.collider != null)
            {
                Handheld.Vibrate();
                // Verificar si el rayo golpea el objeto final
                if (hitP.collider.gameObject == SpaceMovement)
                {
                    Vector2 direccion = (hitP.point - (Vector2)transform.position).normalized;
                    float distancia = Vector2.Distance(transform.position, hitP.point);
                    ChangeDirectionVelocityCoroutine = StartCoroutine(ChangeDirectionVelocity(direccion,InitialVelocity * Mathf.Pow(2, distancia)));
                }
            }
        }
        SpaceMovement.SetActive(false);
    }
    private IEnumerator ChangeDirectionVelocity(Vector2 finalDirection, float finalSpeed)
    {
        finalDirection.Normalize();
        float currentSpeed = rb.velocity.magnitude;
        Vector2 currentDirection = rb.velocity.sqrMagnitude > 0.01f ? rb.velocity.normalized : transform.up;

        while (true)
        {

            float speedDelta = finalSpeed - currentSpeed;
            float maxSpeedChange = acceleration * Time.deltaTime;
            if (Mathf.Abs(speedDelta) > maxSpeedChange)
                currentSpeed += Mathf.Sign(speedDelta) * maxSpeedChange;
            else
                currentSpeed = finalSpeed;


            float angle = Vector2.SignedAngle(currentDirection, finalDirection);
            float maxAngleChange = angularSpeed * Time.deltaTime;
            if (Mathf.Abs(angle) > maxAngleChange)
                angle = Mathf.Sign(angle) * maxAngleChange;

            currentDirection = Quaternion.Euler(0, 0, angle) * currentDirection;
            currentDirection.Normalize();



            rb.velocity = currentDirection * currentSpeed;
            transform.up = currentDirection;



            if (Mathf.Approximately(currentSpeed, finalSpeed) && Vector2.Angle(currentDirection, finalDirection) < 0.5f)
                break;

            yield return null;
        }


        rb.velocity = finalDirection * finalSpeed;
        transform.up = finalDirection;
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
