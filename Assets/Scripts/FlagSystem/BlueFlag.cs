using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class BlueFlag : MonoBehaviour
{
    public bool capturedBlueFlag;
    public GameObject redTeamFlag;
    RedFlag redFlag;
    int count;
    // Start is called before the first frame update
    private void Start()
    {
        redFlag = redTeamFlag.GetComponent<RedFlag>();
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "RedPlayer" when capturedBlueFlag == false && gameObject.CompareTag("BlueFlag"): // when Red Player touches flag they steal it
                capturedBlueFlag = true;
                gameObject.SetActive(false);
                break;
            case "BluePlayer" when redFlag.capturedRedFlag && gameObject.CompareTag("BlueFlag"): // when Blue Player returns to the abse with a red flag
                redFlag.capturedRedFlag = false;
                redFlag.gameObject.SetActive(true);
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
            SceneManager.LoadScene("BlueWin");
        }
    }
}