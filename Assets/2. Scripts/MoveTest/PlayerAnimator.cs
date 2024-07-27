using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [NonSerialized]
    public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnMovement(float horizontal, float vertical)
    {
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);
    }

    public void OnJump()
    {
        animator.SetTrigger("OnJump");
    }

    public void OnAttack()
    {
        animator.SetTrigger("OnAttack");
    }

    public void OnComboAttack()
    {
        animator.SetTrigger("OnWeaponAttack");
    }
}
