using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CreatureState : MonoBehaviour
{
    public enum State
    {
        Temp,
        Idle,
        Move,
        Dash,
        Jump,
        JumpWhileRun,
        Attack,
        Die
    }

    protected abstract void StateMachine();

    protected abstract void ChangeAnimation();

    protected abstract void CheckAnimationEnd();
}

[Serializable]
public class Stats
{
    public float health;
    public float maxHealth;
    public float speed;
    public float atk;
    public float atkSpeed;
}
