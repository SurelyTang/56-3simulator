using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class ChoseAndJoin : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public GameObject characterPrefab;

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(1);
        GameObject player = PhotonNetwork.Instantiate(characterPrefab.name, new Vector3(1, 1, 0), Quaternion.identity, 0);
    }
}
