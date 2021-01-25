using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject nave, GameOverText, restartbutton;
    public int quantos;
    // Start is called before the first frame update
    void Start()
    {
        GameOverText.SetActive(false);
        restartbutton.SetActive(false);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        quantos = nave.transform.childCount;
        //Debug.Log(quantos);

        if (quantos <= 0)
        {
            GameOverText.SetActive(true);
            restartbutton.SetActive(true);
        }
    }
}
