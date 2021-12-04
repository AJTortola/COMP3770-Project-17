using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilverPileCoins : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Capsule")
        {
            other.GetComponent<PickUpCounter>().points += 5;
            //Add 1 point
            Destroy(gameObject);//this destorys the item 
        }
    }
}
