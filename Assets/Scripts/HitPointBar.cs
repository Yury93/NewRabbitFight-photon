using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HitPointBar : MonoBehaviour
{
    [SerializeField] Image bar, supBar;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private Player player;
    public Vector3 offset;
    private Transform target, myTransform;

    private float curValue, maxValue;
    private float tempValue;
    public bool IsInit { get; set; }

    public void Init(Transform target, float maxHp)
    {
        IsInit = true;
        myTransform = GetComponent<Transform>();
        this.target = target;
        curValue = 0;
        maxValue = maxHp;
        Set(maxHp);
        player = target.GetComponent<Player>();

        hpText.text = player.CurrentHp + "/" + maxValue;
    }
    void Update()
    {
        if (IsInit == false) return;
        tempValue = Mathf.MoveTowards(tempValue, curValue, Time.unscaledDeltaTime * maxValue / 3f);
        bar.fillAmount = tempValue / maxValue;
        supBar.fillAmount = curValue / maxValue;
        if (bar.fillAmount < 0.05f) bar.fillAmount = 0.05f;
        if (supBar.fillAmount < 0.05f) supBar.fillAmount = 0.05f;


        myTransform.position = Camera.main.WorldToScreenPoint(target.position + offset);
    }
    public void Set(float val)
    {
        curValue = val;
        hpText.text = player.CurrentHp + "/" + maxValue;
    }
}
