using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUi : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject uIElements;
    [SerializeField] private Text  scoreTxt;
    [SerializeField] private int score;
    [SerializeField] private HitPointBar hitPointBar;
    public int Score => score;
    public HitPointBar HitPointBar => hitPointBar;
    [SerializeField] private int scoreLvl;
    [SerializeField] private PhotonView photonView;

    private Player player;
    private Destructible dest;
   public void Init( Player player)
    {
        this.player = player;
        if (player.ViewPlayer.IsMine)
        {
            uIElements.SetActive(true);
        }
        else
        {
            uIElements.SetActive(false);
        }
        dest = player as Destructible;
        //hitPointBar.Init(this.transform, player.Hp);
        scoreTxt.text = $"Ёнерги€: {score}";
        HitPointBar.Init(player.transform, player.CurrentHp);
    }
    
    
    public void UpdateScore(int i)
    {
        if (photonView.IsMine)
            photonView.RPC("RPC_UpdateScore", RpcTarget.All);
        scoreTxt.text = $"Ёнерги€: {score}";
    }
    [PunRPC]
    public void RPC_UpdateScore()
    {
      
        score += 1;
    
    }
    public void SetScoreLvl(int i)
    {
        scoreLvl = i;
    }
    public void ScoreNull()
    {
        score = 0;
        scoreTxt.text = $"Ёнерги€: {score}";
    }
    public void TakeBanan()
    {
        score -= 1;
        scoreTxt.text = $"Ёнерги€: {score}";
    }
}
