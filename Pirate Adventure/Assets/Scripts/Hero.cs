using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    //SET HERO DIRECTION
    [SerializeField]
    private int direction = 0;

    private Rigidbody2D rb;

    //SET HERO JUMP FORCE
    public float jumpForce = 0;

    //WITH SWORD
    [SerializeField]
    public bool withSword = true;

    // FLOOR CHECK
    [SerializeField]
    public bool inTheFloor;
    public Transform inTheFloorCheck;
    public float inTheFloorRadius = 0.1f;
    public LayerMask whatIsFloor;

    //THROW SWORD
    [SerializeField]
    private GameObject sword;
    [SerializeField]
    private GameObject swordPosition;
    [SerializeField]
    private bool instantiateSword = false;
    private int swordControl;


    //SET FILEDS FOR LIFE BAR
    [SerializeField]
    private int life;
    [SerializeField]
    private Image lifeBar;


    //MOVEMENT AFETR HITS
    private float force = 0.4f;

    //THROW THE FOR SIDE OF ENEMY
    private float headForceDamage = 0.9f;

    //CHECK IF THE LIFE IS BIGGER TAHN 0
    private bool alive;

    //START ANIMATOR
    //SET THE ANIMATION CONFIGS
    public Animator animHero;
    private string deadAnim = "Dead";
    private string runSwordAnim = "Run_Sword";
    private string runAnim = "Run";
    private string idleSwordAnim = "Idle_Sword";
    private string idleAnim = "Idle";
    private string throwSwordAnim = "Throw_Sword";
    private string hitSwordAnim = "Hit_Sword";
    private string hitAnim = "Hit";
    private string jumpAnim = "Jump";
    private string jumpSwordAnim = "Jump_Sword";
    private string attackAnim = "Attack";

    //SET BOOL FOR ANIMATION
    private bool running;
    private bool throwSword;
    private bool canAttack = true;
    [SerializeField]
    private bool attacking;
    [SerializeField]
    private bool hit;
  
    //CHANGE FACE
    public bool face;

    

    //AMOUNT OF COINS COLLECTED
    private int coins;
    [SerializeField]
    private Text coinsText;

    [SerializeField]
    private AudioSource runSound;

    [SerializeField]
    private GameObject menu;


    [SerializeField]
    private bool withKey;
    [SerializeField]
    private GameObject key;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animHero = GetComponent<Animator>();
        life = 100;
        face = true;
        menu.SetActive(false);
        key.SetActive(false);
        withKey = false;
        swordControl = 0;

    }

    //OTHER OBJECTS GET THE LIFE
    public int getLifeHero
    {
        get { return life; }
    }

    public bool getKeyHero
    {
        get { return withKey; }
    }

    // Update is called once per frame
    void Update()
    {
        //  APENAS PARA TESTE REMOVER!!!!!!!!
        /*if (Input.GetKey(KeyCode.Space))
        {
            
            
            instantiateSword = true;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            direction = 4;
            if (!face)
            {
                Flip();
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            direction = -4;
            if (face)
            {
                Flip();
            }
        }
        else { direction = 0; }

        /*if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }*/


        //CHECK IF THE HERO WAS ALIVE AND SET MOVEMENT
        if (life>0)
        {
            alive = true;
            Move();
        }
        else
        {
            alive = false;
            AnimControl(deadAnim);
            Invoke("ShowMenu", 2f);
        }

        //CALL THE CODE FOR INSTANTIATE SWORD
        if (throwSword && withSword)
        {
            instantiateSword = false;
            swordControl++;
            if (swordControl == 1)
            {
                Invoke("CreateSword", .2f);
            }
            
        }

    }


    //SET ALL VIRTUAL BUTTONS
    //SET STOP FOR THE DIRECTIONAL BUTTONS
    public void Stop()
    {
        direction = 0;
    }

    //SET VIRTUAL JOYSTICK LEFT BUTTON
    public void Left()
    {
        direction = -5;
    }

    //SET VIRTUAL JOYSTICK Right BUTTON
    public void Right()
    {
        direction = 5;

    }

    //SET THE JUMP BUTTON
    public void Jump()
    {

        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

    }

    //SET THROW SWORD BUTTON
    public void ThrowSword()
    {
        if (withSword)
        {
            throwSword = true;
        }
    }

    public void Attack()
    {
        if (withSword && canAttack)
        {
            attacking = true;
        }
    }

    //SET ANIMATIONS
    void AnimControl(string newAnim)
    {
        animHero.Play(newAnim);
    }

    //CREATE GIZMOS FOR THE FLOOR CHECK GAME OBJECT
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(inTheFloorCheck.position, inTheFloorRadius);
    }

    //MOVE HERO
    void Move()
    {
        transform.Translate(direction * Time.deltaTime, 0, 0);
    }





    private void FixedUpdate()
    {
        //FILL THE LIFE BAR WITH THE CORRECT LIFE
        lifeBar.fillAmount = (float)life / 100;

        //CHECK IF IS INTHEFLOOR IS TRUE
        inTheFloor = Physics2D.OverlapCircle(inTheFloorCheck.position, inTheFloorRadius, whatIsFloor);

        //CHECK KEY
        if (withKey)
        {
            key.SetActive(true);
        }

        if (alive)
        {

            //FLIP FOR THE FACE GO TO RIGHT DIRECTION
            if (direction > 0 && !face)
            {
                Flip();
            }
            else if (direction < 0 && face)
            {
                Flip();
            }

            //SET JUMP FORCE
            if (inTheFloor)
            {
                jumpForce = 14f;
            }
            else
            {
                jumpForce = 0;
            }

            //SET BOOL FOR RUN ANIM
            if (direction != 0 && inTheFloor)
            {
                running = true;
            }
            else
            {
                running = false;
            }



            //CONTROL ANIMATIONS
            //IDLE
            if (inTheFloor && !running && !throwSword && !hit && !attacking)
            {
                if (withSword)
                {
                    AnimControl(idleSwordAnim);
                }
                else
                {
                    AnimControl(idleAnim);
                }

            }//RUNNING
            else if (running && !throwSword && !hit && !attacking)
            {
                if (withSword)
                {
                    
                    AnimControl(runSwordAnim);
                    
                }
                else
                {
                    AnimControl(runAnim);
                }

            }//THROW SWORD
            else if (throwSword && withSword && !hit && !attacking)
            {
                AnimControl(throwSwordAnim);
                withSword = false;
                Invoke("ThrowSwordComplete", 0.25f);
            }//HIT
            else if (hit)
            {
                if (withSword)
                {
                    AnimControl(hitSwordAnim);
                }
                else
                {
                    AnimControl(hitAnim);
                }
            }//JUMP
            else if(!inTheFloor && !hit)
            {
                if (withSword)
                {
                    AnimControl(jumpSwordAnim);
                }
                else
                {
                    AnimControl(jumpAnim);
                }
            }//ATTACK
            else if(attacking && !hit && inTheFloor &&canAttack)
            {
                canAttack = false;
                AnimControl(attackAnim);
                Invoke("AttackComplete", .4f);
            }

                    

        }
        else
        {
            jumpForce = 0;
        }

    }

    //FLIP FACE OF THE HERO
    void Flip()
    {
        face = !face;
        Vector3 scale = this.transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    

    void CreateSword()
    {
        swordControl = 0;
        if (face)
        {
            GameObject swordInst = Instantiate(sword, swordPosition.transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
            swordInst.GetComponent<Move_Sword>().Speed *= transform.localScale.x;
        }
        else if (!face)
        {
            GameObject swordInst = Instantiate(sword, swordPosition.transform.position, Quaternion.Euler(0f, 180f, 0f)) as GameObject;
            swordInst.GetComponent<Move_Sword>().Speed *= transform.localScale.x;
        }
    }



    void OnTriggerEnter2D(Collider2D collision)
    {
        //DAMAGE FROM CRABBY ATTACK
        if (collision.gameObject.CompareTag("CrabbyAttack"))
        {
            int damage = Random.Range(2, 5);
            life -= damage;
            if (this.transform.position.x < collision.transform.position.x)
            {
                transform.Translate(-force, 0, 0);
            }
            else
            {
                transform.Translate(force, 0, 0);
            }
            hit = true;
            Invoke("HitComplete", 0.15f);
        }
        //DAMAGE FROM FIERCE ATTACK
        else if (collision.gameObject.CompareTag("FierceAttack"))
        {
            int damage = Random.Range(2, 5);
            life -= damage;
            if (this.transform.position.x < collision.transform.position.x)
            {
                transform.Translate(-force, 0, 0);
            }
            else
            {
                transform.Translate(force, 0, 0);
            }
            hit = true;
            Invoke("HitComplete", 0.15f);
        }
        //DAMAGE FROM PINK ATTACK
        if (collision.gameObject.CompareTag("PinkAttack"))
        {
            int damage = Random.Range(2, 5);
            life -= damage;
            if (this.transform.position.x < collision.transform.position.x)
            {
                transform.Translate(-force, 0, 0);
            }
            else
            {
                transform.Translate(force, 0, 0);
            }
            hit = true;
            Invoke("HitComplete", 0.15f);
        }
        //DAMAGE FOR LANDING ON CRABBY HEAD
        else if (collision.gameObject.CompareTag("CrabbyHeadDamage"))
        {
            if (!inTheFloor)
            {
                int damage = Random.Range(4, 6);
                life -= damage;
                if (this.transform.position.x < collision.transform.position.x)
                {
                    transform.Translate(-headForceDamage, 0, 0);
                }
                else
                {
                    transform.Translate(headForceDamage, 0, 0);
                }
                hit = true;
                Invoke("HitComplete", 0.15f);
            }
        }
        //DAMAGE FOR LANDING ON FIERCE HEAD
        else if (collision.gameObject.CompareTag("FierceHeadDamage"))
        {
            if (!inTheFloor)
            {
                int damage = Random.Range(4, 6);
                life -= damage;
                if (this.transform.position.x < collision.transform.position.x)
                {
                    transform.Translate(-headForceDamage, 0, 0);
                }
                else
                {
                    transform.Translate(headForceDamage, 0, 0);
                }
                hit = true;
                Invoke("HitComplete", 0.15f);
            }
        }
         //DAMAGE FOR LANDING ON PINK HEAD
        else if (collision.gameObject.CompareTag("PinkHeadDamage"))
        {
            if (!inTheFloor)
            {
                int damage = Random.Range(4, 6);
                life -= damage;
                if (this.transform.position.x < collision.transform.position.x)
                {
                    transform.Translate(-headForceDamage, 0, 0);
                }
                else
                {
                    transform.Translate(headForceDamage, 0, 0);
                }
                hit = true;
                Invoke("HitComplete", 0.15f);
            }
        }
        //DAMAGE FOR LANDING ON SEASHELL HEAD
        else if (collision.gameObject.CompareTag("SeashellHeadDamage"))
        {
            if (!inTheFloor)
            {
                int damage = Random.Range(4, 6);
                life -= damage;
                if (this.transform.position.x < collision.transform.position.x)
                {
                    transform.Translate(-headForceDamage, 0, 0);
                }
                else
                {
                    transform.Translate(headForceDamage, 0, 0);
                }
                hit = true;
                Invoke("HitComplete", 0.15f);
            }
        }
        //DAMAGE FOR PEARL AMMUNITION
        else if (collision.gameObject.CompareTag("Pearl"))
        {
            int damage = 10;
            life -= damage;
            if (this.transform.position.x < collision.transform.position.x)
            {
                transform.Translate(-force, 0, 0);
            }
            else
            {
                transform.Translate(force, 0, 0);
            }
            hit = true;
            Invoke("HitComplete", 0.15f);
        }
        //DAMAGE FROM DEATH SPIKES
        else if (collision.gameObject.CompareTag("InstaKill"))
        {
            life = 0;
        }
        //CHECK FOR SKULL TRIGGER
        else if (collision.gameObject.CompareTag("Skull"))
        {
            menu.SetActive(true);
        }
        //GET LIFE POTION
        else if (collision.gameObject.CompareTag("LifePotion"))
        {
            if (life < 100)
            {
                int getLife = Random.Range(20, 30);
                life += getLife;
                Destroy(collision.gameObject);
                //incluir uma nova animação para a lifepotion!!!!!!!
                //
                //
                //
                //
                //
                //
                //
            }
        }
        //GET NEW SWORD
        else if(collision.gameObject.CompareTag("Sword_Get")&& !withSword)
        {
            withSword = true;
            Destroy(collision.gameObject);
        }else if (collision.gameObject.CompareTag("Coin"))
        {
            coins++;
            coinsText.text = coins.ToString();
        }else if (collision.gameObject.CompareTag("Key"))
        {
            withKey = true;
        }


        if (collision.gameObject.CompareTag("Ship"))
        {
            this.transform.parent = collision.gameObject.transform;
        }
            
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ship"))
        {
            this.transform.parent = null;
        }
    }

    void ThrowSwordComplete()
    {
        throwSword = false;
        instantiateSword = true;
    }

    void HitComplete()
    {
        hit = false;
    }

    void AttackComplete()
    {
        attacking = false;
        canAttack = true;
    }

    void ShowMenu()
    {
        menu.SetActive(true);
    }

}
