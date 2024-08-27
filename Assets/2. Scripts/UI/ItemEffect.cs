using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 효과
/// </summary>
public abstract class ItemEffect : ScriptableObject
{
    public abstract bool ExecuteRole(); //여기에 효과 구현
}
