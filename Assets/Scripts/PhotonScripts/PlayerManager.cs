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

    int numOfPlayers;
    public string myTeam;
    void Awake(){
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        numOfPlayers = PhotonNetwork.PlayerList.Length;
        if(PV.IsMine){
            CreatedController();
        }
    }

    void CreatedController()
    {
        //Instantiate the player character controller
        calculateTeam();
        if(myTeam == "blue"){
            controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "BluePlayerController"), new Vector3(20,10,-21), Quaternion.identity, 0, new object[] { PV.ViewID });
            Debug.Log("Created Blue Player Controller");
            controller.GetComponent<TeamMember>().teamID = myTeam;
        }
        if(myTeam == "red"){
            controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "RedPlayerController"), new Vector3(19,10,25), Quaternion.identity, 0, new object[] { PV.ViewID });
            Debug.Log("Created Blue Player Controller");
            controller.GetComponent<TeamMember>().teamID = myTeam;
        }
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

    void calculateTeam(){
        if(numOfPlayers % 2 == 1){
            myTeam = "blue";
        }
        else if(numOfPlayers % 2 == 0){
            myTeam = "red";
        }
    }

}
