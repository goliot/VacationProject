using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public PlayerMove player;

    public float atk;

    private void Start()
    {
        atk = player.stat.atk;
    }
}
