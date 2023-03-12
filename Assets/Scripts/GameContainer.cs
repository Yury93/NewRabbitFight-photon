using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameContainer : SingletonBase<GameContainer>
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private Button buttonAttack;
    [SerializeField] private Button buttonSlope,buttonLaser;
    [SerializeField] private GameObject effectDeath;
    [SerializeField]private  List<HitPointBar> hitPointBars;
    public Joystick PlayerJoystick => joystick;
    public Button ButtonAttack => buttonAttack;
    public Button ButtonSlope => buttonSlope;
    public Button ButtonLaser => buttonLaser;
    public GameObject EffectDeath => effectDeath;
    public List<HitPointBar> HitPointBars => hitPointBars;
}
