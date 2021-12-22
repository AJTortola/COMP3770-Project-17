using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using UnityEngine.UI; 

public class PlayerManager : MonoBehaviourPunCallbacks
{
    public static PlayerManager Instance;
    PhotonView PV;
    GameObject controller;
    void Awake(){
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        if(PV.IsMine){
            CreatedController();
        }
    }

    void CreatedController()
    {
        //Instantiate the player character controller
        controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), new Vector3(5,10,70), Quaternion.identity, 0, new object[] { PV.ViewID });
        Debug.Log("Created Player Controller");
    }

    IEnumerator CoolDownTime(){
        yield return new WaitForSeconds(5f);
        //respawn
        CreatedController();
    }
    public void Die(){
        Debug.Log("Died");
        PhotonNetwork.Destroy(controller);
        
        StartCoroutine(CoolDownTime());

    }
}
