using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Fish : MonoBehaviour
{

    [SerializeField]
    private Transform pos1, pos2;
    [SerializeField]
    private Transform startPos;

    private float speed = 3.5f;

    private bool face;

    Vector3 nextPos;

    // Start is called before the first frame update
    void Start()
    {
        nextPos = startPos.position;
        face = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position== pos1.position)
        {
            nextPos = pos2.position;
            if (!face)
            {
                Flip();
            }
        }
        if (this.transform.position == pos2.position)
        {
            nextPos = pos1.position;
            if (face)
            {
                Flip();
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }

    void Flip()
    {
        face = !face;
        Vector3 scale = this.transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
