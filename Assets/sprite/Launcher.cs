using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MyLauncher : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public GameObject myhome;
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        myhome.SetActive(false);
    }
    
    public override void OnConnectedToMaster()
    {
        //Debug.Log("welcome Michael");
        PhotonNetwork.JoinOrCreateRoom("Room", new Photon.Realtime.RoomOptions() { MaxPlayers = 9 }, default);

    }
    
    // Update is called once per frame
    public void JoinRoomButton1()
    {
        myhome.SetActive(false);
        GameObject player = PhotonNetwork.Instantiate("Player 1", new Vector3(1, 1, 0), Quaternion.identity, 0);
    }


    public void JoinRoomButton2()
    {
        myhome.SetActive(false);
        GameObject player = PhotonNetwork.Instantiate("Player 2", new Vector3(1, 1, 0), Quaternion.identity, 0);
    }
    public void JoinRoomButton3()
    {
        myhome.SetActive(false);
        GameObject player = PhotonNetwork.Instantiate("Player 3", new Vector3(1, 1, 0), Quaternion.identity, 0);
    }
    public void JoinRoomButton4()
    {
        myhome.SetActive(false);
        GameObject player = PhotonNetwork.Instantiate("Player 4", new Vector3(1, 1, 0), Quaternion.identity, 0);
    }
    public void JoinRoomButton5()
    {
        myhome.SetActive(false);
        GameObject player = PhotonNetwork.Instantiate("Player 5", new Vector3(1, 1, 0), Quaternion.identity, 0);
    }
    public void JoinRoomButton6()
    {
        myhome.SetActive(false);
        GameObject player = PhotonNetwork.Instantiate("Player 6", new Vector3(1, 1, 0), Quaternion.identity, 0);
    }
    public void JoinRoomButton7()
    {
        myhome.SetActive(false);
        GameObject player = PhotonNetwork.Instantiate("Player 7", new Vector3(1, 1, 0), Quaternion.identity, 0);
    }
    public void JoinRoomButton8()
    {
        myhome.SetActive(false);
        GameObject player = PhotonNetwork.Instantiate("Player 8", new Vector3(1, 1, 0), Quaternion.identity, 0);
    }
    public void JoinRoomButton9()
    {
        myhome.SetActive(false);
        GameObject player = PhotonNetwork.Instantiate("Player 9", new Vector3(1, 1, 0), Quaternion.identity, 0);
    }
    public void JoinRoomButton10()
    {
        myhome.SetActive(false);
        GameObject player = PhotonNetwork.Instantiate("Player 10", new Vector3(1, 1, 0), Quaternion.identity, 0);
    }
    public void JoinRoomButton11()
    {
        myhome.SetActive(false);
        GameObject player = PhotonNetwork.Instantiate("Player 11", new Vector3(1, 1, 0), Quaternion.identity, 0);
    }
    public void JoinRoomButton12()
    {
        myhome.SetActive(false);
        GameObject player = PhotonNetwork.Instantiate("Player 12", new Vector3(1, 1, 0), Quaternion.identity, 0);
    }
    public void JoinRoomButton13()
    {
        myhome.SetActive(false);
        GameObject player = PhotonNetwork.Instantiate("Player 13", new Vector3(1, 1, 0), Quaternion.identity, 0);
    }
    public void JoinRoomButton14()
    {
        myhome.SetActive(false);
        GameObject player = PhotonNetwork.Instantiate("Player 14", new Vector3(1, 1, 0), Quaternion.identity, 0);
    }

    public void JoinRoomButton15()
    {
        myhome.SetActive(false);
        GameObject player = PhotonNetwork.Instantiate("Player 15", new Vector3(1, 1, 0), Quaternion.identity, 0);
    }
    public void JoinRoomButton16()
    {
        myhome.SetActive(false);
        GameObject player = PhotonNetwork.Instantiate("Player 16", new Vector3(1, 1, 0), Quaternion.identity, 0);
    }
    public override void OnJoinedRoom()
    {
        myhome.SetActive(true);
        //GameObject player = PhotonNetwork.Instantiate("Player", new Vector3(1, 1, 0), Quaternion.identity, 0);
    }
}
