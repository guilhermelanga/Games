using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{

    public GameObject todos;
    public int quantos;
    public int nextScene;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        quantos = todos.transform.childCount;
        Debug.Log(quantos);

        if (quantos <= 0)
        {

            SceneLevel();

        }
    }

    public void SceneLevel()
    {
        
        nextScene = SceneManager.GetActiveScene().buildIndex + 1;

        if(nextScene == 6)
        {
            SceneManager.LoadScene("StartScene");
            nextScene = 0;
        }
        else
        {
            SceneManager.LoadScene(nextScene);

        }
        

    }
}
