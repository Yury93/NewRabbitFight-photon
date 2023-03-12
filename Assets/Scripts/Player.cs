using Cinemachine;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Destructible
{
    [SerializeField] private int teamId;
    [SerializeField] private PhotonView view;
    [SerializeField] private CharacterController charContrl;
    [SerializeField] private PlayerUi playerUi;

    private Vector3 myPosition;

    public int TeamId => teamId;
    public PhotonView ViewPlayer => view;
    public int Hp => CurrentHp;
    public PlayerUi PlayerUI => playerUi;

    private void Awake()
    {

        myPosition = transform.position;
        view = GetComponent<PhotonView>();

        if (view.IsMine)
        {
            var cam = FindObjectOfType<CinemachineVirtualCamera>();
            cam.Follow = gameObject.GetComponent<Transform>();
            cam.LookAt = gameObject.GetComponent<Transform>();

        }
        playerUi.Init(this);
        OnDifferenceCurrentHitPoints += CheckHp;
    }

    public void CheckHp(int currentHp)
    {
        playerUi.HitPointBar.Set(currentHp);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if (hit.collider.GetComponent<Banana>())
        {
            AudioManager.Instance.AudioPlay("banan");

            playerUi.UpdateScore(1);
            hit.collider.GetComponent<Banana>().OnDest();
            transform.position = new Vector3(transform.position.x, myPosition.y, transform.position.z);
        }
    } 
    
}
