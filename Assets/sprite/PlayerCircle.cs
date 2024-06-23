using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;


public class playerCricle : MonoBehaviourPun
{

    public delegate void MyDelegate(int parameter1, int parameter2);
    int ti = 0;
    // 声明一个带参数的委托实例
    public static int randomValue;
    public static List<int> playerIndices;

    public static bool AddManager = false;
    public MyDelegate myDelegate;
    void Start()
    {
        Transform circleW = transform.Find("circlew");
        Transform circleB = transform.Find("circleb");

        // 将子对象的active属性设置为false
        if (circleW != null)
        {
            circleW.gameObject.SetActive(false);
        }

        if (circleB != null)
        {
            circleB.gameObject.SetActive(false);
        }
        if(photonView.IsMine && this.gameObject.name == "player 9(Clone)")
            AddManager=true;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F9) && (PhotonNetwork.IsMasterClient || AddManager))
        {
            Debug.Log("this.gameObject.name: " + this.gameObject.name);
            if (photonView.IsMine)
            {
                ti++;
                Debug.Log("PhotonNetwork.PlayerList.Length: " + PhotonNetwork.PlayerList.Length);
                Debug.Log("time: " + ti);
                playerIndices = new List<int>();
                for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
                {
                    playerIndices.Add(i);
                }

                Shuffle(playerIndices);
                // 将playerIndices列表中的内容输出到控制台
                foreach (int index in playerIndices)
                {
                    Debug.Log("Player Index: " + index);
                }
                randomValue = UnityEngine.Random.Range(1, 16);
            }
        }

        if (Input.GetKeyDown(KeyCode.F5) && (PhotonNetwork.IsMasterClient || AddManager))
        {
            Debug.Log("this.gameObject.name: " + this.gameObject.name);
            photonView.RPC("SetPlayerCircleOff", RpcTarget.All);
            photonView.RPC("SetSceneOff", RpcTarget.All);
        }

        if (Input.GetKeyDown(KeyCode.F8) && (PhotonNetwork.IsMasterClient || AddManager))
        {
            Debug.Log("this.gameObject.name: " + this.gameObject.name);
            photonView.RPC("SetSceneOn", RpcTarget.All,0,randomValue);
        }

        if (Input.GetKeyDown(KeyCode.F12) && (PhotonNetwork.IsMasterClient || AddManager))
        {
            Debug.Log("this.gameObject.name: " + this.gameObject.name);
            photonView.RPC("SetrandomValue", RpcTarget.All,randomValue);
            Debug.Log("F12 START Received random value: " + randomValue);
            int groupSize = Mathf.CeilToInt(PhotonNetwork.PlayerList.Length / 2f);
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                int groupNumber = i / groupSize;
                Debug.Log("randomValue Before SetScene: " + randomValue);
                //photonView.RPC("SetPlayerGroup", PhotonNetwork.PlayerList[playerIndices[i]], groupNumber, randomValue);
                photonView.RPC("SetPlayerCircle", PhotonNetwork.PlayerList[playerIndices[i]], groupNumber, randomValue);
                photonView.RPC("SetScene", PhotonNetwork.PlayerList[playerIndices[i]], groupNumber, randomValue);
            }

        }


    }

    void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }


    [PunRPC]
    void SetPlayerCircle(int groupNumber, int randomValue)
    {
        Debug.Log("Received random value: " + randomValue);
        if (!photonView.IsMine && PhotonNetwork.IsConnected)
            return;
        Transform circleW = transform.Find("circlew");
        Transform circleB = transform.Find("circleb");
        circleW.gameObject.SetActive(false);
        circleB.gameObject.SetActive(false);
        if (groupNumber == 0)
        {
            if (circleW != null)
            {
                photonView.RPC("circleWOn", RpcTarget.All, randomValue);
                //circleW.gameObject.SetActive(true);
                Debug.Log("Random value for group 0");
            }
            else
            {
                Debug.Log("未找到子对象！");
            }
        }
        else
        {
            if (circleB != null)
            {
                photonView.RPC("circleBOn", RpcTarget.All, randomValue);
                //circleB.gameObject.SetActive(true);
                Debug.Log("Random value for group 1");
            }
            else
            {
                Debug.Log("未找到子对象！");
            }
        }

    }

    [PunRPC]
    void circleWOn(int randomValue)
    {
        Transform circleW = transform.Find("circlew");
        Transform circleB = transform.Find("circleb");
        circleW.gameObject.SetActive(true);
        circleB.gameObject.SetActive(false);
    }


    [PunRPC]
    void circleBOn(int randomValue)
    {
        Transform circleW = transform.Find("circlew");
        Transform circleB = transform.Find("circleb");
        circleB.gameObject.SetActive(true);
        circleW.gameObject.SetActive(false);

    }

    [PunRPC]
    void SetPlayerCircleOff(){
        Transform circleW = transform.Find("circlew");
        Transform circleB = transform.Find("circleb");
        circleB.gameObject.SetActive(false);
        circleW.gameObject.SetActive(false);
    }

    [PunRPC]
    void SetScene(int groupNumber, int randomValue)
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected)
            return;
        GameObject receiverObject = GameObject.Find("bw");
        StartGame receiver = receiverObject.GetComponent<StartGame>();
        MyDelegate MyDelegate = receiver.SetPlayerGroup;
        MyDelegate(groupNumber, randomValue);
    }


    [PunRPC]
    void SetSceneOn(int groupNumber, int randomValue)
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected)
            return;
        GameObject receiverObject = GameObject.Find("bw");
        StartGame receiver = receiverObject.GetComponent<StartGame>();
        MyDelegate MyDelegate = receiver.SetSceneOnbw;
        MyDelegate(groupNumber, randomValue);
    }

    [PunRPC]
    void SetSceneOff()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected)
            return;
        GameObject receiverObject = GameObject.Find("bw");
        StartGame receiver = receiverObject.GetComponent<StartGame>();
        receiver.SetSceneOffbw();
    }

    [PunRPC]
    void SetrandomValue(int randomValueNow)
    {
        randomValue = randomValueNow;
    }

}
