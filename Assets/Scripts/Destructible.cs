using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviourPunCallbacks
{
    [SerializeField] private int currentHp;
    [SerializeField] private bool increadible;
    public int CurrentHp
    {
        get
        {
            
            if(PhotonView.IsMine)
            {
                Debug.Log("(photonView.IsMine) return current hp: " + currentHp);
               return currentHp;
            }
            else
            {
                Debug.Log("(photonView.IsMine == false) return current hp enemy: " + currentHp);
                return currentHp;
            }
        }
        set
        {
            currentHp = value;
        }
     
    }
    PlayerAnimationController ani;
    private PhotonView photonView;
   public PhotonView PhotonView
    {
        get
        {
            if (photonView == null)
            {
                photonView = GetComponent<PhotonView>();
            }
            return photonView;
        }
    }
    private int damage;
    public Action<int> OnDifferenceCurrentHitPoints;
    public Action OnLose;

    public void Rpc_ApplyDamage()
    {
        PhotonView.RPC("ApplyDamage", RpcTarget.All);
    }
    [PunRPC]
    public void ApplyDamage()
    {
        this.damage = 10;


        if (!increadible)
        {
            if(ani == null)
            ani =  gameObject.GetComponent<PlayerAnimationController>();
            ani.Damage();
            currentHp -= damage;
            currentHp = Mathf.Clamp(currentHp, 0, 100);
            OnDifferenceCurrentHitPoints?.Invoke(currentHp);

            if (currentHp <= 0)
            {
                PhotonView.RPC("RPC_DestroyGo", RpcTarget.All);
            
            }
        }
    }
 
    [PunRPC]
    public void RPC_DestroyGo()
    {
        OnLose?.Invoke();
        Destroy(gameObject,0.5F);
    }
    public void SetIncreadible(bool incread)
    {
        increadible = incread;
    }
   
    public void SetHp()
    {
        if(currentHp != 30)
        {
            currentHp += 10;
        }
    }
}
