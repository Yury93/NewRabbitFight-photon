using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SingleGame
{
    public class PlayerControllerSingleGame : MonoBehaviour
    {
        public enum State
        {
            idle,
            move,
            attack,
            slope,
        }
        [SerializeField] private CharacterController characterController;
        [SerializeField] private State currentState, lateState;
        [SerializeField] private Joystick joystick;
        [SerializeField] private Button buttonAttack, buttonSlope, buttonLaser;
        [SerializeField] private float startTimer, timerStateTransitions, speed;
        private Vector3 constPosY, inputVector;

        public State CurrentState => currentState;

        public event Action OnChangeState;

        void Start()
        {
            startTimer = timerStateTransitions;
            constPosY = transform.position;
            buttonAttack.onClick.AddListener(Attack);
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = new Vector3(transform.position.x, constPosY.y, transform.position.z);
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
                    //buttonSlope.interactable = true;
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


                    currentState = State.idle;

                    timerStateTransitions = startTimer;
                }
            }
            if (currentState == State.slope)
            {
                timerStateTransitions -= Time.deltaTime;
                if (timerStateTransitions <= 0)
                {


                    currentState = State.idle;

                    timerStateTransitions = startTimer;
                }
            }
        }
        private void Move()
        {
            characterController.Move(inputVector * speed * Time.deltaTime);
            transform.LookAt(transform.position + inputVector);
        }
        public void Attack()
        {
            currentState = State.attack;

            buttonAttack.interactable = false;
            //buttonSlope.interactable = false;
        }
    }
}