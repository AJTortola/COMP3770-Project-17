using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviourPunCallbacks
{

    public static Launcher Instance;
    [SerializeField] InputField roomNameInput;
    [SerializeField] Text errorText;
    [SerializeField] Text roomNameText;
    [SerializeField] Transform roomListContent;
    [SerializeField] GameObject roomListItemPrefab;
    [SerializeField] GameObject playerListItemPrefab;
    [SerializeField] Transform playerListContent;
    [SerializeField] GameObject startGameButton;
    List<RoomInfo> fullRoomList = new List<RoomInfo>();
    List<RoomListItem> roomListItems = new List<RoomListItem>();


    void Awake(){
        Instance = this;
    }
    void Start()
    {
        Debug.Log("Connecting to Lobby");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster(){
        Debug.Log("Joined Master");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby(){
        MenuManager.Instance.OpenMenu("title");
        Debug.Log("Joined Lobby");
        PhotonNetwork.NickName = "BattlePerson " + Random.Range(0,1000).ToString("0000");
    }
    
    public void CreateRoom(){
        if(string.IsNullOrEmpty(roomNameInput.text)){
            return;
        }
        RoomOptions options = new RoomOptions();                    //set options
        options.MaxPlayers = 4;                                     //set max players to 4

        PhotonNetwork.CreateRoom(roomNameInput.text, options);
        MenuManager.Instance.OpenMenu("loading");
    }

    public override void OnJoinedRoom(){
        MenuManager.Instance.OpenMenu("room");
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;

        Player[] players = PhotonNetwork.PlayerList;

        foreach (Transform child in playerListContent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < players.Length; i++)
        {
            Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }
    
    public override void OnMasterClientSwitched(Player newMasterClient){
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnCreateRoomFailed(short returnCode, string message){
        errorText.text = message;
        MenuManager.Instance.OpenMenu("error");

    }

    public void StartMoonGame(){
        PhotonNetwork.LoadLevel(1);
    }
    
    public void StartWarehouseGame(){
        PhotonNetwork.LoadLevel(2);
    }

    public void LeaveRoom(){
        PhotonNetwork.LeaveRoom();
        Destroy(RoomManager.Instance.gameObject);
        //PhotonNetwork.Disconnect();
        //MenuManager.Instance.OpenMenu("loading");
    }

    public void JoinRoom(RoomInfo info){
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.Instance.OpenMenu("loading");

    }

    public override void OnLeftRoom(){
        SceneManager.LoadScene(0);

        base.OnLeftRoom();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList) {
        foreach(RoomInfo updatedRoom in roomList)
        {
            RoomInfo existingRoom = fullRoomList.Find(x => x.Name.Equals(updatedRoom.Name)); // Check to see if we have that room already
            if(existingRoom == null) // WE DO NOT HAVE IT
            {
                fullRoomList.Add(updatedRoom); // Add the room to the full room list
            }
            else if(updatedRoom.RemovedFromList) // WE DO HAVE IT, so check if it has been removed
            {
                fullRoomList.Remove(existingRoom); // Remove it from our full room list
            }
        }
        RenderRoomList();
    }

    void RenderRoomList()
    {
        RemoveRoomList();
        foreach(RoomInfo roomInfo in fullRoomList)
        {
            RoomListItem roomListItem = Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>();
            roomListItem.SetUp(roomInfo, roomInfo.PlayerCount +"/"+roomInfo.MaxPlayers);
            roomListItems.Add(roomListItem);
        }
    }

    void RemoveRoomList()
    {
        foreach(RoomListItem roomListItem in roomListItems)
        {
            Destroy(roomListItem.gameObject);
        }
        roomListItems.Clear();
    }


    public override void OnPlayerEnteredRoom(Player newPlayer){
        Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }
}
