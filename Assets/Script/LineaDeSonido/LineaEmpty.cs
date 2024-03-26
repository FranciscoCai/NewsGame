using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineaEmpty : MonoBehaviour
{
    [SerializeField] private Ballena[] ballena;

    public void EfectoDestruir()
    {
        for (int i = 0; i < ballena.Length; i++)
        {
            ballena[i].Estado = EstadoBarco.SeguirBarco;
            ballena[i].EmpezarCorutina();
        }
        LifeSystem.instance.RestarVida();
        Destroy(gameObject);
    }
}
