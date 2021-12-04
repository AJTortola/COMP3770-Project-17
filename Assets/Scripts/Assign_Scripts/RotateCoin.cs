using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCoin : MonoBehaviour
{

    public float speed = 1.0f;
    public Vector3 Rotate = new Vector3(30, 15, 45);

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Rotate, speed);
    }
}