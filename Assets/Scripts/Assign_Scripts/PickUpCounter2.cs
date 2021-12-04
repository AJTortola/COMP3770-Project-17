using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PickUpCounter2 : MonoBehaviour
{
    public int points = 0;
    string path = "Assets/CSV/points2.csv";
    StreamWriter writer;
    private void OnGUI()
    {
        GUI.color = Color.white;
        GUI.Label(new Rect(10, 10, 100, 20), "Coins : " + points);

        //Open the writer
        writer = new StreamWriter(path);
        writer.WriteLine("coinsCollected2");
        writer.WriteLine(points);
        writer.Close();

    }



}
