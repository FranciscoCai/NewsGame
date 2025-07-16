using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialPanel;
    public TMP_Text tutorialText;

    public GameObject ondas;

    public GameObject[] vidasUI;

    private GameObject[] barcos;
    private Vector3[] posicionesIniciales;
    private Rigidbody2D[] rigidbodies;

    private GameObject[] ballenas;
 

    private int step = 0;
    private bool tutorialFinished = false;
    private bool waitingForBarcoClick = false;
    private bool waitingForSwipe = false;
    private bool sawWhales = false;

    private List<Coroutine> whaleCoroutines = new List<Coroutine>();
    private List<Coroutine> boatCoroutines = new List<Coroutine>();

    void Start()
    {
        Time.timeScale = 0f;

        GameObject[] posiblesBarcos = GameObject.FindGameObjectsWithTag("Boats");

        var listaBarcos = new List<GameObject>();
        var listaRigidbodies = new List<Rigidbody2D>();
        var listaPosiciones = new List<Vector3>();

        foreach (GameObject b in posiblesBarcos)
        {
            Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                listaBarcos.Add(b);
                listaRigidbodies.Add(rb);
                listaPosiciones.Add(b.transform.position);
            }
        }
        foreach (var vida in vidasUI)
        {
            if (vida != null)
                vida.SetActive(false);
        }

        barcos = listaBarcos.ToArray();
        rigidbodies = listaRigidbodies.ToArray();
        posicionesIniciales = listaPosiciones.ToArray();

        ballenas = GameObject.FindGameObjectsWithTag("Whale");
        foreach (var whale in ballenas)
            whale.SetActive(false);

        if (ondas != null) ondas.SetActive(false);

        ShowMessage("TÚ CONTROLAS LOS BARCOS");
        foreach (var barco in barcos)
        {
            Coroutine c = StartCoroutine(LoopFlashAndPulse3D(barco));
            boatCoroutines.Add(c);
        }
    }

    void Update()
    {
        if (tutorialFinished) return;

        if (step >= 4 && step <= 6 && TapDetected() && sawWhales)
        {
            step++;              // Incrementar paso para avanzar diálogo
            ContinueAfterBallenas();  // Mostrar mensaje correspondiente al nuevo paso
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
                ShowMessage("TU MISIÓN ES LLEGAR AL FINAL");
                break;
            case 2:
                ShowMessage("PULSA ENCIMA DE LOS BARCOS PARA MOVERLOS");
                waitingForBarcoClick = true;
                break;
            case 3:
                // Aquí es cuando termina el case 2, así que paramos el parpadeo barcos
                StopBoatEffects();
                break;
        }
    }
    void StopBoatEffects()
    {
        foreach (var coroutine in boatCoroutines)
        {
            if (coroutine != null)
                StopCoroutine(coroutine);
        }
        boatCoroutines.Clear();

        foreach (var barco in barcos)
        {
            Renderer rend = barco.GetComponent<Renderer>();
            if (rend != null)
            {
                Color c = rend.material.color;
                c.a = 1f;  // Opacidad completa
                rend.material.color = c;
            }
            barco.transform.localScale = Vector3.one;
        }
    }

    void ShowBallenasMessage()
    {
        StartCoroutine(ShowBallenasMessageCoroutine());
    }

    IEnumerator ShowBallenasMessageCoroutine()
    {
        
        foreach (var whale in ballenas)
            whale.SetActive(true);

        yield return new WaitForSecondsRealtime(5f);

        // 3. Pausa el juego
        PauseGame();

        foreach (var whale in ballenas)
        {
            Coroutine anim = StartCoroutine(LoopFlashAndPulse(whale));
            whaleCoroutines.Add(anim);
        }

   
        ShowMessage("ESTÁS NAVEGANDO EN UNA ZONA HABITADA POR BALLENAS");
        step = 4;
        sawWhales = true;
    }

    void ContinueAfterBallenas()
    {
        switch (step)
        {
            case 4:
                ShowMessage("TIENES QUE EVITAR QUE EL RUIDO DEL BARCO CHOQUE CON SU CANTO");
                if (ondas != null) ondas.SetActive(true);
                break;

            case 5:
                ShowMessage("SI COLISIONAS CON LAS ONDAS DE UNA BALLENA PIERDES UNA VIDA.");
                foreach (var vida in vidasUI)
                {
                    if (vida != null)
                    {
                        vida.SetActive(true);
                        StartCoroutine(FlashAndPulse(vida, 5));
                    }
                }
                break;

            case 6:
                ShowMessage("SI CHOCAS DIRECTAMENTE CON UNA, PIERDES TODAS.");
                break;

            case 7:
                StopWhaleEffects();
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

    IEnumerator FlashAndPulse(GameObject obj, int pulses = 3, float scaleAmount = 1.3f, float duration = 0.25f)
    {
        if (obj == null) yield break;

        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr == null) yield break;

        Vector3 originalScale = obj.transform.localScale;
        Color originalColor = sr.color;

        for (int i = 0; i < pulses; i++)
        {
            sr.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.3f);
            obj.transform.localScale = originalScale * scaleAmount;
            yield return new WaitForSecondsRealtime(duration);

            sr.color = originalColor;
            obj.transform.localScale = originalScale;
            yield return new WaitForSecondsRealtime(duration);
        }
    }

    IEnumerator LoopFlashAndPulse(GameObject obj, float scaleAmount = 1.3f, float pulseDuration = 0.6f)
    {
        if (obj == null) yield break;

        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr == null) yield break;

        Vector3 originalScale = obj.transform.localScale;
        Color originalColor = sr.color;

        float t = 0f;
        bool fadingOut = true;

        while (true)
        {
            t += Time.unscaledDeltaTime;
            float progress = t / pulseDuration;

            // Calcular alfa (opacidad) gradual
            float alpha = fadingOut ? Mathf.Lerp(1f, 0.3f, progress) : Mathf.Lerp(0.3f, 1f, progress);
            sr.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            // Calcular escala gradual
            float scale = fadingOut ? Mathf.Lerp(1f, scaleAmount, progress) : Mathf.Lerp(scaleAmount, 1f, progress);
            obj.transform.localScale = originalScale * scale;

            if (progress >= 1f)
            {
                t = 0f;
                fadingOut = !fadingOut;
            }

            yield return null; // Espera un frame sin bloquear el tiempo del juego
        }
    }
    IEnumerator LoopFlashAndPulse3D(GameObject obj, float scaleAmount = 1.3f, float pulseDuration = 0.6f)
    {
        if (obj == null) yield break;

        Renderer rend = obj.GetComponent<Renderer>();
        if (rend == null) yield break;

        Vector3 originalScale = obj.transform.localScale;
        Color originalColor = rend.material.color;

        float t = 0f;
        bool fadingOut = true;

        while (true)
        {
            t += Time.unscaledDeltaTime;
            float progress = t / pulseDuration;

            // Opacidad gradual
            float alpha = fadingOut ? Mathf.Lerp(1f, 0.3f, progress) : Mathf.Lerp(0.3f, 1f, progress);

            Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            rend.material.color = newColor;

            // Escala gradual
            float scale = fadingOut ? Mathf.Lerp(1f, scaleAmount, progress) : Mathf.Lerp(scaleAmount, 1f, progress);
            obj.transform.localScale = originalScale * scale;

            if (progress >= 1f)
            {
                t = 0f;
                fadingOut = !fadingOut;
            }

            yield return null;
        }
    }

    void StopWhaleEffects()
    {
        foreach (var coroutine in whaleCoroutines)
        {
            if (coroutine != null)
                StopCoroutine(coroutine);
        }

        foreach (var whale in ballenas)
        {
            if (whale != null)
            {
                SpriteRenderer sr = whale.GetComponent<SpriteRenderer>();
                if (sr != null)
                    sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
                whale.transform.localScale = Vector3.one;
            }
        }

        whaleCoroutines.Clear();
    }
}
