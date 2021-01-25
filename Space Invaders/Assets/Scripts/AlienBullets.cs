using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlienBullets : MonoBehaviour
{
    private Rigidbody2D alienBullet;
    public float speed = 30;
    public Sprite SpaceshipExplosion;
    

    // Start is called before the first frame update
    void Start()
    {
       

        alienBullet = GetComponent<Rigidbody2D>();

        alienBullet.velocity = Vector2.down * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<SpriteRenderer>().sprite = SpaceshipExplosion;

            Destroy(gameObject);

            Object.Destroy(collision.gameObject, 0.5f);
           

        }

        if (collision.tag == "Shield")
        {
            Destroy(gameObject);

            Object.Destroy(collision.gameObject);
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
