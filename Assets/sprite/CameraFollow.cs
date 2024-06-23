using UnityEngine;
using Photon.Pun;

public class CameraFollow : MonoBehaviourPun
{
    public Transform playerTransform;

    void Update()
    {
        if (playerTransform != null && playerTransform.GetComponent<PhotonView>().IsMine)
        {
            // 设置相机位置为玩家位置
            transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);
        }
    }
}