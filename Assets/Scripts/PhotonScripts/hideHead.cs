using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class hideHead : MonoBehaviour
{

    PhotonView PV;
    void Awake()
    {
        PV = GetComponentInParent<PhotonView>();
    }
    void Start()
    {
        if(PV.IsMine){
            gameObject.SetActive(false);
        }
    }

}
