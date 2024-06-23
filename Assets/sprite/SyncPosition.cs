using Photon.Pun;
using UnityEngine;

public class SyncPosition : MonoBehaviourPun, IPunObservable
{
    private Vector3 correctPlayerPos;
    private Quaternion correctPlayerRot;

    void Start()
    {
        if (!photonView.IsMine)
        {
            // 如果对象不是本地玩家的话，启用Photon Transform View Classic组件
            PhotonTransformViewClassic photonTransformView = GetComponent<PhotonTransformViewClassic>();
            photonTransformView.enabled = true;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // 当对象是本地玩家的时候，将位置和旋转信息发送到其他玩家
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            // 当对象是远程玩家的时候，接收其他玩家发送的位置和旋转信息
            correctPlayerPos = (Vector3)stream.ReceiveNext();
            correctPlayerRot = (Quaternion)stream.ReceiveNext();
        }
    }

    void Update()
    {
        if (!photonView.IsMine)
        {
            // 当对象是远程玩家的时候，更新位置和旋转信息
            transform.position = Vector3.Lerp(transform.position, correctPlayerPos, Time.deltaTime * 5);
            transform.rotation = Quaternion.Lerp(transform.rotation, correctPlayerRot, Time.deltaTime * 5);
        }
    }
}