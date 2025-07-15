using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialPanel;
    public TMP_Text tutorialText;

    public GameObject ondas;

    private GameObject[] barcos;
    private Vector3[] posicionesIniciales;
    private Rigidbody2D[] rigidbodies;

    private GameObject[] ballenas;
    private GameObject vidasUI;

    private int step = 0;
    private bool tutorialFinished = false;
    private bool waitingForBarcoClick = false;
    private bool waitingForSwipe = false;

    void Start()
    {
        Time.timeScale = 0f;

        barcos = GameObject.FindGameObjectsWithTag("Boats");
        posicionesIniciales = new Vector3[barcos.Length];
        rigidbodies = new Rigidbody2D[barcos.Length];

        for (int i = 0; i < barcos.Length; i++)
        {
            posicionesIniciales[i] = barcos[i].transform.position;
            rigidbodies[i] = barcos[i].GetComponent<Rigidbody2D>();
        }

        ballenas = GameObject.FindGameObjectsWithTag("Whale");
        foreach (var whale in ballenas)
            whale.SetActive(false); 

        GameObject[] vidasObjs = GameObject.FindGameObjectsWithTag("Lifes");
        if (vidasObjs.Length > 0) vidasUI = vidasObjs[0];
        if (vidasUI != null) vidasUI.SetActive(false);

        if (ondas != null) ondas.SetActive(false);

        ShowMessage("Vosotros sois los barcos");
    }

    void Update()
    {
        if (tutorialFinished) return;

        if (step >= 4 && step <= 6 && TapDetected())
        {
            ContinueAfterBallenas();
            return;
        }

        if (waitingForSwipe)
        {
            for (int i = 0; i < barcos.Length; i++)
            {
                if ((barcos[i].transform.position - posicionesIniciales[i]).magnitude > 0.5f ||
                    rigidbodies[i].velocity.magnitude > 0.1f)
                {
                    waitingForSwipe = false;
                    ResumeGame();
                    Invoke(nameof(ShowBallenasMessage), 5f);
                    return;
                }
            }
            return;
        }

        if (waitingForBarcoClick)
        {
            if (TapDetected())
            {
                Vector2 tapPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Collider2D hit = Physics2D.OverlapPoint(tapPos);
                if (hit != null && hit.CompareTag("Boats"))
                {
                    waitingForBarcoClick = false;
                    ShowMessage("Puedes cambiar la dirección del barco en función de donde deslices");
                    waitingForSwipe = true;

                    for (int i = 0; i < barcos.Length; i++)
                        posicionesIniciales[i] = barcos[i].transform.position;
                }
            }
            return;
        }

        if (TapDetected())
        {
            NextStep();
        }
    }

    bool TapDetected()
    {
#if UNITY_EDITOR
        return Input.GetMouseButtonDown(0);
#else
        return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
#endif
    }

    void NextStep()
    {
        step++;
        switch (step)
        {
            case 1:
                ShowMessage("Vuestra misión es llegar al final");
                break;
            case 2:
                ShowMessage("Pulsa encima del barco para avanzar");
                waitingForBarcoClick = true;
                break;
        }
    }

    void ShowBallenasMessage()
    {
        PauseGame();

        foreach (var whale in ballenas)
            whale.SetActive(true); // Activa las ballenas

        ShowMessage("Estás navegando en una zona habitada por ballenas");
        step = 4;
    }

    void ContinueAfterBallenas()
    {
        switch (step)
        {
            case 4:
                ShowMessage("Tienes que evitar que el ruido del barco choque con su canto");
                if (ondas != null) ondas.SetActive(true);
                step = 5;
                break;
            case 5:
                ShowMessage("Si colisionas con las ondas de una ballena pierdes una vida.\nSi chocas directamente con una ballena, pierdes todas.");
                if (vidasUI != null) vidasUI.SetActive(true);
                step = 6;
                break;
            case 6:
                tutorialFinished = true;
                ResumeGame();
                break;
        }
    }

    void ShowMessage(string msg)
    {
        tutorialText.text = msg;
        tutorialPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    void ResumeGame()
    {
        tutorialPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
    }
}
