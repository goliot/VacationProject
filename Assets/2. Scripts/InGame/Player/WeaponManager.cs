using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour // �κ񿡼� ��� ����, �ű⿡ ���� ��� Ȱ��ȭ�ǵ���
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
