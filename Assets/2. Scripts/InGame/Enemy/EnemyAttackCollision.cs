using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackCollision : MonoBehaviour
{
    [SerializeField]
    private EnemyController enemyController;

    private void OnEnable()
    {
        StartCoroutine(AutoDisable());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().TakeDamage(enemyController.enemyData.attack);
        }
    }

    private IEnumerator AutoDisable()
    {
        yield return new WaitForSeconds(0.1f);

        gameObject.SetActive(false);
    }
}