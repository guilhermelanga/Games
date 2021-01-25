using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Pearl : MonoBehaviour
{

    private float speed = -12f;
    [SerializeField]
    private GameObject pearlDestroy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Move()
    {
        Vector3 aux = transform.position;
        aux.x += speed * Time.deltaTime;
        transform.position = aux;
    }

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Hero"))
        {
            Destroy(this.gameObject);
            GameObject destroyPearlInst = Instantiate(pearlDestroy, this.transform.position, Quaternion.identity) as GameObject;
            
        }
        else if (collision.gameObject.CompareTag("Floor"))
        {
            Destroy(this.gameObject);
            GameObject destroyPearlInst = Instantiate(pearlDestroy, this.transform.position, Quaternion.identity) as GameObject;
           
        }
    }


}
