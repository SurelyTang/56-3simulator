using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSync : MonoBehaviour, IPunObservable
{
    private Vector3 playerPosition;
    private Quaternion playerRotation;
    private PhotonView photonView; // 添加PhotonView成员变量

    void Start()
    {
        photonView = GetComponent<PhotonView>(); // 获取PhotonView组件
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // 当本地玩家发送数据时，将位置和旋转信息发送给远程玩家
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            // 当远程玩家接收数据时，接收位置和旋转信息并更新对象状态
            playerPosition = (Vector3)stream.ReceiveNext();
            playerRotation = (Quaternion)stream.ReceiveNext();
        }
    }

    void Update()
    {
        if (photonView != null && !photonView.IsMine) // 检查photonView是否为空
        {
            // 当对象不是本地玩家时，更新位置和旋转信息
            transform.position = Vector3.Lerp(transform.position, playerPosition, Time.deltaTime * 5);
            transform.rotation = Quaternion.Lerp(transform.rotation, playerRotation, Time.deltaTime * 5);
        }
    }
}