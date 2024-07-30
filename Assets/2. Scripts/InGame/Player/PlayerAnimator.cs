using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [NonSerialized]
    public Animator animator;

    [SerializeField]
    private GameObject attackCollisionBox;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void StopAnim()
    {
        animator.speed = 0;
    }

    public void ResumeAnim()
    {
        animator.speed = 1;
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

    public void OnAttackCollision()
    {
        attackCollisionBox.SetActive(true);
    }

    public void OnHit()
    {
        animator.SetTrigger("OnHit");
    }

    public void Die()
    {
        animator.SetTrigger("OnDead");
    }
}
