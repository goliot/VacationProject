using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;

    private float coolTime;

    private void Update()
    {
        coolTime += Time.deltaTime;

        if(coolTime > GameManager.Instance.spawnTime)
        {
            Spawn();
            coolTime = 0;
        }
    }

    private void Spawn()
    {
        GameObject obj = GameManager.Instance.pool.Get(0);
        obj.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
    }
}
