using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{

    [SerializeField]
    private Transform startPos;
    [SerializeField]
    private Transform endPos;

    [SerializeField]
    private float speed;

    private Animator animShip;

    [SerializeField]
    private bool animToWind;

    private float distance;

    [SerializeField]
    private bool heroOnShip;

    private bool idle;


    // Start is called before the first frame update
    void Start()
    {
        animShip = GetComponent<Animator>();
        animToWind = true;
        idle = true;

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.MoveTowards(transform.position, endPos.position, speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(startPos.position, endPos.position);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hero"))
        {
            heroOnShip = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hero"))
        {
            heroOnShip = false;
            animToWind = true;
        }
    }



    void FixedUpdate()
    {

        distance = GetDistance();

        if(distance>0 && heroOnShip)
        {
            speed = 3.5f;
        }
        else
        {
            speed = 0;
        }

        

        if(distance!=0 && heroOnShip && animToWind)
        {
            animShip.Play("To_Wind");
            Invoke("AnimToWind", 0.18f);

        }else if(distance!=0 && !animToWind && heroOnShip)
        {
            animShip.Play("Wind");
        }else if(heroOnShip && distance == 0 && !idle)
        {
            animShip.Play("To_No_Wind");
            Invoke("IdleAnim", 0.18f);
        }else if((heroOnShip && distance == 0 && idle) || (!heroOnShip && distance == 0 && idle) || (!heroOnShip && distance!= 0 && idle))
        {
            animShip.Play("Idle");
        }

    }

    void AnimToWind()
    {
        animToWind = false;
    }

    public float GetDistance()
    {
        return Vector2.Distance(this.transform.position, endPos.transform.position);
    }

    void IdleAnim()
    {
        idle = true;
    }
}
