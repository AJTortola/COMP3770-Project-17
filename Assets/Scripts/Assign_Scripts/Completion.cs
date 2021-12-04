using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Completion : MonoBehaviour
{
    public GameObject Player;
    public string LevelName;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("Level Completion Scene");
        }

    }
}
