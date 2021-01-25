using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{

    private Animator animChest;
    [SerializeField]
    private GameObject interrogation;

    [SerializeField]
    private Transform hero;
    private bool withKey;

    [SerializeField]
    private Transform menu;
    [SerializeField]
    private bool openChest;

    [SerializeField]
    private GameObject locker;

    [SerializeField]
    private Transform skullPosition;
    [SerializeField]
    private GameObject skull;

    // Start is called before the first frame update
    void Start()
    {
        animChest = GetComponent<Animator>();
        interrogation.SetActive(false);
        Physics2D.IgnoreLayerCollision(16, 17);
    }

    // Update is called once per frame
    void Update()
    {
        withKey = hero.GetComponent<Hero>().getKeyHero;

        openChest = menu.GetComponent<Menu_Controller>().Openchest;



        if (openChest)
        {
            interrogation.SetActive(false);
            animChest.Play("Opening");
            Invoke("LockerCreate", 0.25f);
            Invoke("SkullCreate", 1.25f);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hero"))
        {
            if (withKey)
            {
                //animChest.Play("Opening");
                //GameObject lockerInst = Instantiate(locker, this.transform.position, Quaternion.identity) as GameObject;
                interrogation.SetActive(true);
            }
        }
    }

    private void LockerCreate()
    {
        GameObject lockerInst = Instantiate(locker, this.transform.position, Quaternion.identity) as GameObject;
    }

    private void SkullCreate()
    {
        GameObject skullInst = Instantiate(skull, skullPosition.transform.position, Quaternion.identity) as GameObject;
    }
}
