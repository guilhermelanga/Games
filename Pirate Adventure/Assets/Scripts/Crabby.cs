using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crabby : MonoBehaviour
{
    //CREATE ANIMATOR
    public Animator animCrabby;

    //SET HERO FOR CHECK LIFE AND POSITION
    [SerializeField]
    private Transform hero;
    private int heroLife;
    private float distance;

    //SET SPEED
    private float speed = 2.75f;

    //SET RIGIDBODY
    private Rigidbody2D rb;

    //CONTROL THE TIME FOR ATTACKS
    private float timeToAttack = 0.5f;


    //ANIM CONTROL
    private string attackAnim = "Attack";
    private string idleAnim = "Idle";
    private string runAnim = "Run";
    private string hitAnim = "Hit";
    private string interrogationAnim = "Interrogation";
    private string deadAnim = "Dead_Ground";

    //SET BOOL FOR CHANGE ANIMATION
    [SerializeField]
    public bool attacking;
    [SerializeField]
    public bool follow;
    [SerializeField]
    public bool hit;
    [SerializeField]
    public bool interrogation;
    [SerializeField]
    private bool canFollow;

    //NEVER FALL IN A HOLE
    [SerializeField]
    private Transform whatIsFloor;
    public bool inTheFloor;

    //BOOL FOR CHANGE FACE
    private bool face;


    //SET LIFE VALUE AND LIFE BAR
    public int life;
    [SerializeField]
    private GameObject lifeBar;
    [SerializeField]
    private Image bar;

    //SET INTERROGATION OBJECT FOR NEVER FLIP
    [SerializeField]
    private GameObject interrogationOb;

    //FIX DOUBLE DAMAGE BUG
    private int damageControl = 0;




    // Start is called before the first frame update
    void Start()
    {
        animCrabby = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        face = true;
        life = 50;
        lifeBar.SetActive(false);
        

}

    // Update is called once per frame
    void Update()
    {

    }

   
    //CHECK FOR COLLISIONS
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HeroAttack"))
        {
            if (life > 0)
            {
                damageControl++;
                if (damageControl == 1)
                {
                    int damage = Random.Range(4, 8);
                    print(damage);
                    life -= damage;
                    hit = true;
                    AnimControl(hitAnim);
                    Invoke("HitComplete", .24f);
                    Invoke("DamageControlReset", 0.5f);
                }
                
            }


        }
        else if (collision.gameObject.CompareTag("SwordAttack"))
        {
            Destroy(collision.gameObject);
            if (life > 0)
            {
                damageControl++;
                if (damageControl == 1)
                {
                    int damage = Random.Range(10, 15);
                    print(damage);
                    life -= damage;
                    hit = true;
                    AnimControl(hitAnim);
                    Invoke("HitComplete", .24f);
                    Destroy(collision.gameObject);
                    Invoke("DamageControlReset", 1f);
                }
                    
            }

        }
        else if (collision.gameObject.CompareTag("InstaKill"))
        {
            life = 0;
        }
    }

    //GET THE HERO DISTANCE
    public float GetDistance()
    {
        return Vector2.Distance(this.transform.position, hero.transform.position);
    }


    //CREATE ANIMATION CONTROL
    void AnimControl(string newAnim)
    {
        animCrabby.Play(newAnim);
    }

    void FixedUpdate()
    {
        //CHECK IF IS INTHEFLOOR IS TRUE
        if (Physics2D.Raycast(whatIsFloor.position, Vector2.down, 0.1f)){
            inTheFloor = true;
        }
        else
        {
            inTheFloor = false;
        }
           
        //GET HERO LIFE
        heroLife = hero.GetComponent<Hero>().getLifeHero;

        //GET DISTANCE FROM HERO
        distance = GetDistance();


        //FILL THE LIFE BAR WITH THE NEW AMOUNT OF LIFE
        bar.fillAmount = (float)(life * 2) / 100;

        //SET THE ACTION IF THE HERO WAS ALIVE
        if (heroLife > 0)
        {
            if (life > 0)
            {
                if (distance < 8 && distance > 7 && inTheFloor)
                {
                    lifeBar.SetActive(false);
                    interrogation = true;
                }
                else if (distance < 7 && distance > 1.5f && canFollow && inTheFloor)
                {
                    ActiveFollow();
                    follow = true;
                    interrogation = false;
                    lifeBar.SetActive(true);

                }
                else if (distance < 1.5f && inTheFloor)
                {
                    follow = false;
                    Attack();
                    lifeBar.SetActive(true);
                }
                else
                {
                    StopFollow();
                    lifeBar.SetActive(false);
                }

                //FIX THE FLIP FACE BUG
                float left = hero.transform.position.x - this.transform.position.x;
                float right = this.transform.position.x - hero.transform.position.x;
                if (right > 0 && right < 0.2f || left > 0 && left < 0.2f)
                {
                    canFollow = false;
                }
                else
                {
                    canFollow = true;
                } 
           

                //CHECK STATUS FOR ANIMATION
                if (inTheFloor && follow && !hit && !interrogation && canFollow)
                {
                    AnimControl(runAnim);
                }
                else if (inTheFloor && attacking && !interrogation)
                {
                    AnimControl(attackAnim);
                }
                else if (inTheFloor && !follow && !attacking && !hit && !interrogation && canFollow)
                {
                    AnimControl(idleAnim);
                }
                else if (interrogation && !hit || !canFollow || !inTheFloor)
                {
                    AnimControl(interrogationAnim);

                }

                //FLIP FACE
                if ((hero.transform.position.x > this.transform.position.x + 0.3f) && face)
                {
                    Flip();
                }
                else if ((hero.transform.position.x < this.transform.position.x - 0.3f) && !face)
                {
                    Flip();
                }
            }
            else
            {
                lifeBar.SetActive(false);
                AnimControl(deadAnim);
                Destroy(this.gameObject, 1.5f);
            }

        }
        else
        {
            lifeBar.SetActive(false);
            AnimControl(interrogationAnim);
        }
    }


    //WAIT FOR ANIMATION COMPLETE
    void AttackComplete()
    {
        attacking = false;
    }

    void HitComplete()
    {
        hit = false;
    }

    //FOLLOW AND STOP FOLLOW CODES
    void ActiveFollow()
    {

        if (hero.transform.position.x < this.transform.position.x)
        {
            transform.Translate(new Vector2(-speed * Time.deltaTime, 0));
        }
        else if (hero.transform.position.x > this.transform.position.x)
        {
            transform.Translate(new Vector2(speed * Time.deltaTime, 0));
        }
    }

    void StopFollow()
    {
        transform.Translate(new Vector2(0, 0));
    }

    //ATTACK SETTINGS, ATTACK ONLY ONTIME FOR SECOND
    void Attack()
    {
        if (timeToAttack > 0)
        {
            timeToAttack -= Time.deltaTime;
            StopFollow();
            //print(timeToAttack);

        }
        else
        {
            attacking = true;
            timeToAttack = 1;

            Invoke("AttackComplete", .2f);

        }
    }


    void Flip()
    {
        face = !face;
        Vector3 scale = this.transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;


        //NEVER FLIP LIFEBAR AND INTERROGATION
        lifeBar.transform.localScale = scale;
        interrogationOb.transform.localScale = scale;

    }

    void DamageControlReset()
    {
        damageControl = 0;
    }
}
