using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float cameraSpeed = 0.7f;
    private bool followPlayer;
    public Vector3 lastTargetPos;
    public Vector3 currenSpeed;

   
    
    // Start is called before the first frame update
    void Start()
    {
        followPlayer = true;
        lastTargetPos = player.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (followPlayer)
        {
            if(player.transform.position.x != transform.position.x)
            {
                Vector3 newCameraPos = Vector3.SmoothDamp(transform.position, player.transform.position, ref currenSpeed, cameraSpeed);
                transform.position = new Vector3(newCameraPos.x, newCameraPos.y, transform.position.z);
                lastTargetPos = player.transform.position;
            }
        }
    }
}
