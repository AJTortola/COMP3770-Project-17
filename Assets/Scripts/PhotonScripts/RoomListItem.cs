using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RoomListItem : MonoBehaviour
{

    [SerializeField] Text text;
    [SerializeField] Text numOfPlayers;
    public RoomInfo info;
    public void SetUp(RoomInfo _info, string numPlayers){
        info = _info;
        text.text = _info.Name;
        Debug.Log(numPlayers);
        numOfPlayers.text = numPlayers;       
    }

    public void OnClick(){
        Launcher.Instance.JoinRoom(info);
    }
}
