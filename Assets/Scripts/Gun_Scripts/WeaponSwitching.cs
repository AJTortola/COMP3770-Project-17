using UnityEngine;
using Photon.Pun;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0; // default is the first weapon in the Weapon Holder object
    PhotonView PV;

    void Awake(){
        PV = GetComponentInParent<PhotonView>();
    }
    private void Start()
    {
        SelectWeapon();
    }

    private void Update()
    {
        if(PV.IsMine){

            int previousSelectedWeapon = selectedWeapon;
            
            if (Input.GetAxis("Mouse ScrollWheel") > 0) // if > 0 scrolled up
            {
                if (selectedWeapon >= transform.childCount - 1)
                    selectedWeapon = 0; // set it make to 0 to loop back to the first weapon on the list
                else
                    selectedWeapon++;
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0) // if < 0 scrolled down
            {
                if (selectedWeapon == 0)
                    selectedWeapon = transform.childCount - 1; // set it to the last weapon's index to loop back to it
                else
                    selectedWeapon--;
            }
            
            if(Input.GetKeyDown(KeyCode.Alpha1)) // if they press 1 on the keyboard then select first weapon
            {
                selectedWeapon = 0;
            }
            if(Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2) // make sure there are 2 weapons
            {
                selectedWeapon = 1;
            }
            if(Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3) 
            {
                selectedWeapon = 2;
            }
            if(Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4)
            {
                selectedWeapon = 3;
            }
            if (previousSelectedWeapon != selectedWeapon)
            {
                SelectWeapon(); // enable and disable the corresponding objects
            }
        }
    }

    void SelectWeapon() // loop through all weapons and enable the weapon that matches the weapon index and vice versa
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
                weapon.gameObject.SetActive(true); // enable weapon
            else 
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}