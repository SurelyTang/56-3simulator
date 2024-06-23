using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class StartGame : MonoBehaviourPun
{
    public void SetPlayerGroup(int groupNumber, int randomValue)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        string objectName1 = "b" + randomValue;
        string objectName2 = "w" + randomValue;
        if (groupNumber == 0)
        {
            Transform childTransform1 = transform.Find(objectName1);
            Debug.Log("子对象"+objectName1);
            GameObject b = childTransform1.gameObject;
            if (b != null)
            {
                b.SetActive(true);
            }
            else
            {
                Debug.Log("未找到子对象！"+objectName1);
            }

        }
        else
        {
            Transform childTransform2 = transform.Find(objectName2);
            GameObject w = childTransform2.gameObject;
            if (w != null)
            {
                w.SetActive(true);
            }
            else
            {
                Debug.Log("未找到子对象！"+objectName1);
            }
        }

    }

    public void SetSceneOnbw(int groupNumber, int randomValue)
    {
        string objectName1 = "b" + randomValue;
        string objectName2 = "w" + randomValue;
        Transform childTransform1 = transform.Find(objectName1); 
        GameObject childObject = childTransform1.gameObject;
        childObject.SetActive(true);
        Transform childTransform2 = transform.Find(objectName2);
        GameObject childObject2 = childTransform2.gameObject;
        childObject2.SetActive(true);
    }


    public void SetSceneOffbw()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }


    [PunRPC]
    void StartTheGame()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        int rand = Random.Range(1, 3); // 生成1到2之间的随机数
        string objectName1 = "b" + rand;
        string objectName2 = "w" + rand;

        Transform childTransform1 = transform.Find(objectName1); // 查找名为"b1"的子对象
        if (childTransform1 != null)
        {
            GameObject childObject = childTransform1.gameObject;
            childObject.SetActive(true); // 显示找到的子对象
        }
        else
        {
            Debug.Log("未找到子对象！");
        }

        Transform childTransform2 = transform.Find(objectName2); // 查找名为"b1"的子对象
        if (childTransform2 != null)
        {
            GameObject childObject = childTransform2.gameObject;
            childObject.SetActive(true); // 显示找到的子对象
        }
        else
        {
            Debug.Log("未找到子对象！");
        }
    }
}