using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogoEfecto : MonoBehaviour
{
    public float tiempoTotal = 2f; // Tiempo total en segundos para el cambio de transparencia
    private float tiempoPasado = 0f; // Tiempo transcurrido desde el inicio
    private Image image;
    private Color colorInicial;
    private Color colorFinal;

    void Start()
    {
        image = GetComponent<Image>(); // Obtener el componente Image
        colorInicial = image.color; // Almacenar el color inicial
        colorFinal = colorInicial; // El color final es igual al inicial
        colorFinal.a = 1f; // Establecer la transparencia m¨¢xima
    }

    void Update()
    {
        // Incrementar el tiempo transcurrido
        tiempoPasado += Time.deltaTime;

        // Calcular el progreso del cambio de transparencia
        float progreso = Mathf.Clamp01(tiempoPasado / tiempoTotal);

        // Interpolar entre el color inicial y el color final
        Color nuevoColor = Color.Lerp(colorInicial, colorFinal, progreso);

        // Asignar el nuevo color a la imagen
        image.color = nuevoColor;

        // Si el tiempo total ha pasado, reiniciar el tiempo
        if (tiempoPasado >= tiempoTotal)
        {
            Invoke("CambioDeEscena", 2f);
        }
    }
    private void CambioDeEscena()
    {
        SceneManager.LoadScene("Play");
    }
}
