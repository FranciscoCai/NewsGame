using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Sprite[] PagTutorial;
    private int NumeroDePag = 0;
    private SpriteRenderer spriteR;
    private void Start()
    {
        spriteR = GetComponent<SpriteRenderer>();
    }
    public void CambioDePagina()
    {
        NumeroDePag++;
        if (NumeroDePag < PagTutorial.Length)
        {
            spriteR.sprite = PagTutorial[NumeroDePag];
        }
        else
        {
            SceneManager.LoadScene(SceneController.Instance.SceneToLoad);
        }
    }
}
