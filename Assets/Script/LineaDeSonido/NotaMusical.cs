using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotaMusical : MonoBehaviour
{
    public float rotationSpeed = 50f; // Velocidad de rotaci車n

    private bool rotateClockwise = true; // Variable para controlar la direcci車n de rotaci車n
    private float targetRotation = 90f; // El 芍ngulo de rotaci車n m芍ximo
    [SerializeField] private float[] GradoDeGiro;
    private void Start()
    {
        targetRotation = Random.Range(GradoDeGiro[0], GradoDeGiro[1]);
    }
    void Update()
    {
        // Determinar la direcci車n de rotaci車n
        float rotationDirection = rotateClockwise ? 1f : -1f;

        // Rotar el objeto
        transform.Rotate(0f, 0f, rotationDirection * rotationSpeed * Time.deltaTime);

        // Verificar si alcanzamos el 芍ngulo m芍ximo de rotaci車n
        if (Mathf.Abs(transform.rotation.eulerAngles.z) >= Mathf.Abs(targetRotation))
        {
            targetRotation = Random.Range(GradoDeGiro[0], GradoDeGiro[1]);
            rotateClockwise = !rotateClockwise;

            // Cambiar el 芍ngulo de rotaci車n objetivo
            targetRotation = rotateClockwise ? targetRotation : -targetRotation;
        }
    }
}
