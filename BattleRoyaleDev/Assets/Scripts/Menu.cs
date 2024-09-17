using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class Menu : MonoBehaviourPunCallbacks, ILobbyCallbacks
{
    //UI Containers
    [Header("Screens")]
    public GameObject mainScreen;
    public GameObject createRoomScreen;
    public GameObject lobbyScreen;
    public GameObject lobbyBrowserScreen;

    [Header("Main Screen")]
    public Button createRoomButton;
    public Button findRoomButton;

    //lobby text to change
    [Header("Lobby")]
    public TextMeshProUGUI playerListText;
    public TextMeshProUGUI roomInfoText;
    public Button startGameButton;

    //extesible lists to hold a changing number of room buttons
    [Header("Lobby Browser")]
    public RectTransform roomListContainer;
    public GameObject roomButtonPrefab;

    private List<GameObject> roomButtons = new List<GameObject>();
    private List<RoomInfo> roomList = new List<RoomInfo>();

    private void Start()
    {
        //disable menus that aren't start
        mainScreen.SetActive(true);
        createRoomScreen.SetActive(false);
        lobbyScreen.SetActive(false);
        lobbyBrowserScreen.SetActive(false);

        //disable buttons until connected
        createRoomButton.interactable = false;
        findRoomButton.interactable = false;

        //make sure the cursor is free as it is locked during gameplay
        Cursor.lockState = CursorLockMode.None;

        //if in room from previous game
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.CurrentRoom.IsVisible = true;
            PhotonNetwork.CurrentRoom.IsOpen = true;
        }
    }

    /// <summary>
    /// disables all screens then renables passed screen
    /// </summary>
    /// <param name="screen"></param>
    private void SetScreen(GameObject screen)
    {
        
        mainScreen.SetActive(false);
        createRoomScreen.SetActive(false);
        lobbyScreen.SetActive(false);
        lobbyBrowserScreen.SetActive(false);

        screen.SetActive(true);
    }

    /*************************
     * MAIN SCREEN FUNCTIONS *
     *************************/
    /// <summary>
    /// changes photon nickname to what is in the input field
    /// </summary>
    /// <param name="playerNameInput"></param>
    public void OnPlayerNameChanged(TMP_InputField playerNameInput)
    {
        PhotonNetwork.NickName = playerNameInput.text;
    }

    //allows the player to interact with buttons when they have connected to photon
    public override void OnConnectedToMaster()
    {
        createRoomButton.interactable = true;
        findRoomButton.interactable = true;
    }

    public void OnCreateRoomButton()
    {
        SetScreen(createRoomScreen);
    }

    public void OnFindRoomButton()
    {
        SetScreen(lobbyBrowserScreen);
    }

    public void OnBackButton()
    {
        SetScreen(mainScreen);
    }

    /// <summary>
    /// creates a room with the name of the input
    /// </summary>
    /// <param name="roomNameInput"></param>
    public void OnCreateButton(TMP_InputField roomNameInput)
    {
        NetworkManager.instance.CreateRoom(roomNameInput.text);
    }
}
