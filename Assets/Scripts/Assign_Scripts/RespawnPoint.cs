using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    public GameObject Player;
    public GameObject Respawn;

    void Start()
    {
        Player = GameObject.Find("Capsule");
        Respawn = GameObject.Find("PlayerRespawn");
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player.transform.position = Respawn.transform.position;
            Physics.SyncTransforms();
        }
    }
}
