using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperEyes : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private Collider collider;
    [SerializeField] private SuperEyes otherSuperEyes;
    private Coroutine coroutine;
    private PlayerController playerController;
    [SerializeField]private PhotonView photonView;
    [SerializeField] private GameObject lightEffect;
    private PlayerUi playerUi;
    public bool IsSecond;
    private int scoreEnemy;
    public void Init(PlayerController player, bool numberEyes)
    {
        playerController = player;
        playerUi = playerController.Player.PlayerUI;
        IsSecond = numberEyes;
    }
    [PunRPC]
    public void RefrehScoreEnemy()
    {

        //if (playerUi.photonView.IsMine)
        //{
        //    Debug.Log(playerUi.Score + " очков у меня");
        //}
        //else
        //{
        //    Debug.Log(playerUi.Score + "  очков у врага");
        //}
    }
    public void ScaleEyes()
    {
        photonView.RPC("RefrehScoreEnemy", RpcTarget.All);
        Debug.Log(playerUi.Score + " очков у игрокаы");
        if (playerUi.Score > 0)
        {
            playerUi.TakeBanan();
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }
            coroutine = StartCoroutine(CorScale());
        }
        else
        {
            return;
        } 
       
        
        
        IEnumerator CorScale()
        {
            float timeCor = time;
            while (timeCor >= 0)
            {
                mesh.enabled = true;
                collider.enabled = true;
                lightEffect.gameObject.SetActive(true);
                timeCor -= 1;
                yield return new WaitForSecondsRealtime(0.1f);

            }
            yield return new WaitForEndOfFrame();
            Debug.Log("Конец корутины");
            mesh.enabled = false;
            collider.enabled = false;
            lightEffect.gameObject.SetActive(false);
            yield return new WaitForSeconds(1f);
            OffTextHpEnemy();
        }
    }

    public  void OffTextHpEnemy()
    {
        if (IsSecond == false)
            playerController.HpEnemyText.enabled = false;
        else
        {
            otherSuperEyes.OffTextHpEnemy();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var destr = other.GetComponent<Destructible>();
        if (destr && destr.photonView.IsMine == false && mesh.enabled && playerController.Player != destr)
        {
            destr.Rpc_ApplyDamage();
            ShowHpEnemy(destr);
        }
    }

    public void ShowHpEnemy(Destructible destr)
    {
        if (IsSecond == false)
        {
            playerController.HpEnemyText.enabled = true;
            playerController.HpEnemyText.text = "Здоровье врага: " + destr.CurrentHp;
        }
        else
        {
            otherSuperEyes.ShowHpEnemy(destr);
        }
    }
}
