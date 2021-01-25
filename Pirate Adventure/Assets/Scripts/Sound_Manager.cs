using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Manager : MonoBehaviour
{

    [SerializeField]
    private AudioSource heroRun;
    // Start is called before the first frame update
    
    void HeroRunSound()
    {
        heroRun.Play();
    }
}
