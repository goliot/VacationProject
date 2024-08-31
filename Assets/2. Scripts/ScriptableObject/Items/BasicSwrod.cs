using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffects/BasicSword")]
public class BasicSwrod : ItemEffect
{
    public override void ExecuteEffect()
    {
        Debug.Log("Basic Sword");
        GameManager.Instance.player.GetComponent<PlayerController>().atkItemBonus += 10;
    }

    public override void CancelEffect()
    {
        GameManager.Instance.player.GetComponent<PlayerController>().atkItemBonus -= 10;
    }
}
