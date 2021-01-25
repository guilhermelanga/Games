using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Barrel : MonoBehaviour
{

    [SerializeField]
    private GameObject key;
    [SerializeField]
    private Transform keyPosition;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("CreateKey", 0.5f);
        Destroy(this.gameObject, 1.5f);
    }

    void CreateKey()
    {
        GameObject keyInst = Instantiate(key, keyPosition.transform.position, Quaternion.identity) as GameObject;
    }

    
}
