using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ ȿ��
/// </summary>
public abstract class ItemEffect : ScriptableObject
{
    public abstract void ExecuteEffect(); //���⿡ ȿ�� ����
    public abstract void CancelEffect();
}
