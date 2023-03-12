using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBanana : MonoBehaviour
{
    [SerializeField] private float radius, startTimer, time;
    [SerializeField] private bool loop;
    [SerializeField] private GameObject prefab;
    [SerializeField] private List<GameObject> bananas;
    private void Start()
    {
        bananas = new List<GameObject>();
        if (!loop)
        {
            Spawn();
        }
    }
    private void Update()
    {
            if (loop)
            {
                time -= Time.deltaTime;
                if (time <= 0)
                {
                    Spawn();
                    time = startTimer;
                }
            }
    }
    private void Spawn()
    {
        var gameManager = FindObjectOfType<GameManager>();
        if (bananas.Count < 5 && gameManager.Players.Count > 1)
        {
            var rnd = RandomCircle(transform.position, Random.Range(0, radius));
            var obj = PhotonNetwork.Instantiate(prefab.name, rnd, Quaternion.identity);
           var banana  = obj.GetComponent<Banana>();
            banana.OnDestroyBanana += OnRemoveList;
            bananas.Add(obj);
        }
    }
    public void OnRemoveList(Banana banana)
    {
        bananas.Remove(banana.gameObject);
    }
    public Vector3 RandomCircle(Vector3 center, float radius)
    {
        var ang = Random.value * 360;
        Vector3 pos = new Vector3();
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y;
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, radius);
    }
    
}
