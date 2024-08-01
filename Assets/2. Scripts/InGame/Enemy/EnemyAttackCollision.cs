using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackCollision : MonoBehaviour
{
    [SerializeField]
    private MinionController minionController;

    private void OnEnable()
    {
        StartCoroutine(AutoDisable());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().TakeDamage(minionController.enemyData.attack);
        }
        else if(other.CompareTag("Minion"))
        {
            other.GetComponent<MinionController>().TakeDamage(minionController.enemyData.attack);
        }
    }

    private IEnumerator AutoDisable()
    {
        yield return new WaitForSeconds(0.1f);

        gameObject.SetActive(false);
    }
}