using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Box : MonoBehaviour
{

    [SerializeField]
    public GameObject sword;
    [SerializeField]
    public GameObject lifePotion;

    [SerializeField]
    private Transform instPosition;


    // Start is called before the first frame update
    void Start()
    {
        int number = Random.Range(0, 3);
        StartCoroutine(ChooseObject(number));
        Destroy(this.gameObject, 1.5f);
    }

    IEnumerator ChooseObject(int number)
    {
        yield return new WaitForSeconds(0.5f);

        switch (number)
        {
            case 1:
                GameObject swordInst = Instantiate(sword, instPosition.transform.position, Quaternion.identity) as GameObject;
                break;

            case 2:
                GameObject lifePotionInst = Instantiate(lifePotion, instPosition.transform.position, Quaternion.identity) as GameObject;
                break;

            default:
                GameObject lifePotionInst1 = Instantiate(lifePotion, instPosition.transform.position, Quaternion.identity) as GameObject;
                break;

        }
    }

   
}
