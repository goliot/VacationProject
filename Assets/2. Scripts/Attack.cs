using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    Animator anim;
    int hashAttackCount = Animator.StringToHash("AttackCount");

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public int AttackCount
    {
        get => anim.GetInteger(hashAttackCount);
        set => anim.SetInteger(hashAttackCount, value);
    }
}
