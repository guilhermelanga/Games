using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Alien : MonoBehaviour
{
    public float timeLeft;
    public float speed = 10;
    public Rigidbody2D alien;
    public Sprite startImage;
    public Sprite altImage;
    private SpriteRenderer spriteRenderer;
    public float secBeforeSpriteChange = 0.5f;

    
    public GameObject alienBullet;

    public float minrateFire = 80.0f;
    public float maxrateFire = 90.0f;
    public float waitFire = 0.0f;

    public Sprite spaceshipExplosion;

    
    

    public GameObject nave;
    public int gameover;
    bool continuar = true;

    // Start is called before the first frame update
    void Start()
    {
        waitFire = Time.time;

        alien = GetComponent<Rigidbody2D>();

        alien.velocity = new Vector2(1, 0) * speed;

        spriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(ChangeAlienSprite());

        waitFire = waitFire + Random.Range(minrateFire, maxrateFire);

    }

    //mudar dirações dos aliens
    void Turn(int direction)
    {
        Vector2 newVelocity = alien.velocity;
        newVelocity.x = speed * direction;
        alien.velocity = newVelocity;
    }

    //mover para baixo depois de atingir a parede
    void MoveDown()
    {
        Vector2 position = transform.position;
        position.y -= 2;
        transform.position = position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name== "LeftWall")
        {
            Turn(1);
            //MoveDown();
        }
        if (collision.gameObject.name == "RightWall")
        {
            Turn(-1);
            //MoveDown();
        }

        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
        }



    }

    public IEnumerator ChangeAlienSprite()
    {
        while (true)
        {
            if (spriteRenderer.sprite == startImage)
            {
                spriteRenderer.sprite = altImage;
            }
            else
            {
                spriteRenderer.sprite = startImage;
            }

            yield return new WaitForSeconds(secBeforeSpriteChange);
        }
    }

    private void FixedUpdate()
    {
        

        gameover = nave.transform.childCount;

        if (gameover <= 0)
        {
            continuar = false;
            //Debug.Log(gameover);
        }

        if (continuar)
        {
            if (Time.time > waitFire)
            {
                waitFire = waitFire + Random.Range(minrateFire, maxrateFire);

                Instantiate(alienBullet, transform.position, Quaternion.identity);
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<SpriteRenderer>().sprite = spaceshipExplosion;
            Destroy(gameObject);
            Object.Destroy(collision.gameObject, 0.5f);
        }
    }

    
}
