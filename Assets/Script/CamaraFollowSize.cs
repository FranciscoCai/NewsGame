using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraFollowSize : MonoBehaviour
{
    public List<Transform> targets;
    public float minSize = 5f;
    public float maxSize = 10f;
    public float paddingDistance;
    public float padding = 2f;
    public static CamaraFollowSize instance;
    private Camera cam;
    private void Awake()
    {
        instance = this; 
    }
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        // Verifica si hay objetivos
        if (targets.Count == 0)
            return;

        // Encuentra el punto central entre todos los objetivos
        Vector3 centerPoint = GetCenterPoint();

        // Calcula la distancia entre el punto central y los objetos más lejanos
        float distance = GetGreatestDistance(centerPoint);

        padding = paddingDistance*distance;
        // Calcula el tamaño de la cámara basado en la distancia
        float newSize = distance / 2f + padding;

        // Limita el tamaño de la cámara dentro de los límites especificados
        newSize = Mathf.Clamp(newSize, minSize, maxSize);

        // Asigna el nuevo tamaño de la cámara
        cam.orthographicSize = newSize;
    }

    // Encuentra el punto central entre todos los objetivos
    Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
            return targets[0].position;

        Bounds bounds = new Bounds(targets[0].position, Vector3.zero);
        foreach (Transform target in targets)
        {
            bounds.Encapsulate(target.position);
        }
        return bounds.center;
    }

    // Calcula la distancia entre el punto central y los objetos más lejanos
    float GetGreatestDistance(Vector3 centerPoint)
    {
        float maxDistance = 0f;
        foreach (Transform target in targets)
        {
            float distance = Vector3.Distance(target.position, centerPoint);
            if (distance > maxDistance)
            {
                maxDistance = distance;
            }
        }
        return maxDistance;
    }
}
