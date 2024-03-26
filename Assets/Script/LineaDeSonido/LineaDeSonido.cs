using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineaDeSonido : MonoBehaviour
{
    [SerializeField] private GameObject ballenaASeguir;

    void FixedUpdate()
    {
        gameObject.transform.position = ballenaASeguir.transform.position;
    }

}
