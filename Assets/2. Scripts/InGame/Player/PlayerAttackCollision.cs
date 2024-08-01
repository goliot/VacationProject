using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCollision : MonoBehaviour
{
    [SerializeField]
    PlayerController playerController;

    private void OnEnable()
    {
        StartCoroutine(AutoDisable());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Minion"))
        {
            other.GetComponent<MinionController>().TakeDamage(playerController.playerData.attack);
        }
    }

    private IEnumerator AutoDisable()
    {
        yield return new WaitForSeconds(0.1f);

        gameObject.SetActive(false);
    }
}
