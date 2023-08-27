using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;

public class RoomManager : MonoBehaviourPunCallbacks
{
    private string mapType;

    void Start()
    {

    }

    void Update()
    {

    }

    #region UI Callback Methods
    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void OnEnterButtonClicked_Outdoor()
    {
        mapType = MultiplayerVRConstants.MAP_TYPE_VALUE_OUTDOOR;
        ExitGames.Client.Photon.Hashtable expectedCustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { MultiplayerVRConstants.MAP_TYPE_KEY, mapType } };
        PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, 0);
    }

    public void OnEnterButtonClicked_School()
    {
        mapType = MultiplayerVRConstants.MAP_TYPE_VALUE_SCHOOL;
        ExitGames.Client.Photon.Hashtable expectedCustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { MultiplayerVRConstants.MAP_TYPE_KEY,mapType} };
        PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties,0);
    
    }

    #endregion

    #region Photom Callback Methods
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log(message);
        CreateAndJoinRoom();
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("A room is created with the name: " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("The Local player:  " + PhotonNetwork.NickName
                          + " joined to " + PhotonNetwork.CurrentRoom.Name
                          + " Player count " + PhotonNetwork.CurrentRoom.PlayerCount);

        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(MultiplayerVRConstants.MAP_TYPE_KEY))//customPoomProperties 가있다면
        {
            object mapType;//이건 뭐야?
            if(PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(MultiplayerVRConstants.MAP_TYPE_KEY, out mapType))//maptype 이 있다면
            {
                Debug.Log("Joined room with the map: " + mapType);//map type 출력
            }
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + "joined to. Player count: " + PhotonNetwork.CurrentRoom.PlayerCount);
    }
    #endregion

    #region Private Methods
    private void CreateAndJoinRoom()
    {
        string randomRoomName = "Room_" + Random.Range(0, 10000);//랜덤한 방이름
        RoomOptions roomOptions = new RoomOptions();//룸옵션 생성
        roomOptions.MaxPlayers = 20;//최대 인원설정

        string[] roomPropsInLobby = { MultiplayerVRConstants.MAP_TYPE_KEY };
        //MultiplayerVRConstants.MAP_TYPE_KEY 의 문장을 출력하는 변수roomPropsInLobby를 선언

        ExitGames.Client.Photon.Hashtable customPoomProperties = new ExitGames.Client.Photon.Hashtable() 
                        { {MultiplayerVRConstants.MAP_TYPE_KEY, mapType} };
        //포톤 해쉬테이블 만드는데 new ExitGames.Client.Photon.Hashtable() { { MultiplayerVRConstants.MAP_TYPE_KEY, MultiplayerVRConstants.MAP_TYPE_VALUE_SCHOOL} };가 뭘 의미하는지 모르겠음

        roomOptions.CustomRoomPropertiesForLobby = roomPropsInLobby;//CustomRoomPropertiesForLobby 이게 무슨기능을 하는지 모르겠음
        roomOptions.CustomRoomProperties = customPoomProperties;//CustomRoomProperties 얘도 모르겠어

        PhotonNetwork.CreateRoom(randomRoomName,roomOptions);//설정한 방이름과 옵션을 가진 방이 만들어짐
    }



    #endregion
}
