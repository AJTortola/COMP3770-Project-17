using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
   

    public void Play()
    {
        SceneManager.LoadScene("Level 1");
    }


    public void Back()
    {
        SceneManager.LoadScene("Main Menu");
    }

}
