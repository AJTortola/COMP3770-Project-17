using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;
    
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
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), new Vector3(5,10,70), Quaternion.identity);
        Debug.Log("Created Player Controller");
    }
}
