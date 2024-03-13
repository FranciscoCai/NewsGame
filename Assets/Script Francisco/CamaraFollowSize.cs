using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraFollowSize : MonoBehaviour
{
    public Transform target1;
    public Transform target2;
    public float minSize = 5f;
    public float maxSize = 10f;
    public float padding = 2f;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        // Calcula la distancia entre los dos objetos
        float distance = Vector3.Distance(target1.position, target2.position);

        // Calcula el tamaño de la cámara basado en la distancia
        float newSize = distance / 2f + padding;

        // Limita el tamaño de la cámara dentro de los límites especificados
        newSize = Mathf.Clamp(newSize, minSize, maxSize);

        // Asigna el nuevo tamaño de la cámara
        cam.orthographicSize = newSize;
    }
}
