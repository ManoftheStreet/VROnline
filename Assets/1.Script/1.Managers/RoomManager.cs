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

        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(MultiplayerVRConstants.MAP_TYPE_KEY))//customPoomProperties ���ִٸ�
        {
            object mapType;//�̰� ����?
            if(PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(MultiplayerVRConstants.MAP_TYPE_KEY, out mapType))//maptype �� �ִٸ�
            {
                Debug.Log("Joined room with the map: " + mapType);//map type ���
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
        string randomRoomName = "Room_" + Random.Range(0, 10000);//������ ���̸�
        RoomOptions roomOptions = new RoomOptions();//��ɼ� ����
        roomOptions.MaxPlayers = 20;//�ִ� �ο�����

        string[] roomPropsInLobby = { MultiplayerVRConstants.MAP_TYPE_KEY };
        //MultiplayerVRConstants.MAP_TYPE_KEY �� ������ ����ϴ� ����roomPropsInLobby�� ����

        ExitGames.Client.Photon.Hashtable customPoomProperties = new ExitGames.Client.Photon.Hashtable() 
                        { {MultiplayerVRConstants.MAP_TYPE_KEY, mapType} };
        //���� �ؽ����̺� ����µ� new ExitGames.Client.Photon.Hashtable() { { MultiplayerVRConstants.MAP_TYPE_KEY, MultiplayerVRConstants.MAP_TYPE_VALUE_SCHOOL} };�� �� �ǹ��ϴ��� �𸣰���

        roomOptions.CustomRoomPropertiesForLobby = roomPropsInLobby;//CustomRoomPropertiesForLobby �̰� ��������� �ϴ��� �𸣰���
        roomOptions.CustomRoomProperties = customPoomProperties;//CustomRoomProperties �굵 �𸣰ھ�

        PhotonNetwork.CreateRoom(randomRoomName,roomOptions);//������ ���̸��� �ɼ��� ���� ���� �������
    }



    #endregion
}
