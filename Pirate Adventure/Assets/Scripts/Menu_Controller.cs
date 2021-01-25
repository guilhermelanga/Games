using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Controller : MonoBehaviour
{
    // Start is called before the first frame update

    private bool open;


    public void Restart()
    {
        Invoke("LoadScene", 1f);
    }

    void LoadScene()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    public void StartButton()
    {
        Invoke("LoadScene", 1f);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }



    //CHEST
    public void OpenChest()
    {
        open = true;
        Invoke("ChestDone", 0.001f);
    }

    public bool Openchest
    {
        get { return open; }
    }

    private void ChestDone()
    {
        open = false;
    }

}
