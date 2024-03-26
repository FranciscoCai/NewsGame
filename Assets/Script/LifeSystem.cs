using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeSystem : MonoBehaviour
{
    public static LifeSystem instance;
    [SerializeField] private List<Image> Vida;
    [SerializeField] private Sprite vidaPerdida;
    void Start()
    {
        instance = this;
    }

    public void RestarVida()
    {
        int ultimaVIda = Vida.Count-1;
        if (ultimaVIda == 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        Image vida = Vida[ultimaVIda];
        vida.sprite = vidaPerdida;
        Vida.RemoveAt(ultimaVIda);
    }

}
