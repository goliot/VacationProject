using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureState : MonoBehaviour
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

    protected virtual void ChangeAnimation()
    {

    }
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
