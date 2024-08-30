using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffects/BasicSword")]
public class BasicSwrod : ItemEffect
{
    public override void ExecuteRole()
    {
        Debug.Log("Basic Sword");
    }
}
