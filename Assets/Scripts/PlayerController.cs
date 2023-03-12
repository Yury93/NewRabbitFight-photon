using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class PlayerController : MonoBehaviourPun
{
    [SerializeField] private CharacterController charController;
    [SerializeField] private Joystick joystick;
    private Vector3 inputVector;
    [SerializeField] private Button buttonAttack, buttonSlope, buttonLaser;
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private Text hpTxt;
    [SerializeField] private TextMeshProUGUI hpEnemyText;
    [SerializeField] private SuperEyes superEyes1,superEyes2;
    [SerializeField] private float timerStateTransitions;
    [SerializeField] private State currentState, lateState;
    private Player player;
    private float startTimer;
    private Vector3 constPos;
    public enum State
    {
        idle,
        move,
        attack,
        slope,
    }

    public State CurrentState => currentState;
    public TextMeshProUGUI HpEnemyText => hpEnemyText;
    public Player Player => player;

    public event Action OnChangeState;


    private void Start()
    {
        player = GetComponent<Player>();
        //if (player.ViewPlayer.IsMine)
        //{
        joystick = GameContainer.Instance.PlayerJoystick;
        buttonAttack = GameContainer.Instance.ButtonAttack;
        buttonSlope = GameContainer.Instance.ButtonSlope;
        buttonLaser = GameContainer.Instance.ButtonLaser;

        buttonSlope.onClick.AddListener(SlopeMode);//не забыть отписаться
        buttonAttack.onClick.AddListener(Attack);
        buttonLaser.onClick.AddListener(PushLaser);

        player = GetComponent<Player>();
        startTimer = timerStateTransitions;
        constPos = transform.position;
        superEyes1.Init(this,false);
        superEyes2.Init(this,true);
        //}

    }
    private void PushLaser()
    {
        if (photonView.IsMine)
            photonView.RPC("RPC_PushLaser", RpcTarget.All);
    }
    [PunRPC]
    public void RPC_PushLaser()
    {
        superEyes2.ScaleEyes();
        superEyes1.ScaleEyes();
    }
    private void Update()
    {
        transform.position = new Vector3(transform.position.x, constPos.y, transform.position.z);
        
        if (player.ViewPlayer.IsMine)
        {
            transform.position = new Vector3(transform.position.x, constPos.y, transform.position.z);
            if (currentState != lateState)//смена состояния
            {
                OnChangeState?.Invoke();
                lateState = currentState;
            }
            if (currentState == State.idle)
            {
                inputVector = new Vector3(joystick.Horizontal,
                 0, joystick.Vertical);
                if (inputVector != Vector3.zero)
                {
                    currentState = State.move;
                }
                if (!buttonAttack.interactable)
                {
                    buttonAttack.interactable = true;
                    buttonSlope.interactable = true;
                }
            }
            if (currentState == State.move)
            {
                inputVector = new Vector3(joystick.Horizontal,
                 0, joystick.Vertical);
                if (inputVector == Vector3.zero)
                {
                    currentState = State.idle;
                }
                else
                {
                    Move();
                }
                if (!buttonAttack.interactable)
                {
                    buttonAttack.interactable = true;
                    buttonSlope.interactable = true;
                }
            }
            if (currentState == State.attack)
            {
                timerStateTransitions -= Time.deltaTime;
                if (timerStateTransitions <= 0)
                {
                    player.SetIncreadible(false);

                    currentState = State.idle;

                    timerStateTransitions = startTimer;
                }
            }
            if (currentState == State.slope)
            {
                timerStateTransitions -= Time.deltaTime;
                if (timerStateTransitions <= 0)
                {
                    player.SetIncreadible(false);

                    currentState = State.idle;

                    timerStateTransitions = startTimer;
                }
            }
        }
    }
    private void Move()
    {
        charController.Move(inputVector * speed * Time.deltaTime);
        transform.LookAt(transform.position + inputVector);
    }
    public void Attack()
    {
        currentState = State.attack;

        //RPC_Attack();

        if(photonView.IsMine)
        player.ViewPlayer.RPC("RPC_Attack", RpcTarget.All);
        AudioManager.Instance.AudioPlay("kickAir");
        buttonAttack.interactable = false;
        buttonSlope.interactable = false;
    }



    [PunRPC]
    public void RPC_Attack()
    {
        RaycastHit hit;
       

        if (Physics.Raycast(transform.position + transform.up, transform.forward, out hit, 15f))
        {
            var dest = hit.collider.transform.root.GetComponent<Destructible>();
            if (dest != null && dest.PhotonView.IsMine == false)
            {
                
                AudioManager.Instance.AudioStop("kickAir");
                AudioManager.Instance.AudioPlay("kick");
                AudioManager.Instance.AudioPlay("damage");
                hpEnemyText.enabled = true;
            

                //var playerUi = GetComponent<PlayerUi>();
                if (dest.CurrentHp < 15)
                {
                    var effect = PhotonNetwork.Instantiate(GameContainer.Instance.EffectDeath.name,
                        dest.transform.position,
                        Quaternion.identity);
                    StartCoroutine(CorDestroy());
                    IEnumerator CorDestroy()
                    {
                        hpEnemyText.enabled = false;
                        yield return new WaitForSeconds(2f);
                        PhotonNetwork.Destroy(effect);
                  
                    }
                    //playerUi.UpdateScore(50);
                }
   

                dest.Rpc_ApplyDamage();
                Debug.Log(dest.CurrentHp + " Жизней у врага");
                hpEnemyText.text = "Здоровье врага: " + dest.CurrentHp;
            }
        }
    }

  

    public void SlopeMode()
    {
        currentState = State.slope;
        player.SetIncreadible(true);
        buttonAttack.interactable = false;
        buttonSlope.interactable = false;
    }
}
