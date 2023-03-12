using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class ListItem : MonoBehaviour
{
    [SerializeField] private Text roomNameTxt;
    [SerializeField] private Text countPlayerTxt;
    [SerializeField] private Button button;
    public void SetInfo(RoomInfo info)
    {
        roomNameTxt.text = info.Name;
        countPlayerTxt.text = info.PlayerCount.ToString() + "/" + info.MaxPlayers ;
        if (info.PlayerCount == info.MaxPlayers) button.interactable = false;
        else button.interactable = true;
    }
    public void JointListToRoom()
    {
        PhotonNetwork.JoinRoom(roomNameTxt.text);
    }
}
