using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private Animator animKey;
    
    // Start is called before the first frame update
    void Start()
    {
        animKey = GetComponent<Animator>();
        
        animKey.Play("Idle");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hero"))
        {
            animKey.Play("Effect");
            Destroy(this.gameObject, 0.40f);
        }
    }

   
}
