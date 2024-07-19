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
        Punch,
        Die
    }
}
