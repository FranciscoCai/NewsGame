using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuComienzo : MonoBehaviour
{
    public Menu menu;
    [SerializeField] private string N_BallenaAzul;
    [SerializeField] private string N_BallenaJorobada;
    [SerializeField] private string N_BallenaRorcual;
    [SerializeField] private string N_Cinematica;
    [SerializeField] private string N_Tutorial;
    [SerializeField] private string N_Resume;
    [SerializeField] private string N_Restart;

    void Start()
    {
        if (menu == null)
        {
            menu = FindObjectOfType<Menu>();
        }
    }

    public void BallenaAzul()
    {
        SceneController.Instance.SceneToLoad = N_BallenaAzul;
        SceneManager.LoadScene(N_Tutorial);
    }
    public void BallenaJorobada()
    {
        SceneController.Instance.SceneToLoad = N_BallenaJorobada;
        SceneManager.LoadScene(N_Tutorial);
    }
    public void BallenaRorcual()
    {
        SceneController.Instance.SceneToLoad = N_BallenaRorcual;
        SceneManager.LoadScene(N_Tutorial);
    }
    public void Cinematica()
    {
        SceneManager.LoadScene(N_Cinematica);
    }
    public void Resume()
    {
       menu.Pausa();
    }
    public void Restart()
    {
        SceneManager.LoadScene(N_Restart);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
