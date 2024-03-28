using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotaMusical : MonoBehaviour
{
    public float rotationSpeed = 50f; // Velocidad de rotaci��n

    private bool rotateClockwise = true; // Variable para controlar la direcci��n de rotaci��n
    private float targetRotation = 90f; // El ��ngulo de rotaci��n m��ximo
    [SerializeField] private float[] GradoDeGiro;
    private void Start()
    {
        targetRotation = Random.Range(GradoDeGiro[0], GradoDeGiro[1]);
    }
    void Update()
    {
        // Determinar la direcci��n de rotaci��n
        float rotationDirection = rotateClockwise ? 1f : -1f;

        // Rotar el objeto
        transform.Rotate(0f, 0f, rotationDirection * rotationSpeed * Time.deltaTime);

        // Verificar si alcanzamos el ��ngulo m��ximo de rotaci��n
        if (Mathf.Abs(transform.rotation.eulerAngles.z) >= Mathf.Abs(targetRotation))
        {
            targetRotation = Random.Range(GradoDeGiro[0], GradoDeGiro[1]);
            rotateClockwise = !rotateClockwise;

            // Cambiar el ��ngulo de rotaci��n objetivo
            targetRotation = rotateClockwise ? targetRotation : -targetRotation;
        }
    }
}
