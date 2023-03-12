using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour
{
    private Vector3 pos;
    public Action<Banana> OnDestroyBanana;
    private void Start()
    {
        pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
    private void Update()
    {
        transform.position =  new Vector3 (transform.position.x,pos.y,transform.position.z);
    }
    public void OnDest()
    {
       
          var view =  gameObject.GetComponent<PhotonView>();
            view.RPC("SetAct", RpcTarget.All);
      
    }
    [PunRPC]
    public void SetAct()
    {
        OnDestroyBanana?.Invoke(this);
        if(gameObject)
        {
            Destroy(this.gameObject);
        }
    }
}
