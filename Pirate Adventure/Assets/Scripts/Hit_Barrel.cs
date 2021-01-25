using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit_Barrel : MonoBehaviour
{
    //START ANIMATOR
    //SET THE ANIMATION CONFIGS
    public Animator animBarrel;
    private string hitAnim = "Hit";
    private string idleAnim = "Idle";

    private bool hit;
    private int life;


    //INSTANTIATE A BOX
    [SerializeField]
    public GameObject barrelDestroy;
    [SerializeField]
    private Transform barrelPosition;


    // Start is called before the first frame update
    void Start()
    {
        animBarrel = GetComponent<Animator>();
        life = 15;
    }

    // Update is called once per frame
    void Update()
    {
        if (life > 0)
        {
            if (!hit)
            {
                animBarrel.Play(idleAnim);
            }
            else if (hit)
            {
                animBarrel.Play(hitAnim);
            }
        }
        else
        {
            Destroy(this.gameObject);
            GameObject boxInst = Instantiate(barrelDestroy, barrelPosition.transform.position, Quaternion.identity) as GameObject;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HeroAttack"))
        {
            hit = true;
            int damage = Random.Range(3, 6);
            life -= damage;
            Invoke("HitAnimComplete", 0.30f);
        }else if (collision.gameObject.CompareTag("SwordAttack"))
        {
            hit = true;
            Destroy(collision.gameObject);
            Invoke("HitAnimComplete", 0.30f);
            Invoke("NoMoreLife", 0.30f);
        }
    }

    void HitAnimComplete()
    {
        hit = false;
    }

    void NoMoreLife()
    {
        life = 0;
    }
}
