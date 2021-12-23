using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class RedFlag : MonoBehaviour 
{
    public bool capturedRedFlag = false; // did the blue team capture the read team's flag
    public GameObject blueTeamFlag; // Reference BlueTeamFlag
    BlueFlag blueFlag; // references BlueTeamFlag
    int count;
    // Start is called before the first frame update
    void Start()
    {
        blueFlag = blueTeamFlag.GetComponent<BlueFlag>(); // set to blueTeamFlag prefab
        gameObject.SetActive(true); // set child Model to Active at the start of the game

    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag) // switch statement when someone colldes with the redFlag
        {
            case "BluePlayer" when capturedRedFlag == false && gameObject.CompareTag("RedFlag"): // when Blue Player touches flag they steal it
                capturedRedFlag = true; // player captured the flag
                gameObject.SetActive(false); // Reference RedTeamFlag to turn on and off when it's stolen by blue team
                break;
            case "RedPlayer" when blueFlag.capturedBlueFlag && gameObject.CompareTag("RedFlag"): // when Red Player returns to the base with a blue flag
                blueFlag.capturedBlueFlag = false;
                blueFlag.gameObject.SetActive(true);
                count++;
                break;
        }
    }
    void Update()
    {
        if (count == 10)
        {
            
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("RedWin");
        }
    }

}