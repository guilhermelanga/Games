using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Seashell : MonoBehaviour
{

    //CREATE ANIMATOR
    public Animator animSeashell;

    //SET HERO FOR CHECK LIFE AND POSITION
    [SerializeField]
    private Transform hero;
    private int heroLife;
    private float distance;

    //BOOL FOR CHANGE FACE
    private bool face;

    //SET LIFE VALUE AND LIFE BAR
    public int life;
    [SerializeField]
    private GameObject lifeBar;
    [SerializeField]
    private Image bar;

    //SET BOOL FOR CHANGE ANIMATION
    [SerializeField]
    public bool attacking;
    [SerializeField]
    public bool hit;

    //ANIM CONTROL
    private string attackAnim = "Attack";
    private string idleAnim = "Idle";
    private string hitAnim = "Hit";
    private string winAnim = "Win";


    //FIX DOUBLE DAMAGE BUG
    private int damageControl = 0;

    //SET THE INSTATIENTE SHEASHELL DESTROYED
    [SerializeField]
    private GameObject seashellDestroy;

    //CONTROL THE TIME FOR ATTACKS
    private float timeToAttack = 2f;
    [SerializeField]
    private GameObject pearl;
    [SerializeField]
    private Transform pearlPosition;
    int numberOfPearl = 0;


    // Start is called before the first frame update
    void Start()
    {
        animSeashell = GetComponent<Animator>();
        face = true;
        life = 50;
    }

    // Update is called once per frame
    void Update()
    {
        if (life <= 0)
        {
            Destroy(this.gameObject);
            GameObject seashellInst = Instantiate(seashellDestroy, this.transform.position, Quaternion.identity) as GameObject;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HeroAttack"))
        {
            if (life > 0)
            {
                damageControl++;
                if (damageControl == 1)
                {
                    int damage = Random.Range(10, 15);
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
                    Destroy(collision.gameObject);
                    life = 0;
                    hit = true;
                    AnimControl(hitAnim);
                    Invoke("HitComplete", .24f);
                    Invoke("DamageControlReset", 0.5f);
                }

            }
        }
    }

    void HitComplete()
    {
        hit = false;
    }

    void DamageControlReset()
    {
        damageControl = 0;
    }

    void FixedUpdate()
    {
        //GET HERO LIFE
        heroLife = hero.GetComponent<Hero>().getLifeHero;

        //GET DISTANCE FROM HERO
        distance = GetDistance();
        

        //FILL THE LIFE BAR WITH THE NEW AMOUNT OF LIFE
        bar.fillAmount = (float)(life * 2f) / 100;

        if (heroLife > 0)
        {
            if (life > 0)
            {
                //CHECK DISTANCE FOR START ATTACKING
                if (distance < 40)
                {
                    Attack();
                }
           

                //CONTROL ANIMATIONS
                if(!hit && !attacking)
                {
                    AnimControl(idleAnim);
                }else if(!hit && attacking)
                {
                    AnimControl(attackAnim);
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
        }
        else
        {
            lifeBar.SetActive(false);
            AnimControl(winAnim);
            
        }
       


    }

    void Attack()
    {
       
        if (timeToAttack > 0)
        {
            timeToAttack -= Time.deltaTime;
        }
        else
        {
            attacking = true;
            Invoke("AttackComplete", .30f);
            numberOfPearl++;
            if (numberOfPearl == 1)
            {
                Invoke("CreatePearl", 0.35f);
            }
        }
    }

    void CreatePearl()
    {
        GameObject swordInst = Instantiate(pearl, pearlPosition.transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
        swordInst.GetComponent<Move_Pearl>().Speed *= transform.localScale.x;

    }

    void AttackComplete()
    {
        attacking = false;
        timeToAttack = 2f;
        numberOfPearl = 0;
    }


    //GET THE HERO DISTANCE
    public float GetDistance()
    {
        return Vector2.Distance(this.transform.position, hero.transform.position);
    }

    //CREATE ANIMATION CONTROL
    void AnimControl(string newAnim)
    {
        animSeashell.Play(newAnim);
    }

    void Flip()
    {
        face = !face;
        Vector3 scale = this.transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;


        //NEVER FLIP LIFEBAR
        lifeBar.transform.localScale = scale;
    }
}
