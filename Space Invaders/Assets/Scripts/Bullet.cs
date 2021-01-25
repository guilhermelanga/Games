using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Bullet : MonoBehaviour
{
    public float speed = 30;

    private Rigidbody2D bullet;

    public Sprite AlienExplosion;

    public static int boss, score;

    

    // Start is called before the first frame update
    void Start()
    {
        bullet = GetComponent<Rigidbody2D>();

        bullet.velocity = Vector2.up * speed;

        

    }

    

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            Destroy(gameObject);
        }

        if(collision.tag == "Alien")
        {
            Score();

            

            collision.GetComponent<SpriteRenderer>().sprite = AlienExplosion;

            Destroy(gameObject);

            Object.Destroy(collision.gameObject, 0.5f);

            

            

        }

        if (collision.tag == "AlienBullet")
        {
           

            collision.GetComponent<SpriteRenderer>().sprite = AlienExplosion;

            Destroy(gameObject);

            Object.Destroy(collision.gameObject, 0.1f);

        }

        if (collision.tag == "Shield")
        {
            Destroy(gameObject);

            Object.Destroy(collision.gameObject);
        }

        


    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void Score()
    {

        if (boss == 3)
        {
            var textUIComp = GameObject.Find("Score").GetComponent<Text>();

            score = int.Parse(textUIComp.text);

            score += 30;

            textUIComp.text = score.ToString();
        }
        else
        {
            var textUIComp = GameObject.Find("Score").GetComponent<Text>();

            score = int.Parse(textUIComp.text);

            score += 10;

            textUIComp.text = score.ToString();
        }

        
        
        
    }

    
}
