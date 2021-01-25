using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{

    private Animator animCoin;
    [SerializeField]
    private AudioSource coinSound;
    // Start is called before the first frame update
    void Start()
    {
        animCoin = GetComponent<Animator>();
        coinSound = GetComponent<AudioSource>();
        animCoin.Play("Idle");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hero"))
        {
            if (!coinSound.isPlaying)
            {
                coinSound.Play();
            }
            Invoke("AnimAndDestroy", 0.15f);
        }
    }

    void AnimAndDestroy()
    {
        animCoin.Play("Effect");
        Destroy(this.gameObject, 0.40f);
    }
}
