using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SystemaDePuntos : MonoBehaviour
{
    public static SystemaDePuntos instance;
    public int puntos;
    public TMP_Text textMeshPro;
    void Awake()
    {
        instance = this;
    }

    public void SumarPuntos()
    {
        puntos++;
        textMeshPro.text = puntos.ToString();
    }
    
}
