using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuComienzo : MonoBehaviour
{

    [SerializeField] private string N_BallenaAzul;
    [SerializeField] private string N_BallenaJorobada;
    [SerializeField] private string N_BallenaRorcual;
    public void BallenaAzul()
    {
        SceneManager.LoadScene(N_BallenaAzul);
    }
    public void BallenaJorobada()
    {
        SceneManager.LoadScene(N_BallenaJorobada);
    }
    public void BallenaRorcual()
    {
        SceneManager.LoadScene(N_BallenaRorcual);
    }
}
