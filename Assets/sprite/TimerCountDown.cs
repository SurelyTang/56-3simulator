using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class CountdownTimer : MonoBehaviourPun
{
    public float timeRemaining = 0;
    public float timeSet = 20;

    public static bool AddManager = false;
    public static bool TimerON = false;
    [SerializeField] private TextMeshProUGUI TimerText;

    void Start()
    {
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            photonView.RPC("ReSet", RpcTarget.All);
        }
        if (TimerON)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                int minutes = Mathf.FloorToInt(timeRemaining / 60);
                int seconds = Mathf.FloorToInt(timeRemaining % 60);
                TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
            else
            {
                TimerON = false;
            }
        }
    }
    [PunRPC]
    void ReSet()
    {
        TimerON = true;
        timeRemaining = timeSet;
    }
}