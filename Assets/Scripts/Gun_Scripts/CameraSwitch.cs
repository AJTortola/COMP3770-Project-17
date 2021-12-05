using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{

    public GameObject ThirdCam;
    public GameObject FirstCam;

    public int camMode;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Camera")){
            if (camMode == 1){
                camMode = 0;
            }
            else{
                camMode += 1;
            }
            StartCoroutine(camChange());
        }

        IEnumerator camChange (){
            yield return new WaitForSeconds(0.01f);
            if (camMode == 0){
                ThirdCam.SetActive(true);
                FirstCam.SetActive(false);
            }
            if (camMode == 1){
                FirstCam.SetActive(true);
                ThirdCam.SetActive(false);
            }
        }
    }
}
