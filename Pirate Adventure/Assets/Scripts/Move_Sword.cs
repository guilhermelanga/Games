using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Sword : MonoBehaviour
{
    private float speed = 8f;
    public bool stopMove = false;
    

    //START ANIMATOR
    public Animator animSword;

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }


    // Start is called before the first frame update

    void Start()
    {
        animSword = GetComponent<Animator>();
    }

    void Move()
    {
        Vector3 aux = transform.position;
        aux.x += speed * Time.deltaTime;
        transform.position = aux;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopMove)
        {
            Move();
        }

        if (stopMove)
        {
            ChangeAnim(2);
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            
            stopMove = true;
        }
    }

    //ANIMATION SWORD
    void ChangeAnim(int anim)
    {
        switch (anim)
        {
            case 1:
                animSword.SetInteger("Anim", 1);
                break;
            case 2:
                animSword.SetInteger("Anim", 2);
                break;
            default:
                animSword.SetInteger("Anim", 1);
                break;
        }
    }

}
