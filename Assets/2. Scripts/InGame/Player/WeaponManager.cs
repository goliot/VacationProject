using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour // 로비에서 장비 고르면, 거기에 따른 장비가 활성화되도록
{
    private static WeaponManager instance = null;

    public GameObject WeaponNextStage;

    public static WeaponManager Instance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


}
