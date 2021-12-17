using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RoomListItem : MonoBehaviour
{

    [SerializeField] Text text;
    RoomInfo info;
    public void SetUp(RoomInfo _info){
        info = _info;
        text.text = _info.Name;
    }

    public void OnClick(){
        Launcher.Instance.JoinRoom(info);
    }
}
