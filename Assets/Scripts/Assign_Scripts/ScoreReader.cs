using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreReader : MonoBehaviour
{
    public string path = "Assets/CSV/points.csv";
    public string path2 = "Assets/CSV/points2.csv";
    public int total;
    // Start is called before the first frame update
    void Start()
    {
        StreamReader reader = new StreamReader(path);
        StreamReader reader2 = new StreamReader(path2);

        total = int.Parse(reader.ReadLine()) + int.Parse(reader2.ReadLine());
        reader.Close();
        reader2.Close();
        GameObject TotalScore = GameObject.Find("TotalScore");
        TotalScore.GetComponent<Text>().text = total.ToString();
    }

    
}
