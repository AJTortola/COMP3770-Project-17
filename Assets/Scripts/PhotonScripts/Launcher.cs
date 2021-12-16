using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Launcher : MonoBehaviourPunCallbacks
{

    [SerializeField] InputField roomNameInput;
    [SerializeField] Text errorText;

    [SerializeField] Text roomNameText;
    void Start()
    {
        Debug.Log("Connecting to Lobby");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster(){
        Debug.Log("Joined Master");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby(){
        MenuManager.Instance.OpenMenu("title");
        Debug.Log("Joined Lobby");
    }
    
    public void CreateRoom(){
        if(string.IsNullOrEmpty(roomNameInput.text)){
            return;
        }
        PhotonNetwork.CreateRoom(roomNameInput.text);
        MenuManager.Instance.OpenMenu("loading");
    }

    public override void OnJoinedRoom(){
        MenuManager.Instance.OpenMenu("room");
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
    }
    
    public override void OnCreateRoomFailed(short returnCode, string message){
        errorText.text = message;
        MenuManager.Instance.OpenMenu("error");

    }

    public void LeaveRoom(){
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu("loading");
    }

    public override void OnLeftRoom(){
        MenuManager.Instance.OpenMenu("title");
    }
}
